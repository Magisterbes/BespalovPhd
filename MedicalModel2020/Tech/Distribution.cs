using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalModel
{

    enum DistributionInputType
    {
        CDF,
        PDF
    }

    class Distiribution
    {

        double[] CDF;
        double[] PDF;
        public double NormalizationCoef;  

        public Distiribution(double[] input, DistributionInputType dtype)
        {
            if (dtype == DistributionInputType.CDF)
            {
                NormalizationCoef = input.Max();
                CDF = input.Select(a => a / NormalizationCoef).ToArray();

                var temp = CDF.Zip(CDF.Skip(1), (a, b) => b - a).ToList();
                temp.Insert(0, CDF[0]);
                PDF = temp.ToArray();

            }
            else
            {
                NormalizationCoef = input.Sum();
                PDF = input.Select(a => a / NormalizationCoef).ToArray();

                double sum = 0;

                CDF = PDF.Select(w => sum += w).ToArray();
            }
        }

        public int GenerateRandom()
        {
            var rand = Tech.NextDouble(false);

            for (int i = 0; i < CDF.Length; i++)
            {
                if (rand < CDF[i])
                    return i-1;
            }

            return -1;
            // return BinarySearchIterative(this.CDF, rand);
        }

        private int BinarySearchIterative(double[] inputArray, double key)
        {
            int min = 0;
            int max = inputArray.Length - 1;
            while (min <= max)
            {
                int mid = (min + max) / 2;
                if (max-min ==1 || max == min)
                {
                    if (max == min)
                        return min;

                    if (Math.Abs(key - inputArray[min]) < Math.Abs(key - inputArray[max]))
                        return min;
                    else
                        return max;
                }
                else if (key < inputArray[mid])
                {
                    max = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }
            }
            return -1;
        }

    }
}
