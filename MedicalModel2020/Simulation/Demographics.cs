using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalModel
{

    static class Demo
    {

        public static int errors = 0;

        public static void GenerateMigraion(Parameters prms, Person p)
        {
            
        }


        public static Person GenerateAgeAndGender(Parameters prms, int id, bool isNew)
        {
            var p = new Person(id);
            
            GetBirthday(prms.InitAgeDist, p, isNew);
            GetNaturalDeath(prms.Aging, p);
            GetDiagnose(prms.DiagnoseHazard, p);


            if (p.DiagnosisAge != Environment.Params.UnrealLifeLength && p.DiagnosisAge< p.NaturalDeathAge)
            {
                p.CurrentCancer = new Cancer(p);
                // Environment.Stats.UpdateStats(StatsType.Inicdence, Environment.CurrentDate - (p.Age- p.IncidenceAge), p.IncidenceAge, p.Sex);
            }


            return p;
        }

        private static void GetBirthday(Distribution distr, Person p, bool isNew)
        {
            if (isNew)
            {
                p.DateBirth = Environment.CurrentDate;
            }
            else
            {
                var r = distr.GenerateRandom();
                p.DateBirth = Environment.CurrentDate - r;
            }

        }

        private static void GetNaturalDeath(Distribution distr, Person p)
        {
            var counter = 0;
            while (p.Age >= p.NaturalDeathAge && counter<5)
            {
                p.NaturalDeathAge = distr.GenerateRandom();
                counter++;
            }
            
            if (p.Age >= p.NaturalDeathAge)
            {
                p.NaturalDeathAge = p.Age + 1;
                errors++;
            }

        }

        private static void GetDiagnose(Hazard hz, Person p)
        {
            if (p.Age>98)
            {
                p.DiagnosisAge = -1;
                return;
            }

            //while (p.Age >= p.IncidenceAge)
            //{
                p.DiagnosisAge = (int)hz.T(0,0);
           // }

        }

    }



}
