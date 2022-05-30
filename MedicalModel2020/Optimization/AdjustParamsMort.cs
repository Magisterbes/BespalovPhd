using System;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Optimization;
using System.Collections.Generic;
using System.Linq;

namespace MedicalModel
{
    static class AdjustParamsMort
    {

        static private List<double> paramNorms;

        static internal List<Person> cancered;
        static internal Dictionary<double,double> nonCanceredAtRisk;

        static private void GenerateSample()
        {
            Environment.CurrentDate = 0;
            Environment.Start();

            cancered = Environment.Population.Where(a => (a.CurrentCancer != null) && (a.CancerDeathAgeNoScreening != -1)).ToList();

            var nonCanceredDeaths = Environment.Population
                .Where(a => (a.CurrentCancer == null) || (a.CancerDeathAgeNoScreening ==-1))
                .Select(a => (double)(a.NaturalDeathAge + a.DateBirth))
                .ToList();

            nonCanceredAtRisk = GetAtRisk(nonCanceredDeaths);
        }

        static internal Dictionary<double, double> GetAtRisk(List<double> deathYears, int years = 25)
        {
            Dictionary<double, double> AtRisk = new Dictionary<double, double>();
            var deaths = deathYears.Where(a => a <= 25)
                .GroupBy(x => x)
                .Select(x => new KeyValuePair<double, double>(x.Key, x.Count()))
                .ToDictionary(x => x.Key, x => x.Value);

            foreach (var key in deaths.Keys)
            {
                var allDead = 0.0;
                for (int j = 1; j <= key; j++)
                {
                    allDead += deaths[(double)j];
                }

                AtRisk[key] = Environment.Params.InitPopulation - allDead;
            }

            return AtRisk;
        }

        static public void Adjust(Form1 mainForm)
        {
            GenerateSample();
            double[] initialPosition = GatherParameters();

            var alg = new NelderMeadSimplex(0.1, 500);
            var initialVec = Vector.Build.DenseOfEnumerable(initialPosition);

            var objective = new ObjectiveFunctionMortLH(initialPosition.Length);
            var objFunc = ObjectiveFunction.Value(betas => objective.F(betas));

            MinimizationResult res = null;

            try
            {
                res = alg.FindMinimum(objFunc, initialVec);

            }
            catch (Exception e)
            {
                var alg2 = new NelderMeadSimplex(1, 2000);

                res = alg2.FindMinimum(objFunc, initialVec);
            }

            if (res != null)
            {
                var xl = res.MinimizingPoint.ToArray().ToList();

                ToParams(xl);

                mainForm.AddLog(string.Format("Eval:{0}", res.FunctionInfoAtMinimum.Value));
            }
            else
            {
                ToParams(initialPosition.ToList());
                mainForm.AddLog(string.Format("Unable to find solution"));
            }

            

        }


        public static void ToParams(List<double> x)
        {

            var counter = 0;

            SetParam(ref Environment.Params.CancerDeathHazard.Constants, x, ref counter);
          //  SetParam(ref Environment.Params.growthRateLimits, x, ref counter);
          //  SetParam(ref Environment.Params.aggressivenessRateThreshold, x, ref counter);


        }


        public static void SetParam(ref double[] param, List<double> x, ref int counter)
        {
            for (int j = 0; j < param.Length; j++)
            {
                param[j] = x[counter];

                counter++;
            }
        }

        public static void SetParam(ref double param, List<double> x, ref int counter)
        {
                param = x[counter];
                counter++;
        }

        private static double[] GatherParameters()
        {
            List<double> res = new List<double>();

            res.AddRange(Environment.Params.CancerDeathHazard.Constants);
    //        res.AddRange(Environment.Params.GrowthRateLimits);
     //       res.AddRange(new double[]{Environment.Params.AggressivenessRateThreshold});

            return res.ToArray();
        }

    }
}
