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
            IncidenceAge = p.IncidenceAge;
            DiagnoseStage = -1;
            ScreeningStage = -1;
            ScreeningAge = -1;
            MalignancyAge = -1;

            GrowthRate = Environment.Params.GrowthRateDistribution.GenerateRandom();
            DiagnoseHazard = Environment.Params.DiagnoseHazard;
            CancerDeathHazard = Environment.Params.CancerDeathHazard;
            MalignancyHazard = Environment.Params.MalignancyHazard;
            
            CalculateHistory(p);

        }



        void CalculateHistory(Person p)
        {
            IsCured = false;
            IsScreeningCured = false;
            int stages = Environment.Params.StageCriteria.Stages;
            MalignancyAge = IncidenceAge + (int)MalignancyHazard.T(0, 0);
            StagesAges = Environment.Params.StageCriteria.GetStages(GrowthRate,IncidenceAge,MalignancyAge);
            DiagnoseAge = IncidenceAge + SimulateDiagnoseAge();
            StagesAges[stages-1] = StagesAges[stages - 2] + (int)CancerDeathHazard.T(0, 0);
            Reoccurred = Tech.CheckByProb(Environment.Params.ReoccurrenceProbability); 

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
                        p.CancerDeathAge = StagesAges[Environment.Params.StageCriteria.Stages-1];
                    }
                    break;
                }
            }
        }
    }
}
