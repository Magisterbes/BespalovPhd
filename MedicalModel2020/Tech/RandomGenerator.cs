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
        private Weibull weibull;
        private ContinuousUniform uniform;


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
                GenerateRandom = GenerateNormal;
                Sample = SampleNormal;
            }
            else if (distributionType == "exp")
            {
                exp = new Exponential(coefs[0]);
                GenerateRandom = GenerateExp;
                Sample = SampleExp;
            }
            else if (distributionType == "uniform")
            {
                uniform = new ContinuousUniform(coefs[0],coefs[1]);
                GenerateRandom = GenerateUn;
                Sample = SampleUn;
            }
            else if (distributionType == "weibull")
            {
                weibull = new Weibull(coefs[0], coefs[1]);
                GenerateRandom = GenerateWe;
                Sample = SampleWe;
            }

        }

        private double GenerateLogNormal()
        {
            return lnorm.Sample();
        }

        private double[] SampleLogNormal(int size)
        {
            var res = new double[size];
            lnorm.Samples(res);
            return res;
        }

        private double GenerateNormal()
        {
            return norm.Sample();
        }

        private double[] SampleNormal(int size)
        {
            var res = new double[size];
            norm.Samples(res);
            return res;
        }

        private double GenerateExp()
        {
            return exp.Sample();
        }

        private double[] SampleExp(int size)
        {
            var res = new double[size];
            exp.Samples(res);
            return res;
        }

        private double GenerateUn()
        {
            return uniform.Sample();
        }

        private double[] SampleUn(int size)
        {
            var res = new double[size];
            uniform.Samples(res);
            return res;
        }

        private double GenerateWe()
        {
            return weibull.Sample();
        }

        private double[] SampleWe(int size)
        {
            var res = new double[size];
            weibull.Samples(res);
            return res;
        }
    }

}
