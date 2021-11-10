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
    class ObjectiveFunctionMale : LibOptimization.Optimization.absObjectiveFunction
    {
        int _size = 2;
        int _simYears = 30;
        int _minAge = 40;
        int _maxAge = 85;
        int _popSize = 200000;
        int _delay = 5;
        Parameters savedParams;

        public ObjectiveFunctionMale(int size)
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

            Environment.Params.YearsToSimulate = _simYears;
            Environment.Params.InitPopulation = _popSize;
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

           var minc = GetAvgStats(Environment.Stats.MStats[StatsType.Inicdence], Environment.Stats.MStats[StatsType.AtRisk]);
            var mmort = GetAvgStats(Environment.Stats.MStats[StatsType.CancerMortality], Environment.Stats.MStats[StatsType.AtRisk]);

            var F1 = Enumerable.Repeat((double)0, _maxAge-_minAge).ToList();
            var F2 = Enumerable.Repeat((double)0, _maxAge - _minAge).ToList();

            for (int i = _minAge; i < _maxAge; i++)
            {
                F1[i- _minAge] = LogDistance(Convert.ToDouble(Environment.Params.TrainData["incidence male"][i]),minc[i], Convert.ToDouble(Environment.Params.TrainData["male population"][i]));
                F2[i - _minAge] = LogDistance(Convert.ToDouble(Environment.Params.TrainData["mortality cancer male"][i]), mmort[i], Convert.ToDouble(Environment.Params.TrainData["male population"][i]));
            }

            var F = AggErrors(F1);
            F.AddRange(AggErrors(F2));

            SplashUtility<Waitbar>.DrawPlot(F.ToArray());
            SplashUtility<Waitbar>.SetStatusText("Error function value: " + Math.Round(F.Sum(),4).ToString());


            return F.Sum();
        }

        private List<double> AggErrors(List<double> F)
        {
             return F
                    .Select((s, i) => new { Value = s, Index = i })
                    .GroupBy(x => (int)(x.Index / 5))
                    .Select(grp => grp.Select(x => x.Value).Average())
                    .ToList();
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
            
            for (int i = _delay; i < vals.Count; i++)
            {
                for (int j = _minAge; j < _maxAge; j++)
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
