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

        public Cancer(Person p)
        {
            IncidenceAge = p.Age;
            DiagnoseStage = -1;
            ScreeningStage = -1;
            ScreeningAge = -1;

            CalculateHistory(p);

        }

        void CalculateHistory(Person p)
        {
            IsCured = false;
            IsScreeningCured = false;
            StagesAges = Enumerable.Repeat(0, Environment.Params.StageCriteriaSize.Length+1).ToArray();
            var diagParam = Environment.Params.DiagnoseHazard;
            DiagnoseAge = IncidenceAge + (int)Tech.ExpSample(Tech.Diag);

            //Get Cancer Transitions
            for (int i = 0; i < Tech.StageTrans.Count; i++)
            {
                if (i == 0)
                    StagesAges[i] = IncidenceAge + (int)Tech.ExpSample(Tech.StageTrans[i]);
                else
                    StagesAges[i] = StagesAges[i - 1] + (int)Tech.ExpSample(Tech.StageTrans[i]);
            }

            p.CancerDeathAge = StagesAges[Tech.StageTrans.Count - 1];
            //See if person has died earier or before diagnose
            if (DiagnoseAge > p.NaturalDeathAge && StagesAges[Tech.StageTrans.Count - 1]> p.NaturalDeathAge)
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
                        p.CancerDeathAge = StagesAges[Tech.StageTrans.Count - 1];
                    }
                    break;
                }
            }
        }
    }
}
