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
    class ObjectiveFunctionMortLH
    {
        int _size = 2;
        Parameters savedParams;


        public ObjectiveFunctionMortLH(int size)
        {
            _size = size;
            savedParams = Environment.Params.Clone();
        }

        public double F(Vector<double> x)
        {

            var xl = x.ToList();

            AdjustParamsMort.ToParams(xl);

            return CalcF();
        }


        private List<Person> ApplyParameters(List<Person> cancered)
        {

            foreach (var p in cancered)
            {
                p.CurrentCancer.CalculateHistory(p);
            }

            return cancered;
        }

        private List<Person> GetMortalityDistribution(List<Person> cancered)
        {

            var allDeaths = cancered
                .Select(a => (double)(new double[]{a.NaturalDeathAge + a.DateBirth,a.CancerDeathAge + a.DateBirth}).Min())
                .ToList();

            var cancerDeaths = cancered.Where(a => a.CancerDeathAge>a.NaturalDeathAge)
                .Select(a => (double)(a.CancerDeathAge + a.DateBirth))
                .Where(a => a <= 25)
                .GroupBy(x => x)
                .Select(x => new KeyValuePair<double, double>(x.Key, x.Count()))
                .ToDictionary(x => x.Key, x => x.Value);
          

            var canceredAtRisk = AdjustParamsMort.GetAtRisk(allDeaths);



            return cancered;
        }

        private double CalcF()
        {

            var cancered = AdjustParamsMort.cancered;
            cancered = ApplyParameters(cancered);
            var distr = GetMortalityDistribution(cancered);


            var L = new List<double>();

            var len = new double[] {100.0, Environment.Params.TrainIncidence.Length};

            for (int i = 0; i < len.Min(); i++)
            {
                
 
                var inc = Convert.ToDouble(Environment.Params.TrainIncidence[i]);
                var h = Environment.Params.DiagnoseHazard.GetValue((double) i);

                L.Add(inc * Math.Log(h) + (1 - inc) * Math.Log(1 - h));
            }

            SplashUtility<Waitbar>.DrawPlot(L.ToArray());
            SplashUtility<Waitbar>.SetStatusText("Error function value: " + Math.Round(100*L.Sum(),4).ToString());


            return -L.Sum();
        }




        
    }
}
