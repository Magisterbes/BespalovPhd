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
        int _size = 2;
        Parameters savedParams;

        public ObjectiveFunction(int size)
        {
            _size = size;
            savedParams = Environment.Params.Clone();
        }

        public override double F(List<double> x)
        {

            foreach (var xx in x)
            {
                if (xx<= 0)
                {
                    return double.MaxValue;
                }
            }

            ToParams(x);

            Environment.Params.YearsToSimulate = 50;
            Environment.Params.InitPopulation = 200000;
            Environment.Params.ScreeningDate = 100;

            Environment.Start();

            for (int i = 0; i < Environment.Params.YearsToSimulate - 1; i++)
            {
                Environment.CurrentDate++;
            }


            Environment.Params.YearsToSimulate = savedParams.YearsToSimulate;
            Environment.Params.InitPopulation = savedParams.InitPopulation;
            Environment.Params.ScreeningDate = savedParams.ScreeningDate;


            return CalcF();
        }


        private double CalcF()
        {

            var finc = GetAvgStats(Environment.Stats.FStats[StatsType.Inicdence], Environment.Stats.FStats[StatsType.AtRisk]);
            var fmort = GetAvgStats(Environment.Stats.FStats[StatsType.CancerMortality], Environment.Stats.FStats[StatsType.AtRisk]);
            var minc = GetAvgStats(Environment.Stats.MStats[StatsType.Inicdence], Environment.Stats.MStats[StatsType.AtRisk]);
            var mmort = GetAvgStats(Environment.Stats.MStats[StatsType.CancerMortality], Environment.Stats.MStats[StatsType.AtRisk]);

            var F = Enumerable.Repeat((double)0,50).ToList();
            var delay = 30;
            for (int i = delay; i < 80; i++)
            {
                F[i-delay] += LogDistance(Convert.ToDouble(Environment.Params.TrainData["incidence male"][i]),minc[i], Convert.ToDouble(Environment.Params.TrainData["male population"][i]));
                F[i - delay] += LogDistance(Convert.ToDouble(Environment.Params.TrainData["incidence female"][i]), finc[i], Convert.ToDouble(Environment.Params.TrainData["female population"][i]));
                F[i - delay] += LogDistance(Convert.ToDouble(Environment.Params.TrainData["mortality cancer female"][i]), fmort[i], Convert.ToDouble(Environment.Params.TrainData["female population"][i]));
                F[i - delay] += LogDistance(Convert.ToDouble(Environment.Params.TrainData["mortality cancer male"][i]), mmort[i], Convert.ToDouble(Environment.Params.TrainData["male population"][i]));
            }


            ////F = F.Select(a =>
            ////{
            ////    if (a > F.Average() * 1.5)
            ////    {
            ////        return F.Average() * 1.5;
            ////    }

            ////    return a;
            ////}).ToList();

            SplashUtility<Waitbar>.DrawPlot(F.ToArray());
            SplashUtility<Waitbar>.SetStatusText("Error function value: " + Math.Round(F.Sum(),4).ToString());
            return F.Sum();
        }
        
        private double LogDistance(double a, double b, double c)
        {
            if (a == 0 || c==0)
            {
                if (b == 0)
                    return 0;
                else
                    return Math.Pow(Math.Sqrt(b), 2);
            }

            if (b ==0)
            {
                return Math.Pow(Math.Sqrt(a / c), 2);
            }


            return Math.Pow(Math.Sqrt(a/c) - Math.Sqrt(b), 2);
        }

        private double[] GetAvgStats(Dictionary<int,int[]> vals, Dictionary<int, int[]> atrisk)
        {
            var res = new List<List<double>>();
            for (int i = 0; i < Environment.Params.UnrealLifeLength + 1; i++)
            {
                res.Add(new List<double>());
            }
            
            for (int i = 25; i < vals.Count; i++)
            {
                for (int j = 30; j < 80; j++)
                {

                    if (atrisk[i][j] > 0)
                    {
                        var val = Convert.ToDouble(vals[i][j]) / Convert.ToDouble(atrisk[i][j]);

                        res[j].Add(val);
                    }
                    else
                    {
                        res[j].Add(0);
                    }
                }
            }



            var final = res.Select(a => {

                if (a.Count > 0)
                    return a.Average();
                else
                    return 0;

            }).ToArray();

            return final;
        }


        private void SetParam(ref double[] param, List<double> x, ref int counter)
        {
            for (int j = 0; j < param.Length; j++)
            {
                param[j] = x[counter];

                counter++;
            }
        }

        private void ToParams(List<double> x)
        {

            var xx = AdjustParams.ReverseNorm(x.ToArray()).ToList();

            var counter = 0;
            SetParam(ref Environment.Params.GrowthRateDistributionFemale.Coefs, xx, ref counter);
            SetParam(ref Environment.Params.IncidenceHazardFemale.Constants, xx, ref counter);
            SetParam(ref Environment.Params.DiagnoseHazardFemale.Constants, xx, ref counter);
            SetParam(ref Environment.Params.MalignancyHazardFemale.Constants, xx, ref counter);
            SetParam(ref Environment.Params.CancerDeathHazardFemale.Constants, xx, ref counter);

            SetParam(ref Environment.Params.GrowthRateDistributionMale.Coefs, xx, ref counter);
            SetParam(ref Environment.Params.IncidenceHazardMale.Constants, xx, ref counter);
            SetParam(ref Environment.Params.DiagnoseHazardMale.Constants, xx, ref counter);
            SetParam(ref Environment.Params.MalignancyHazardMale.Constants, xx, ref counter);
            SetParam(ref Environment.Params.CancerDeathHazardMale.Constants, xx, ref counter);



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
            return _size;
        }
    }
}
