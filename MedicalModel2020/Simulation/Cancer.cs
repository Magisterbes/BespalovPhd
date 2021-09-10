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
        public int DiagnoseAge { get; set; }
        public int AfterDiagYears { get; set; }
        public bool IsCured { get; set; }
        public int DiagnoseStage { get; set; }
        public int ScreeningAge { get; set; }
        public int ScreeningStage { get; set; }
        public bool IsScreeningCured { get; set; }
        public double GrowthRate { get; set; }

        private Hazard DiagnoseHazard;
        private Hazard CancerDeathHazard;

        public Cancer(Person p)
        {
            IncidenceAge = p.Age;
            DiagnoseStage = -1;
            ScreeningStage = -1;
            ScreeningAge = -1;

            if (p.Sex == PersonSex.Female)
            {
                GrowthRate = Environment.Params.GrowthRateDistributionFemale.GenerateRandom();
                DiagnoseHazard = Environment.Params.DiagnoseHazardFemale;
                CancerDeathHazard = Environment.Params.CancerDeathHazardFemale;
            }
            else
            {
                GrowthRate = Environment.Params.GrowthRateDistributionMale.GenerateRandom();
                DiagnoseHazard = Environment.Params.DiagnoseHazardMale;
                CancerDeathHazard = Environment.Params.CancerDeathHazardMale;
            }

            CalculateHistory(p);

        }



        void CalculateHistory(Person p)
        {
            IsCured = false;
            IsScreeningCured = false;
            var stages = Environment.Params.StageCriteriaSize.Length + 1;
            StagesAges = GetStages(stages);
            DiagnoseAge = IncidenceAge + SimulateDiagnoseAge();
            StagesAges[stages-1] = StagesAges[stages - 2] + (int)CancerDeathHazard.T(0, 0);


            //See if person has died earier or before diagnose
            p.CancerDeathAge = StagesAges[stages - 1];
            if (DiagnoseAge > p.NaturalDeathAge && StagesAges[stages - 1] > p.NaturalDeathAge)
            {
                return;
            }


            //Check if it was diagnosed and cured
            for (int i = 0; i < StagesAges.Length; i++)
            {
                if(DiagnoseAge < StagesAges[i])
                {
                    DiagnoseStage = i;
                    var cured = CureIt(i, DiagnoseAge);

                    if (cured)
                    {
                        IsCured = true;
                    }
                    else
                    {
                        IsCured = false;
                    }
                    break;
                }
            }


        }


        int[] GetStages(int stagesLen)
        {
            var stages = Enumerable.Repeat(0, stagesLen).ToArray();

            for (int i = 0; i < Environment.Params.StageCriteriaSize.Length; i++)
            {
                stages[i] = IncidenceAge +(int)(Math.Log(Environment.Params.StageCriteriaSize[i]) / Math.Log(GrowthRate));
            }

            return stages;

        }

        int SimulateDiagnoseAge()
        {
            var simDiagSize = DiagnoseHazard.T(0, 0);
            var t = Math.Log(simDiagSize) / Math.Log(GrowthRate);

            return (int)Math.Round(t);

        }

        public bool CureIt(int Stage,int Age)
        {
            if (Tech.NextDouble(true) < Environment.Params.TreatmentEfficiency[Stage])
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
                    var cured = CureIt(i, DiagnoseAge);

                    if (cured)
                    {
                        IsScreeningCured = true;
                    }
                    else
                    {
                        IsScreeningCured = false;
                        IsCured = false;
                        p.CancerDeathAge = StagesAges[Environment.Params.StageCriteriaSize.Length];
                    }
                    break;
                }
            }
        }
    }
}
