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
            var sexRand = Tech.NextDouble(false);

            if(sexRand > prms.MaleVsFemale)
            {
                p.Sex = PersonSex.Female;
                GetBirthday(prms.InitAgeDistFemale, p, isNew);
                GetNaturalDeath(prms.FemaleAging, p);
                GetIncidence(prms.IncidenceHazardFemale, p);

            }
            else
            {
                p.Sex = PersonSex.Male;
                GetBirthday(prms.InitAgeDistMale, p, isNew);
                GetNaturalDeath(prms.MaleAging, p);
                GetIncidence(prms.IncidenceHazardMale, p);

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

        private static void GetIncidence(Hazard hz, Person p)
        {
            if (p.Age>98)
            {
                p.IncidenceAge = -1;
                return;
            }

            while (p.Age >= p.IncidenceAge)
            {
                p.IncidenceAge = (int)hz.T(0,0);
            }

        }

    }



}
