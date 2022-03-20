using System;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Optimization;
using System.Collections.Generic;
using System.Linq;

namespace MedicalModel
{
    static class AdjustParamsDiag
    {

        static private List<double> paramNorms;



        static public void Adjust(Form1 mainForm)
        {
            double[] initialPosition = GatherParameters();

            var alg = new NelderMeadSimplex(1, 2000);
            var initialVec = Vector.Build.DenseOfEnumerable(initialPosition);

            var objective = new ObjectiveFunctionDiagLH(initialPosition.Length);
            var objFunc = ObjectiveFunction.Value(betas => objective.F(betas));

            MinimizationResult res = null;

            try
            {
                res = alg.FindMinimum(objFunc, initialVec);

            }
            catch (Exception e)
            {
                var alg2 = new NelderMeadSimplex(1, 30000);

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


        public static double[] ReverseNorm(double[] x)
        {
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = x[i] * paramNorms[i];
            }

            return x;

        }

        public static void ToParams(List<double> x)
        {

            var counter = 0;

            SetParam(ref Environment.Params.DiagnoseHazard.Constants, x, ref counter);
            Environment.Params.DiagnoseHazard.UpdateHazard();

        }


        public static void SetParam(ref double[] param, List<double> x, ref int counter)
        {
            for (int j = 0; j < param.Length; j++)
            {
                param[j] = x[counter];

                counter++;
            }
        }

        private static double[] GatherParameters()
        {
            List<double> res = new List<double>();

            res.AddRange(Environment.Params.DiagnoseHazard.Constants);

            // paramNorms = res.Select(a => a * 2).ToList();

            //res = Enumerable.Repeat(0.5, paramNorms.Count).ToList();
            return res.ToArray();
        }

    }
}
