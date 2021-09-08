using System;

namespace MedicalModel
{

    public abstract class Hazard
    {
        public double[] Constants;

        public void SetConstant(double[] cnt)
        {
            Constants = cnt;
        }

        public abstract double GetValue(double time, double covariates);
        public double GetValue(double time)
        {
            return GetValue(time, 0);
        }

        public abstract double GetLog(double time, double covariates);
        public double GetLog(double time)
        {
            return GetLog(time, 0);
        }

        public abstract double H0(double start, double end);
        public abstract double iH0(double value);

        public double T(double start, double covariates)
        {
            var ecov = Math.Exp(covariates);
            var tH = H0(0, start);
            var r = -Math.Log(Tech.NextDouble(false));
            var val = tH + r / ecov;
            var T = iH0(val);

            return T;
        }

    }

    public class GopmHazard: Hazard
    {
        public double L { get => Constants[0]; }
        public double B { get => Constants[1]; }


        public GopmHazard(double lambda, double beta)
        {
            this.Constants = new double[] {lambda, beta};
        }

        public override double GetValue(double time, double covariates)
        {
            return Math.Exp(L+ B* time + covariates);
        }

        public override double GetLog(double time, double covariates)
        {
            return L + B * time + covariates;
        }

        public override double H0(double start, double end)
        {
            var H0 = Math.Exp(L) / B;
            var H = H0 * (Math.Exp(B * start) - Math.Exp(B * end));

            return H;
        }

        public override double iH0(double value)
        {
            var logiH = (B * value / Math.Exp(L)) + 1;
            var iH = Math.Log(logiH) / B;

            return iH;
        }
    }


    public  class ExpHazard: Hazard
    {

        public double L { get => Constants[0]; }
        public ExpHazard(double constant)
        {
            this.Constants = new double[] { constant };
        }

        public override double GetValue(double time, double covariates)
        {
            return Math.Exp(L + covariates);
        }

        public override double GetLog(double time, double covariates)
        {
            return L + covariates;
        }

        public override double H0(double start, double end)
        {
            return (Math.Exp(L) * (end - start));
        }

        public override double iH0(double value)
        {
            return value/(Math.Exp(L));
        }


    }

    public class LogLogisticHazard : Hazard
    {

        public double O {get => Constants[0]; }
        public double k { get => Constants[1]; }

        public LogLogisticHazard(double O, double k)
        {
            this.Constants = new double[] { O, k };
        }

        public override double GetValue(double time, double covariates)
        {

            var ecov = Math.Exp(covariates);

            return Math.Exp(O)*k * Math.Pow(time* ecov, k - 1) / (1 + Math.Exp(O) * Math.Pow(time* ecov, k));
        }

        public override double GetLog(double time, double covariates)
        {
            return  Math.Log(GetValue(time,covariates));
        }

        public override double H0(double start, double end)
        {
  
            var S = 1 / (1 + Math.Exp(O) * Math.Pow(end, k));

            return -Math.Log(S);
        }

        public override double iH0(double value)
        {
            var under = (Math.Exp(value) - 1) / Math.Exp(O);

            return Math.Pow(under, 1 / k);
        }


    }

}
