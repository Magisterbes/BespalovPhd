using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalModel
{
    static class Screening
    {

        public static double TP;
        public static double FP;

        public static void ScreenPerson(Person p)
        {
            if(p.IncidenceAge  == -1 || p.IncidenceAge > p.Age)
            {
                if(Tech.CheckByProb(FP) == 1)
                {
                    Environment.Stats.AggStats[AggStatsType.FalsePositives][Environment.CurrentDate]++;
                }
            }
            else
            {
                if (p.IncidenceAge <= p.Age && p.CurrentCancer.DiagnoseAge> p.Age && Tech.CheckByProb(TP) == 1)
                {
                    p.CurrentCancer.FromScreening(p);
                }

            }
        }

    }
}
