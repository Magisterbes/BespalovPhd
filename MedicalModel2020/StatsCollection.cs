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

    class StatsCollection
    {
        public Dictionary<StatsType,Dictionary<int,int[]>> Stats;
        public int[] DiagnoseStagesDistribution;
        public int[] ScreeningStagesDistribution;
        public int[] PeopleSaved;
        public int[] YearsSaved;
        public int[] FalsePositives;
        public double[] MortalityRates;
        public double[] ScreenedMortalityRates;
        public double[] IncidenceRates;

        public StatsCollection(int LastYear)
        {

            this.DiagnoseStagesDistribution = new int[Environment.Params.CancerTransitions.Length];
            this.ScreeningStagesDistribution = new int[Environment.Params.CancerTransitions.Length];
            this.PeopleSaved = new int[Environment.Params.UnrealLifeLength];
            this.YearsSaved = new int[Environment.Params.UnrealLifeLength];
            this.FalsePositives = new int[Environment.Params.YearsToSimulate];
            this.MortalityRates = new double[Environment.Params.YearsToSimulate];
            this.ScreenedMortalityRates = new double[Environment.Params.YearsToSimulate];
            this.IncidenceRates = new double[Environment.Params.YearsToSimulate];


            this.Stats = new Dictionary<StatsType, Dictionary<int, int[]>>();
            foreach (var stype in Enum.GetValues(typeof(StatsType)))
            {
                this.Stats[(StatsType)stype] = new Dictionary<int, int[]>();
                for (int i = 0; i < Environment.Params.YearsToSimulate; i++)
                {
                    this.Stats[(StatsType)stype][i] = Enumerable.Repeat(0, Environment.Params.UnrealLifeLength).ToArray();
                }
            }

        }

        private static object locker = new object();

        public void UpdateStats(StatsType stype, int year, int age)
        {

            //lock (locker)
            //{
                this.Stats[stype][year][age]++;
            //}
        }
        public void UpdateStats(StatsType stype, int year, int age, int value)
        {
            //lock (locker)
            //{
                this.Stats[stype][year][age] += value;
            //}
        }

        public void GatherStats()
        {
            foreach (var p in Environment.Population)
            {

                if(p.IncidenceAge != -1 
                    && p.IncidenceAge < p.NaturalDeathAge
                    && p.IncidenceAge + p.DateBirth <= Environment.CurrentDate 
                    && p.CurrentCancer.DiagnoseStage != -1)
                {
                    this.DiagnoseStagesDistribution[p.CurrentCancer.DiagnoseStage]++;
                }

                if(p.CurrentCancer != null && p.CurrentCancer.ScreeningStage != -1)
                {
                    this.ScreeningStagesDistribution[p.CurrentCancer.ScreeningStage]++;
                }

                if(p.DeathCause == DeathStatus.NaturalSavedByScreening)
                {
                    this.PeopleSaved[p.IncidenceAge]++;
                    this.YearsSaved[p.IncidenceAge]+=p.NaturalDeathAge - p.CancerDeathAge;
                }

                if (p.IsAlive
                    && p.CurrentCancer != null
                    && !p.CurrentCancer.IsCured
                    && p.CurrentCancer.IsScreeningCured
                    && p.CurrentCancer.ScreeningAge <= p.Age
                    && p.CancerDeathAge <= p.Age
                    )
                {
                    this.PeopleSaved[p.IncidenceAge]++;
                    this.YearsSaved[p.IncidenceAge] += p.Age - p.CancerDeathAge;
                }

            }

            MortalityRates = MakeRates(Stats[StatsType.CancerMortality], Stats[StatsType.AtRisk]);
            ScreenedMortalityRates = MakeRates(Stats[StatsType.CancerScreeningMortality], Stats[StatsType.AtRisk]);
            IncidenceRates = MakeRates(Stats[StatsType.Inicdence], Stats[StatsType.AtRisk]);

        }

        public double[] MakeRates(Dictionary<int,int[]> Data, Dictionary<int, int[]> Pop)
        {
            var data = FullSum(Data);
            var pop = FullSum(Pop);

            return data.Zip(pop, (d, p) => d / p).ToArray();
        }
        

        private double[] FullSum(Dictionary<int, int[]> Data)
        {
            var res = Enumerable.Repeat((double)0, Environment.Params.UnrealLifeLength).ToArray();
            foreach (var key in Data.Keys)
            {
                for (int i = 0; i < Data[key].Length; i++)
                {
                    res[i] += Data[key][i];
                }
            }

            return res;
        }
    }



}
