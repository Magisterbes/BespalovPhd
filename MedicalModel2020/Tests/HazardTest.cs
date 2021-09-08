using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalModel.Tests
{
    static class HazardTest
    {

        public static double[] Test(Hazard hz, Form1 frm)
        {
            List<double> vals = new List<double>();
            List<double> fun = new List<double>();
            List<double> x = new List<double>();


            for (int i = 0; i < 100000; i++)
            {
                var vl = Math.Round(hz.T(0, 0));
                vals.Add(vl);
            }

            for (int i = 0; i < 100; i++)
            {
                fun.Add(hz.GetValue(i));
                x.Add(i);
            }

            var hd = GetHazardDistribution(vals.ToArray());
            var resid = new List<double>();
            for (int i = 0; i < 100; i++)
            {
                resid.Add(hd[i] - fun[i]);
            }

            frm.ChartTest("Calculated", fun.ToArray(), x.ToArray());
            frm.ChartTest("Esimated", hd, x.ToArray());

            return resid.ToArray();
        }

        static double[] GetHazardDistribution(double[] values)
        {
            var set = values.Distinct().ToList();
            set.Sort();

            double[] res = new double[100];

            foreach (var item in set)
            {
                if (item > 99)
                    continue;
                var eq = values.Where(a => a == item).Count();
                var ge = values.Where(a => a >= item).Count();
                List<double> r = new List<double>();
                res[(int)item] = (double)eq / (double)ge;

            }


            return res;
        }

    }
}
