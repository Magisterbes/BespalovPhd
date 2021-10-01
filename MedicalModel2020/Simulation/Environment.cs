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
            int fertile = 0;
            int check = 0;
            Population.ForEach(p =>
            {
                if (p.Age == p.NaturalDeathAge)
                {
                    p.IsAlive = false;

                    if (p.DeathCause == DeathStatus.NaturalSavedByScreening)
                    {
                        Stats.UpdateStats(StatsType.CancerMortality, CurrentDate, p.Age, p.Sex);
                    }
                }
                

                if (p.IsAlive)
                {

                    if (p.Age >= Params.LowerestFertilityAge
                    && p.Age <= Params.HighestFertilityAge
                    && p.Sex == PersonSex.Female)
                    {
                        fertile++;
                    }

                    if (p.Age == p.IncidenceAge)
                    {
                        p.CurrentCancer = new Cancer(p);
                        Stats.UpdateStats(StatsType.Inicdence, CurrentDate, p.Age,p.Sex);
                    }

                    if (CurrentDate >= Environment.Params.ScreeningDate
                        && ((CurrentDate - Environment.Params.ScreeningDate) % Environment.Params.Freqency) == 0
                        && CheckScreening(p))
                    {
                        Screening.ScreenPerson(p);
                    }
                }

                if (p.IsAlive)
                {
                    Stats.UpdateStats(StatsType.AgeDistributions, CurrentDate, p.Age, p.Sex);

                    if (p.IncidenceAge == -1 || p.IncidenceAge > p.Age)
                    {
                        Stats.UpdateStats(StatsType.AtRisk, CurrentDate, p.Age, p.Sex);
                    }
                    else
                    {
                        check++;
                    }

                    if (p.Age == p.CancerDeathAge)
                    {
                        if (p.DeathCause == DeathStatus.Cancer)
                        {
                            Stats.UpdateStats(StatsType.CancerMortality, CurrentDate, p.Age, p.Sex);
                            Stats.UpdateStats(StatsType.CancerScreeningMortality, CurrentDate, p.Age, p.Sex);
                        }

                    }
                    
                }


            });

            Population.AddRange(NewPeople(fertile));

        }


        static bool CheckScreening(Person p)
        {
            if (!(p.CurrentCancer != null && p.CurrentCancer.ScreeningAge != -1 )&&
                p.Age >= Environment.Params.StartAge &&
                p.Age <= Environment.Params.FinishAge &&
                Tech.NextDouble(true)< Environment.Params.ParticipationRate)
                return true;
            return false;
        }

        public static List<Person> NewPeople(int fertile)
        {

            var li = new List<Person>();

            Tech.ResetRandom();
            var prev = Stats.Stats[StatsType.AgeDistributions][CurrentDate][1];
            var newBorn = fertile * Params.MeanBornProbabilityPerYear;
            newBorn = Math.Round(newBorn + (0.5 - Tech.NextDouble(false)) * 0.03 * newBorn);

            newBorn = 0.8 * prev + 0.2*newBorn;

            for (int i = 0; i < newBorn; i++)
            {
                li.Add(Demo.GenerateAgeAndGender(Params, MaxID+1+i,true));
            }

            return li;
        }


    }
}
