using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Optimization;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra;

namespace MedicalModel
{
    class ObjectiveFunctionDiagLH
    {
        int _size = 2;
        Parameters savedParams;


        public ObjectiveFunctionDiagLH(int size)
        {
            _size = size;
            savedParams = Environment.Params.Clone();
        }

        public double F(Vector<double> x)
        {

            var xl = x.ToList();

            AdjustParamsDiag.ToParams(xl);

            return CalcF();
        }


        private double CalcF()
        {

            var L = new List<double>();

            var len = new double[] {100.0, Environment.Params.TrainIncidence.Length};

            for (int i = 0; i < len.Min(); i++)
            {
                
 
                var inc = Convert.ToDouble(Environment.Params.TrainIncidence[i]);
                var h = Environment.Params.DiagnoseHazard.GetValue((double) i);

                L.Add(inc * Math.Log(h) + (1 - inc) * Math.Log(1 - h));
            }


            //SplashUtility<Waitbar>.DrawPlot(L.ToArray());
            //SplashUtility<Waitbar>.SetStatusText("Error function value: " + Math.Round(100*L.Sum(),4).ToString());


            return -L.Sum();
        }




        
    }
}
