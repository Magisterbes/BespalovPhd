using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using Meta.Numerics;


namespace MedicalModel
{

    static class Environment
    {

        static public List<Person> Population { set; get; }
        static public Parameters Params { set; get; }
        static public StatsCollection Stats { set; get; }
        static public int MaxID {set;get;}

        static int _currentDate = 0;
        public static int CurrentDate
        {
            set
            {
                if (_currentDate + 1 != value && value != 0)
                {
                    throw new Exception("Wrong value");
                }
                _currentDate = value;

                if (_currentDate>0)
                    ItteratePopulation();
            }
            get
            {
                return _currentDate;
            }
        }
        static public void Init(string filename)
        {
            Params = new Parameters(filename);
            Tech.Setup();
            Stats = new StatsCollection(Params.YearsToSimulate);
            Environment.CurrentDate = 0;
            
        }

        static public void Start()
        {
            Stats = new StatsCollection(Params.YearsToSimulate);
            Environment.CurrentDate = 0;
            Population = new List<Person>();

            for (int i = 0; i < Params.InitPopulation; i++)
            {
                Population.Add(Demo.GenerateAgeAndGender(Params, i, false));
            }

        }


        static public void ItteratePopulation()
        {
            int check = 0;
            Population.ForEach(p =>
            {
                if (p.Age == p.NaturalDeathAge)
                {
                    p.IsAlive = false;

                    if (p.DeathCause == DeathStatus.NaturalSavedByScreening)
                    {
                        Stats.UpdateStats(StatsType.CancerMortality, CurrentDate, p.Age);
                    }
                }
                

                if (p.IsAlive)
                {


                    if (CurrentDate >= Environment.Params.ScreeningDate
                        && ((CurrentDate - Environment.Params.ScreeningDate) % Environment.Params.Freqency) == 0
                        && CheckScreening(p))
                    {
                        Screening.ScreenPerson(p);
                    }
                }

                if (p.IsAlive)
                {
                    Stats.UpdateStats(StatsType.AgeDistributions, CurrentDate, p.Age);

                    if (p.DiagnosisAge == Environment.Params.UnrealLifeLength || p.DiagnosisAge > p.Age)
                    {
                        Stats.UpdateStats(StatsType.AtRisk, CurrentDate, p.Age);
                    }
                    else
                    {
                        check++;
                    }

                    if (p.Age == p.CancerDeathAge)
                    {
                        if (p.DeathCause == DeathStatus.Cancer)
                        {
                            Stats.UpdateStats(StatsType.CancerMortality, CurrentDate, p.Age);
                            Stats.UpdateStats(StatsType.CancerScreeningMortality, CurrentDate, p.Age);
                        }

                    }
                    
                }


            });


        }


        static bool CheckScreening(Person p)
        {
            if (!(p.CurrentCancer != null && p.CurrentCancer.ScreeningAge != -1 )&&
                p.Age >= Environment.Params.StartAge &&
                p.Age <= Environment.Params.FinishAge &&
                p.Age < p.DiagnosisAge &&
                Tech.NextDouble(true)< Environment.Params.ParticipationRate)
                return true;
            return false;
        }


    }
}
