using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;
using System.Windows.Forms;
using LibOptimization.Optimization;
using LibOptimization.Util;

namespace MedicalModel
{
    static class AdjustParams
    {

        static private List<double> paramNorms;



        static public void Adjust(Form1 mainForm)
        {
            double[] initialPosition = GatherParameters();

            var func = new ObjectiveFunction(initialPosition.Length);
            var opt = new clsOptSimulatedAnnealing(func);

            opt.InitialPosition = initialPosition;

            opt.Temperature = 10;
            opt.StopTemperature = 0.01;
            opt.CoolingRatio = 0.95;
            opt.IsUseCriterion = false;
            opt.Iteration = 100;



            opt.Init();
            clsUtil.DebugValue(opt);

            while (opt.DoIteration(0) == false)
            {
                var eval = opt.Result.Eval;

                //my criterion
                if (eval < 0.02)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Eval:{0}", opt.Result.Eval);
                }
            }

            mainForm.AddLog(string.Format("Eval:{0}", opt.Result.Eval));


        }


        public static double[] ReverseNorm(double[] x)
        {
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = x[i] * paramNorms[i];
            }

            return x;

        }


        private static double[] GatherParameters()
        {
            List<double> res = new List<double>();

            res.AddRange(Environment.Params.GrowthRateDistribution.Coefs);
            res.AddRange(Environment.Params.IncidenceHazard.Constants);
            res.AddRange(Environment.Params.DiagnoseHazard.Constants);
            res.AddRange(Environment.Params.MalignancyHazard.Constants);
            res.AddRange(Environment.Params.CancerDeathHazard.Constants);

            

            paramNorms = res.Select(a => a * 2).ToList();

            res = Enumerable.Repeat(0.5, paramNorms.Count).ToList();
            return res.ToArray();
        }

    }
}
