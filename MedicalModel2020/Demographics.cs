using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalModel
{

    static class Demo
    {

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
                GetIncidence(prms.FemaleIncidenceProbs, p);

            }
            else
            {
                p.Sex = PersonSex.Male;
                GetBirthday(prms.InitAgeDistMale, p, isNew);
                GetNaturalDeath(prms.MaleAging, p);
                GetIncidence(prms.MaleIncidenceProbs, p);

            }


            return p;
        }

        private static void GetBirthday(Distiribution distr, Person p, bool isNew)
        {
            if (isNew)
            {
                p.DateBirth = Environment.CurrentDate;
            }
            else
            {
                p.DateBirth = Environment.CurrentDate - distr.GenerateRandom();
            }

        }

        private static void GetNaturalDeath(Distiribution distr, Person p)
        {
            while (p.Age >= p.NaturalDeathAge)
            {
                p.NaturalDeathAge = distr.GenerateRandom();
            }

        }

        private static void GetIncidence(Distiribution distr, Person p)
        {
            if (distr.NormalizationCoef < Tech.NextDouble(false) || p.Age>98)
            {
                p.IncidenceAge = -1;
                return;
            }

            while (p.Age >= p.IncidenceAge)
            {
                p.IncidenceAge = distr.GenerateRandom();
            }

        }

    }



}
