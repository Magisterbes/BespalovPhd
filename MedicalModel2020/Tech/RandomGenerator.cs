using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace MedicalModel
{


    class RandomGenerator
    {
        public double[] Coefs;
        public delegate double GenerateRandomDelegate();
        public GenerateRandomDelegate GenerateRandom;
        public delegate double[] SampleDelegate(int size);
        public SampleDelegate Sample;
        private LogNormal lnorm;
        private Exponential exp;
        private Normal norm;


        public RandomGenerator(double[] coefs, string distributionType)
        {
            Coefs = coefs;

            if (distributionType == "lognorm")
            {
                lnorm = new LogNormal(coefs[0], coefs[1]);
                GenerateRandom = GenerateLogNormal;
                Sample = SampleLogNormal;
            }
            else if (distributionType == "norm")
            {
                norm = new Normal(coefs[0], coefs[1]);
                GenerateRandom = GenerateLogNormal;
                Sample = SampleLogNormal;
            }
            else if (distributionType == "exp")
            {
                exp = new Exponential(coefs[0]);
                GenerateRandom = GenerateExp;
                Sample = SampleLogNormal;
            }

        }
        public double GenerateLogNormal()
        {
            return lnorm.Sample();
        }

        public double[] SampleLogNormal(int size)
        {
            var res = new double[size];
            lnorm.Samples(res);
            return res;
        }

        public double GenerateNormal()
        {
            return norm.Sample();
        }

        public double[] SampleNormal(int size)
        {
            var res = new double[size];
            norm.Samples(res);
            return res;
        }

        public double GenerateExp()
        {
            return exp.Sample();
        }

        public double[] SampleExp(int size)
        {
            var res = new double[size];
            exp.Samples(res);
            return res;
        }
    }

}
