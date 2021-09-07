using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalModel
{

    enum HazardType
    {
        exp,
        gomp
    }

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

    }

    public class GopmHazard: Hazard
    {
        public GopmHazard(double lambda, double beta)
        {
            this.Constants = new double[] {lambda, beta};
        }

        public override double GetValue(double time, double covariates)
        {
            return Math.Exp(this.Constants[0] + this.Constants[1] * time + covariates);
        }

        public override double GetLog(double time, double covariates)
        {
            return this.Constants[0] + this.Constants[1] * time + covariates;
        }



    }

    public  class ExpHazard: Hazard
    {
        public ExpHazard(double constant)
        {
            this.Constants = new double[] { constant };
        }

        public override double GetValue(double time, double covariates)
        {
            return Math.Exp(this.Constants[0] + covariates);
        }

        public override double GetLog(double time, double covariates)
        {
            return this.Constants[0] + covariates;
        }


    }



}
