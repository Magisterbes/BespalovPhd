using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalModel
{
    class Cancer
    {
        public int IncidenceAge { get; set; }
        public int[] StagesAges { get; set; }
        public int DiagnosisAge { get; set; }
        public int AfterDiagYears { get; set; }
        public bool IsCured { get; set; }
        public int DiagnoseStage { get; set; }
        public int MalignancyAge { get; set; }
        public int ScreeningAge { get; set; }
        public int ScreeningStage { get; set; }
        public bool IsScreeningCured { get; set; }
        public double GrowthRate { get; set; }
        public double Reoccurred { get; set; }

        private Hazard DiagnoseHazard;
        private Hazard CancerDeathHazard;
        private Hazard MalignancyHazard;

        public Cancer(Person p)
        {
            DiagnosisAge = p.DiagnosisAge;
            DiagnoseStage = -1;
            ScreeningStage = -1;
            ScreeningAge = -1;
            MalignancyAge = -1;

            DiagnoseHazard = Environment.Params.DiagnoseHazard;
            CancerDeathHazard = Environment.Params.CancerDeathHazard;
            // MalignancyHazard = Environment.Params.MalignancyHazard;

            CalculateHistory(p);

        }



        void CalculateHistory(Person p)
        {
            IsCured = false;
            IsScreeningCured = false;
            int stages = Environment.Params.StageCriteria.Length+1;
            IncidenceAge = DiagnosisAge - SimulateIncidenceAge();
            if (IncidenceAge < 0)
            {
                IncidenceAge = 0;
            }

            if (p.DateBirth + IncidenceAge < Environment.Params.YearsToSimulate && (p.DateBirth + IncidenceAge >= 0) && IncidenceAge < Environment.Params.UnrealLifeLength)
            {
                Environment.Stats.UpdateStats(StatsType.Inicdence, IncidenceAge + p.DateBirth, IncidenceAge);
            }

            StagesAges = GetStagesAges(GrowthRate,IncidenceAge);
            
            StagesAges[stages-1] = StagesAges[stages - 2] + (int)CancerDeathHazard.T(0, 0);
            Reoccurred = Tech.CheckByProb(Environment.Params.ReoccurrenceProbability); 

            //See if person has died earier or before diagnose
            p.CancerDeathAge = StagesAges[stages - 1];
            if (DiagnosisAge > p.NaturalDeathAge && StagesAges[stages - 1] > p.NaturalDeathAge)
            {
                return;
            }


            if (p.DateBirth + DiagnosisAge < Environment.Params.YearsToSimulate && (p.DateBirth + DiagnosisAge>=0)&& DiagnosisAge<Environment.Params.UnrealLifeLength)
                Environment.Stats.UpdateStats(StatsType.Diagnosis, p.DateBirth+ DiagnosisAge, DiagnosisAge);

 
            var cured = CureIt(DiagnoseStage, DiagnosisAge);

            if (cured)
            {
                IsCured = true;
            }
            else
            {
                IsCured = false;
            }


        }


        double GetGrowthRate(double stage)
        {
            var isAgg = (Tech.CheckByProb(Environment.Params.ProportionOfAggressive[(int)stage-1])==1);
            var low = Environment.Params.GrowthRateLimits[0];
            var up = Environment.Params.GrowthRateLimits[1];
            var thr = Environment.Params.AggressivenessRateThreshold;

            if (isAgg)
            {
                return low + (1 - thr) * (up - low) * Tech.NextDouble(false);
            }
            else
            {
                return low + (1 - thr) * (up - low) + thr *(up - low) * Tech.NextDouble(false);
            }

        }

        double GetTumorSize(double stage)
        {
            var tumorSize = 1.0;
            var crit = Environment.Params.StageCriteria;

            switch (DiagnoseStage)
            {
                case 1:
                    tumorSize = Tech.GetUni(0, crit[0]);
                    break;
                case 2:
                    tumorSize = Tech.GetUni(crit[0], crit[1]);
                    break;
                case 3:
                    tumorSize = Tech.GetUni(crit[1], crit[2]);
                    break;
                case 4:
                    tumorSize = Tech.GetUni(crit[2], 2 * crit[2]);
                    break;
            }

            return tumorSize;

        }

        int SimulateIncidenceAge()
        {
            this.DiagnoseStage = Tech.StageMlt.Sample().ToList().IndexOf(1)+1;

            double tumorSize = GetTumorSize(DiagnoseStage);
            this.GrowthRate = GetGrowthRate(DiagnoseStage);
            
            var t = Math.Log(tumorSize) / Math.Log(this.GrowthRate);

            if (t < 0)
            {
                t = 1;
            }


            return (int)Math.Round(t);

        }

        public bool CureIt(int Stage,int Age)
        {
            if (Tech.NextDouble(true) < Environment.Params.TreatmentEfficiency[Stage-1])
            {
                return true;
            }

            return false;
        }


        public void FromScreening(Person p)
        {
            ScreeningAge = p.Age;
            for (int i = 0; i < StagesAges.Length; i++)
            {
                if (ScreeningAge < StagesAges[i])
                {
                    ScreeningStage = i;
                    var cured = CureIt(i+1, DiagnosisAge);

                    if (cured)
                    {
                        IsScreeningCured = true;
                    }
                    else
                    {
                        IsScreeningCured = false;
                        IsCured = false;
                        p.CancerDeathAge = StagesAges[StagesAges.Length-1];
                    }
                    break;
                }
            }

        }


        int[] GetStagesAges(double GrowthRate, int IncidenceAge)
        {
            var stages = Enumerable.Repeat(0, 4).ToArray();
            var crit = Environment.Params.StageCriteria;

            for (int i = 0; i < crit.Length; i++)
            {
                var years = (int)(Math.Log(crit[i]) / Math.Log(GrowthRate));

                stages[i] = IncidenceAge + years;

            }


            return stages;

        }

    }
}
