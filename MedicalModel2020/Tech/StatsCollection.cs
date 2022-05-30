using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalModel
{

    public enum StatsType
    {
        AgeDistributions, 
        Inicdence,
        CancerMortality,
        CancerScreeningMortality,
        AtRisk,
        Diagnosis
    }

    public enum AggStatsType
    {
        DiagnoseStagesDistribution,
        ScreeningStagesDistribution,
        PeopleSaved,
        YearsSaved,
        FalsePositives,
        MortalityRates,
        ScreenedMortalityRates,
        IncidenceRates,
        Survival,
        SurvivalScreening,
        DiagnosisRates,
        CancerMortalityAge,
        CancerMortalityAgeScreen,
        AfterDiagnosis
    }

    class StatsCollection
    {
        public Dictionary<StatsType, Dictionary<int, int[]>> Stats;
        public Dictionary<AggStatsType, double[]> AggStats;
        private Dictionary<AggStatsType, int> AggInits;

        public StatsCollection(int LastYear)
        {

            AggInits = new Dictionary<AggStatsType, int>
            {
                {AggStatsType.DiagnoseStagesDistribution, Environment.Params.StageDistributionLength},
                {AggStatsType.ScreeningStagesDistribution, Environment.Params.StageDistributionLength},
                {AggStatsType.PeopleSaved, Environment.Params.UnrealLifeLength+1},
                {AggStatsType.YearsSaved, Environment.Params.UnrealLifeLength+1},
                {AggStatsType.FalsePositives, Environment.Params.YearsToSimulate+1},
                {AggStatsType.MortalityRates, Environment.Params.YearsToSimulate+1},
                {AggStatsType.ScreenedMortalityRates, Environment.Params.YearsToSimulate+1},
                {AggStatsType.IncidenceRates, Environment.Params.YearsToSimulate+1},
                {AggStatsType.Survival, 100},
                {AggStatsType.SurvivalScreening, 100},
                {AggStatsType.CancerMortalityAge, 100},
                {AggStatsType.CancerMortalityAgeScreen, 100},
                {AggStatsType.AfterDiagnosis, 200},
                {AggStatsType.DiagnosisRates, 100}
             };

            InitStats(ref AggStats, ref Stats);


        }

        private void InitStats(ref Dictionary<AggStatsType,double[]> agg, ref Dictionary<StatsType,Dictionary<int,int[]>> st)
        {

            agg = new Dictionary<AggStatsType, double[]>();
            foreach (var stype in Enum.GetValues(typeof(AggStatsType)))
            {
                agg[(AggStatsType)stype] = Enumerable.Repeat((double)0, AggInits[(AggStatsType)stype]).ToArray();

            }

            st = new Dictionary<StatsType, Dictionary<int, int[]>>();
            foreach (var stype in Enum.GetValues(typeof(StatsType)))
            {
                st[(StatsType)stype] = new Dictionary<int, int[]>();
                for (int i = 0; i < Environment.Params.YearsToSimulate; i++)
                {
                    st[(StatsType)stype][i] = Enumerable.Repeat(0, Environment.Params.UnrealLifeLength + 1).ToArray();
                }
            }
        }

        public void UpdateStats(StatsType stype, int year, int age)
        {
            this.Stats[stype][year][age]++;
        }
        public void UpdateStats(StatsType stype, int year, int age, int value)
        {

            this.Stats[stype][year][age] += value;   
        }

        public void GatherStats()
        {
            foreach (var p in Environment.Population)
            {
                GatherOne(p, ref AggStats);

            }

            GatherCalc(AggStats, ref Stats);
        }


        private void GatherCalc(Dictionary<AggStatsType, double[]> agg, ref Dictionary<StatsType, Dictionary<int, int[]>> st)
        {

            agg[AggStatsType.MortalityRates] = GetAvgStats(st[StatsType.CancerMortality], st[StatsType.AtRisk]);
            agg[AggStatsType.ScreenedMortalityRates] = GetAvgStats(st[StatsType.CancerScreeningMortality], st[StatsType.AtRisk]);
            agg[AggStatsType.IncidenceRates] = GetAvgStats(st[StatsType.Inicdence], st[StatsType.AgeDistributions]);
            agg[AggStatsType.DiagnosisRates] = GetAvgStats(st[StatsType.Diagnosis], st[StatsType.AgeDistributions]);


            agg[AggStatsType.SurvivalScreening] = CauseSurvEst(agg[AggStatsType.SurvivalScreening], agg[AggStatsType.CancerMortalityAgeScreen]);
            agg[AggStatsType.Survival] = CauseSurvEst(agg[AggStatsType.Survival], agg[AggStatsType.CancerMortalityAge]);

        }


        private double[] CauseSurvEst(double[] alive, double[] dead)
        {

            List<double> h = new List<double>();
            List<double> S = new List<double>();

            for (int i = 0; i < alive.Length - 1; i++)
            {               
                h.Add(dead[i] / alive[i]);

                var H = h.Sum();

                S.Add(Math.Exp(-H));
            }


            return S.ToArray();
        }


        private double[] SurvEst(double[] alive)
        {

            var diff = alive.Zip(alive.Skip(1), (a, b) => Math.Abs(b - a)).ToArray();

            List<double> S = new List<double>();

            for (int i = 0; i < alive.Length-1; i++)
            {
                if (i == 0) { S.Add(1); continue; }

                S.Add(S[i - 1] * (1 - diff[i] / alive[i]));


            }

            return S.ToArray();                
        }

        private void GatherOne(Person p, ref Dictionary<AggStatsType, double[]> agg)
        {
            if (p.DiagnosisAge != -1
                    && p.DiagnosisAge < p.NaturalDeathAge
                    && p.DiagnosisAge + p.DateBirth <= Environment.CurrentDate
                    && p.CurrentCancer.DiagnoseStage != -1)
            {
                agg[AggStatsType.DiagnoseStagesDistribution][p.CurrentCancer.DiagnoseStage-1]++;
            }

            if (p.CurrentCancer != null)
            {
                var diff = p.CurrentCancer.StagesAges[3] - p.CurrentCancer.DiagnosisAge;

                if (diff > 199)
                {
                    diff = 199;
                }

                if (diff <= 0)
                {
                    diff = 0;
                }

                agg[AggStatsType.AfterDiagnosis][(int)diff]++;
            }

            if (p.CurrentCancer != null && p.CurrentCancer.ScreeningStage != -1)
            {
                agg[AggStatsType.ScreeningStagesDistribution][p.CurrentCancer.ScreeningStage-1]++;

            }


            CalcSurvivalParallel(p);

            if (p.DeathCause == DeathStatus.NaturalSavedByScreening)
            {
                agg[AggStatsType.PeopleSaved][p.CurrentCancer.IncidenceAge]++;

                for (int i = (int)p.CurrentCancer.CancerDeathAgeScreen; i < p.NaturalDeathAge; i++)
                {
                    agg[AggStatsType.YearsSaved][i] += 1;
                }                
            }

            if (p.IsAlive
                && p.CurrentCancer != null
                && !p.CurrentCancer.IsCured
                && p.CurrentCancer.IsScreeningCured
                && p.CurrentCancer.ScreeningAge <= p.Age
                && p.CurrentCancer.CancerDeathAgeInit <= p.Age
                )
            {


                agg[AggStatsType.PeopleSaved][p.CurrentCancer.IncidenceAge]++;
                agg[AggStatsType.YearsSaved][p.CurrentCancer.IncidenceAge] += (int)p.CancerDeathAgeScreening- (int)p.CancerDeathAgeNoScreening;
            }

        }

        /// <summary>
        /// Not used
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="Pop"></param>
        /// <returns></returns>
        public double[] MakeRates(Dictionary<int, int[]> Data, Dictionary<int, int[]> Pop)
        {
            var data = FullSum(Data);
            var pop = FullSum(Pop);

            return data.Zip(pop, (d, p) => d / (p+1)).ToArray();
        }


        public double[] FullSum(Dictionary<int, int[]> Data)
        {
            var res = Enumerable.Repeat((double)0, Environment.Params.UnrealLifeLength+1).ToArray();
            foreach (var key in Data.Keys)
            {
                for (int i = 0; i < Data[key].Length; i++)
                {
                    res[i] += Data[key][i];
                }
            }

            return res;
        }

        private double[] GetAvgStats(Dictionary<int, int[]> vals, Dictionary<int, int[]> atrisk)
        {
            var sz = 100;
            var cases = new double[sz];
            var pop = new double[sz];

            for (int i = 1; i < vals.Count; i++)
            {
                for (int j = 0; j < sz; j++)
                {
                    cases[j] += vals[i][j];
                    pop[j] += atrisk[i][j];

                }
            }


            var final = new List<double>();

            for (int i = 0; i < sz; i++)
            {
                if (pop[i] != 0)
                {
                    final.Add(cases[i]/pop[i]);
                }
                else
                {
                    final.Add(0);
                }
            }

            return final.ToArray();
        }

        /// <summary>
        /// Not used smoothing function
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="Pop"></param>
        /// <returns></returns>
        private double[] Smooth(double[] input)
        {
            double[] final = (double[])input.Clone();

            for (int i = 2; i < input.Length-2; i++)
            {
                final[i] = input[i - 2] * 0.05 + input[i - 1] * 0.2 + input[i] * 0.5 + input[i + 1] * 0.2 +
                    input[i + 2] * 0.05;



            }
            return final;
        }


        private void CalcSurvivalParallel(Person p)
        {

            if ((p.CurrentCancer == null) ||(p.DiagnosisAge> p.Age)||(p.DiagnosisAge ==-1))
                return;

            var screenAge = p.CurrentCancer.DiagnosisAge;

            if (p.CurrentCancer.ScreeningFound)
            {
                screenAge = p.CurrentCancer.ScreeningAge;
            }

            var minAge = (new double[] { p.NaturalDeathAge, p.Age, 
                            p.CancerDeathAgeNoScreening}).Min();

            var minAgeSceen = (new double[] { p.NaturalDeathAge, p.Age,
                            p.CancerDeathAgeScreening}).Min();


            itterSurv(screenAge, (int)minAgeSceen, AggStatsType.SurvivalScreening);
            itterSurv(p.CurrentCancer.DiagnosisAge, (int)minAge, AggStatsType.Survival);

            if(p.CancerDeathAgeNoScreening != -1 && p.CancerDeathAgeNoScreening <= p.NaturalDeathAge)
            {
                AggStats[AggStatsType.CancerMortalityAge][p.CancerDeathAgeNoScreening- p.CurrentCancer.DiagnosisAge]++;
            }

            if (p.CancerDeathAgeScreening != -1 && p.CancerDeathAgeScreening <= p.NaturalDeathAge)
            {

                AggStats[AggStatsType.CancerMortalityAgeScreen][p.CancerDeathAgeScreening - screenAge]++;
            }


            return;
            
        }

        private void itterSurv(int beg, int fin, AggStatsType astype)
        {
            if (beg<0)
                beg = 0;
            if (fin >= AggStats[astype].Count())
                fin = AggStats[astype].Count() - 1;


            AggStats[astype][0]++;
            for (int i = 0; i < fin - beg; i++)
            {
                AggStats[astype][i+1] += 1.0;
            }
        }
        
    }



}
