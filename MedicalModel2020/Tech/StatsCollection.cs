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
        AtRisk
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
        SurvivalScreening
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
                {AggStatsType.DiagnoseStagesDistribution, Environment.Params.StageCriteriaSize.Length+1},
                {AggStatsType.ScreeningStagesDistribution, Environment.Params.StageCriteriaSize.Length+1},
                {AggStatsType.PeopleSaved, Environment.Params.UnrealLifeLength+1},
                {AggStatsType.YearsSaved, Environment.Params.UnrealLifeLength+1},
                {AggStatsType.FalsePositives, Environment.Params.YearsToSimulate+1},
                {AggStatsType.MortalityRates, Environment.Params.YearsToSimulate+1},
                {AggStatsType.ScreenedMortalityRates, Environment.Params.YearsToSimulate+1},
                {AggStatsType.IncidenceRates, Environment.Params.YearsToSimulate+1},
                {AggStatsType.Survival, 100},
                {AggStatsType.SurvivalScreening, 100}
             };

            this.AggStats = new Dictionary<AggStatsType, double[]>();
            foreach (var stype in Enum.GetValues(typeof(AggStatsType)))
            {
                this.AggStats[(AggStatsType)stype] = Enumerable.Repeat((double)0, AggInits[(AggStatsType)stype]).ToArray();

            }

            this.Stats = new Dictionary<StatsType, Dictionary<int, int[]>>();
            foreach (var stype in Enum.GetValues(typeof(StatsType)))
            {
                this.Stats[(StatsType)stype] = new Dictionary<int, int[]>();
                for (int i = 0; i < Environment.Params.YearsToSimulate; i++)
                {
                    this.Stats[(StatsType)stype][i] = Enumerable.Repeat(0, Environment.Params.UnrealLifeLength+1).ToArray();
                }
            }

        }

        public void UpdateStats(StatsType stype, int year, int age)
        {

            //lock (Stats)
            //{
                this.Stats[stype][year][age]++;
            //}
        }
        public void UpdateStats(StatsType stype, int year, int age, int value)
        {
            //lock (Stats)
            //{
                this.Stats[stype][year][age] += value;
            //}
        }

        public void GatherStats()
        {
            foreach (var p in Environment.Population)
            {

                if (p.IncidenceAge != -1
                    && p.IncidenceAge < p.NaturalDeathAge
                    && p.IncidenceAge + p.DateBirth <= Environment.CurrentDate
                    && p.CurrentCancer.DiagnoseStage != -1)
                {
                    AggStats[AggStatsType.DiagnoseStagesDistribution][p.CurrentCancer.DiagnoseStage]++;
                }


                if (p.CurrentCancer != null && p.CurrentCancer.ScreeningStage != -1)
                {
                    AggStats[AggStatsType.ScreeningStagesDistribution][p.CurrentCancer.ScreeningStage]++;

                }


                CalcSurvivalParallel(p);

                if (p.DeathCause == DeathStatus.NaturalSavedByScreening)
                {
                    AggStats[AggStatsType.PeopleSaved][p.IncidenceAge]++;
                    AggStats[AggStatsType.YearsSaved][p.IncidenceAge] += p.NaturalDeathAge - p.CancerDeathAge;
                }

                if (p.IsAlive
                    && p.CurrentCancer != null
                    && !p.CurrentCancer.IsCured
                    && p.CurrentCancer.IsScreeningCured
                    && p.CurrentCancer.ScreeningAge <= p.Age
                    && p.CancerDeathAge <= p.Age
                    )
                {
                    AggStats[AggStatsType.PeopleSaved][p.IncidenceAge]++;
                    AggStats[AggStatsType.YearsSaved][p.IncidenceAge] += p.Age - p.CancerDeathAge;
                }

            }

            AggStats[AggStatsType.MortalityRates] = MakeRates(Stats[StatsType.CancerMortality], Stats[StatsType.AtRisk]);
            AggStats[AggStatsType.ScreenedMortalityRates] = MakeRates(Stats[StatsType.CancerScreeningMortality], Stats[StatsType.AtRisk]);
            AggStats[AggStatsType.IncidenceRates] = MakeRates(Stats[StatsType.Inicdence], Stats[StatsType.AtRisk]);


            AggStats[AggStatsType.SurvivalScreening] = AggStats[AggStatsType.SurvivalScreening].Select(a => 100 * a / AggStats[AggStatsType.SurvivalScreening][0]).ToArray();
            AggStats[AggStatsType.Survival] = AggStats[AggStatsType.Survival].Select(a => 100 * a / AggStats[AggStatsType.Survival][0]).ToArray();

        }

        public double[] MakeRates(Dictionary<int, int[]> Data, Dictionary<int, int[]> Pop)
        {
            var data = FullSum(Data);
            var pop = FullSum(Pop);

            return data.Zip(pop, (d, p) => d / (p+1)).ToArray();
        }


        private double[] FullSum(Dictionary<int, int[]> Data)
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

        private void CalcSurvival(Person p)
        {


            if (p.DeathCause == DeathStatus.NaturalSavedByScreening ||
                p.DeathCause == DeathStatus.NaturalCured ||
                p.DeathCause == DeathStatus.Cancer)
            {

                if (p.CurrentCancer.ScreeningAge != -1)
                {
                    if (p.CurrentCancer.IsScreeningCured)
                        itterSurv(p.CurrentCancer.DiagnoseAge, p.NaturalDeathAge, AggStatsType.SurvivalScreening);
                    else
                        itterSurv(p.CurrentCancer.DiagnoseAge, p.CancerDeathAge, AggStatsType.SurvivalScreening);
                }
                else
                {
                    if (p.CurrentCancer.IsCured)
                        itterSurv(p.CurrentCancer.DiagnoseAge, p.NaturalDeathAge, AggStatsType.Survival);
                    else
                        itterSurv(p.CurrentCancer.DiagnoseAge, p.CancerDeathAge, AggStatsType.Survival);
                }
                
                
            }

        }

        private void CalcSurvivalParallel(Person p)
        {


            if (p.DeathCause == DeathStatus.NaturalSavedByScreening)
            {
                itterSurv(p.CurrentCancer.IncidenceAge, p.NaturalDeathAge, AggStatsType.SurvivalScreening);
                itterSurv(p.CurrentCancer.IncidenceAge, p.CancerDeathAge, AggStatsType.Survival);
            }


            if (p.DeathCause == DeathStatus.Cancer)
            {
                itterSurv(p.CurrentCancer.IncidenceAge, p.CancerDeathAge, AggStatsType.SurvivalScreening);
                itterSurv(p.CurrentCancer.IncidenceAge, p.CancerDeathAge, AggStatsType.Survival);
            }


            if (p.DeathCause == DeathStatus.NaturalCured)
            {
                itterSurv(p.CurrentCancer.IncidenceAge, p.NaturalDeathAge, AggStatsType.SurvivalScreening);
                itterSurv(p.CurrentCancer.IncidenceAge, p.NaturalDeathAge, AggStatsType.Survival);
            }

        }

        private void itterSurv(int beg, int fin, AggStatsType astype)
        {
            AggStats[astype][0]++;
            for (int i = 0; i < fin - beg; i++)
            {
                AggStats[astype][i+1] += 1.0;
            }
        }
        
    }



}
