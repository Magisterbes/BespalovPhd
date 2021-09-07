using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;
using System.Windows.Forms;

namespace MedicalModel
{
    class ObjectiveFunction : LibOptimization.Optimization.absObjectiveFunction
    {
        public ObjectiveFunction()
        {
        }

        public override double F(List<double> x)
        {
            var ret = 0.0;
            var dim = this.NumberOfVariable(); //or x.Count
            for (int i = 0; i < dim; i++)
            {
                ret += x[i] * x[i];// x^2
            }
            return ret;
        }

        public override List<double> Gradient(List<double> x)
        {
            return null;
        }

        public override List<List<double>> Hessian(List<double> x)
        {
               return null;
        }
        public override int NumberOfVariable()
        {
            return 2;
        }
    }
}
