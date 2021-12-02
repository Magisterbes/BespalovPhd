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
    class ObjectiveFunctionLH: LibOptimization.Optimization.absObjectiveFunction
    {
        int _size = 2;
        int _simYears = 30;
        int _minAge = 0;
        int _maxAge = 85;
        int _popSize = 500000;
        int _delay = 5;
        Parameters savedParams;

        public ObjectiveFunctionLH(int size)
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

            var minc = GetAvgStats(Environment.Stats.Stats[StatsType.Diagnosis], Environment.Stats.Stats[StatsType.AgeDistributions]);
            var mmort = GetAvgStats(Environment.Stats.Stats[StatsType.CancerMortality], Environment.Stats.Stats[StatsType.AtRisk]);

            var F1 = Enumerable.Repeat((double)0, _maxAge-_minAge).ToList();
            var F2 = Enumerable.Repeat((double)0, _maxAge - _minAge).ToList();

            for (int i = _minAge; i < _maxAge-5; i++)
            {
                var inc = 0.0;
                var minc_val = new List<double>();
                var mmort_val = new List<double>();
                var mort = 0.0;
                var pop = 0.0;
                for (int j = 0; j < 5; j++)
                {
                    inc += Convert.ToDouble(Environment.Params.TrainData["incidence"][i + j]);
                    mort += Convert.ToDouble(Environment.Params.TrainData["mortality cancer"][i + j]);
                    pop += Convert.ToDouble(Environment.Params.TrainData["population"][i + j]);
                    minc_val.Add(minc[i + j]);
                    mmort_val.Add(mmort[i + j]);
                }

                var minci = minc_val.Average();
                var mmorti = mmort_val.Average();


                if (minci > 0)
                {
                    F1[i - _minAge] =  -(inc / pop) * Math.Log(minci) - (1- (inc / pop)) * Math.Log(1- minci);
                }
                else
                {
                    F1[i - _minAge] = -(inc / pop) * Math.Log(1 / pop) - (1 - (inc / pop)) * Math.Log(1 - minci);
                }

                if (mmorti > 0)
                {
                    F2[i - _minAge] = -(mort / pop) * Math.Log(mmorti) - (1 - (mort / pop)) * Math.Log(1 - mmorti);
                }
                else 
                {
                    F2[i - _minAge] = -(mort / pop) * Math.Log(1 / pop)  - (1 - (mort / pop)) * Math.Log(1 - mmorti);
                }
            }

            var F = F1;
            F.AddRange(F2);

            SplashUtility<Waitbar>.DrawPlot(F.ToArray());
            SplashUtility<Waitbar>.SetStatusText("Error function value: " + Math.Round(100*F.Sum(),4).ToString());


            return 100*F.Sum();
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

            SetParam(ref Environment.Params.GrowthRateDistribution.Coefs, xx, ref counter);
            SetParam(ref Environment.Params.IncidenceHazard.Constants, xx, ref counter);
            Environment.Params.IncidenceHazard.UpdateHazard();
            SetParam(ref Environment.Params.DiagnoseHazard.Constants, xx, ref counter);
            Environment.Params.DiagnoseHazard.UpdateHazard();
            SetParam(ref Environment.Params.MalignancyHazard.Constants, xx, ref counter);
            Environment.Params.MalignancyHazard.UpdateHazard();
            SetParam(ref Environment.Params.CancerDeathHazard.Constants, xx, ref counter);
            Environment.Params.CancerDeathHazard.UpdateHazard();



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
