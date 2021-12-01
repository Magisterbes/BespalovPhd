using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace MedicalModel
{
    static class Tech
    {
        public static Random Rnd = new Random(DateTime.Now.Millisecond);

        public static void Setup()
        {

            var testIdx = Environment.Params.TestParameters.ToList().IndexOf(Environment.Params.SelectedTest);
            Screening.TP = Environment.Params.TestTP[testIdx];
            Screening.FP = Environment.Params.TestFP[testIdx];


        }


        public static double[] CutByZero(double[] input)
        {
            var ldata = new List<double>();

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == 0)
                {
                    break;
                }
                else
                {
                    ldata.Add(input[i]);
                }
            }

            return ldata.ToArray();
        }
        public static double ExpSample(Exponential exp)
        {
            return Math.Round(exp.Sample());
        }


        public static double Heaviside(double input)
        {
            return input >= 0 ? 1 : 0;
        }

        public static double Heaviside(double input, double delay)
        {
            return input >= delay ? 1 : 0;
        }


        public static int CheckByIntDistribution(int[] distribution, int max)
        {
            var rand = Rnd.Next(max);
            for (int i = 0; i < distribution.Length; i++)
            {
                rand = rand - distribution[i];

                if (rand <= 0)
                {
                    return i;
                }
            }

            return -1;
        }

        public static int CheckByFloatDistribution(double[] distribution)
        {
            return CheckByFloatDistribution(distribution, 1);
        }

        public static int CheckByFloatDistribution(double[] distribution, int max)
        {
            var rand = (double)Rnd.Next(max);
            for (int i = 0; i < distribution.Length; i++)
            {
                rand = rand - distribution[i];

                if (rand <= 0)
                {
                    return i;
                }
            }

            return -1;
        }

        private static object locker = new object();


        public static void ResetRandom()
        {
            Rnd = new Random();
        }

        public static double NextDouble(bool Parallel)
        {
            if (Parallel)
            {
                double output;

                lock (locker)
                {
                    output = Rnd.NextDouble();
                }

                return output;

            }
            else
            {
                return Rnd.NextDouble();
            }
           
        }

        public static int CheckByProb(double prob)
        {
            var rand = Rnd.NextDouble();

            if (rand > prob)
                return 0;
            else
                return 1;
        }


        public static void writeinfile(int[,] data)
        {
            using (TextWriter tw = new StreamWriter("c:\\matrix.csv", true))
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int i = 0; i < 24; i++)
                    {
                        if (i != 0)
                        {
                            tw.Write("; ");
                        }
                        tw.Write(data[j, i]);
                    }
                    tw.Write(" \n");
                }

                tw.Write(" \n\n");
            }
        }


        public static void writeinfile(double[,] data, int firstS, int firstE, int secondS, int secondE, string name)
        {
            using (TextWriter tw = new StreamWriter("c:\\" +name+ ".csv", true))
            {
                for (int j = firstS; j < firstE; j++)
                {
                    for (int i = secondS; i < secondE; i++)
                    {
                        if (i != 0)
                        {
                            tw.Write("; ");
                        }
                        tw.Write(data[j, i]);
                    }
                    tw.Write(" \n");
                }

                tw.Write(" \n\n");
            }
        }


        public static void writeinfile(double[,] data, int first, int second)
        {
            using (TextWriter tw = new StreamWriter("c:\\matrix" + DateTime.Now.ToShortDateString().Replace('.', '_') + DateTime.Now.ToLongTimeString().Replace(':', '_') + ".csv", true))
            {
                for (int j = 0; j < first; j++)
                {
                    for (int i = 0; i < second; i++)
                    {
                        if (i != 0)
                        {
                            tw.Write("; ");
                        }
                        tw.Write(data[j, i]);
                    }
                    tw.Write(" \n");
                }

                tw.Write(" \n\n");
            }
        }


        public static void writeinfilearray(int[] data, string name )
        {

            using (TextWriter tw = new StreamWriter("c:\\" + name + ".csv", true))
            {
                for (int i = 0; i < data.Length; i++)
                {
                    if (i != 0)
                    {
                        tw.Write("; ");
                    }
                    tw.Write(data[i]);
                }
                tw.Write(" \n\n");
            }

        }


        public static void writeinfilearrayvert(int[] data, string name)
        {

            using (TextWriter tw = new StreamWriter("c:\\" + name + ".csv", true))
            {
                for (int i = 0; i < data.Length; i++)
                {
                    if (i != 0)
                    {
                        tw.Write("\n ");
                    }
                    tw.Write(data[i]);
                }
                tw.Write(" \n\n");
            }

        }

        public static void writeinfilearray(double[] data, string name)
        {

            using (TextWriter tw = new StreamWriter("c:\\" + name + ".csv", true))
            {
                for (int i = 0; i < data.Length; i++)
                {
                    if (i != 0)
                    {
                        tw.Write("; ");
                    }
                    tw.Write(data[i]);
                }
                tw.Write(" \n\n");
            }

        }


        public static void writeStrInFile(string Info, string name)
        {

            using (TextWriter tw = new StreamWriter("c:\\" + name + ".csv", true))
            {
                tw.WriteLine(Info);
            }

        }

    }
}
