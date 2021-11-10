using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalModel
{

    class Criteria
    {
        public double[,] CritLimits {get; set;}
        public int Stages = 4;
        
        public Criteria(string input)
        {
            CritLimits = new double[3,2];

            if (input.Contains("m"))
            {
                parseMalig( input);
            }
            else
            {
                parseSimple(input);
            }
        }


        public int[] GetStages(double GrowthRate, int IncidenceAge, int MalignancyAge)
        {
            var stages = Enumerable.Repeat(0, 4).ToArray();

            for (int i = 0; i < 3; i++)
            {
                var years = (int)(Math.Log(CritLimits[i, 0]) / Math.Log(GrowthRate));

                var age = IncidenceAge + years;

                if (CritLimits[i,1] == 1)
                {
                    if (MalignancyAge <= age)
                    {
                        stages[i] = age;
                    }
                    else
                    {
                        stages[i] = -1;
                    }
                }
                else
                {
                    stages[i] = age;
                }

            }

            if (stages[2] == -1)
            {
                stages[2] = MalignancyAge;
            }

            for (int i = 1; i >=0; i--)
            {
                if (stages[i] == -1)
                {
                    stages[i] = stages[i+1];
                }
            }


            return stages;

        }


        private void parseMalig(string input)
        {
            input.Split(',').Select((x, i) => new
            {
                item = x,
                index = i
            })
            .ToList()
            .ForEach(obj =>
            {
                string a = obj.item;
                int i = obj.index;

                a = a.Trim();
                if (a.Contains("n"))
                {
                    this.CritLimits[i,1] = 0;
                }
                else if (a.Contains("m"))
                {
                    this.CritLimits[i, 1] = 1;
                }
                else
                {
                    this.CritLimits[i, 1] = 0;
                }

                a = new string(a.Take(a.Length - 1).ToArray());

                this.CritLimits[i, 0] = Convert.ToDouble(a);

            });
        }

        private void parseSimple(string input)
        {
            input.Split(',').Select((x, i) => new
            {
                item = x,
                index = i
            })
            .ToList()
            .ForEach(obj =>
            {
                string a = obj.item;
                int i = obj.index;
                a = a.Trim();

                this.CritLimits[i, 1] = 0;
                this.CritLimits[i, 0] = Convert.ToDouble(a);

            });
        }

    }



}
