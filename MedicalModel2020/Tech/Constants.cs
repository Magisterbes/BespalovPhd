﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Data.Analysis;
using MathNet.Numerics.Distributions;
using MathNet.Numerics;
using MathNet.Numerics.LinearRegression;

namespace MedicalModel
{

    class Parameters
    {
        private int yearsToSimulate = 60;
        private int initPopulation = 500000;
        private int unrealLifeLength = 110;
        private string trainDataFilename = "train_set.csv";
        private string trainStagingDataFilename = "staging_train_set.csv";

        private int stageDistributionLength = 4;

        public DataFrame TrainData;

        private Distribution initAgeDist;
        private Distribution aging;

        private double[] trainIncidence;
        private double[] trainMortality;
        private double[][] stagingRegression;
        private RandomGenerator timeBeforeDiagnosis;
        private double[] timeBeforeDiagnosisStageShift;
        //private Hazard diagnosisSizeHazard;
        // private Hazard malignancyHazard;

        private Hazard diagnoseHazard;
        private Hazard сancerDeathHazard;
        //private RandomGenerator growthRateDistribution;

        private double[] stageCriteria;
        private double reoccurrenceProbability;
        private double treatmentMortalityAdjustment;
        private double[] proportionOfAggressive;
        private double[] stageDistirbution;
        internal double[] growthRateLimits;
        internal double aggressivenessRateThreshold;


        private double[] treatmentEfficiency = new double[] { 0.75, 0.85, 0.95 };
        private double[] complications = new double[] { 0.1 };
        private double[] ageCureConstants = new double[] { 1.062, 1.0601, 1.058, 1.0557, 1.0532, 1.0505, 1.0476, 1.0445, 1.0412, 1.0377, 1.034, 1.0301, 1.026, 1.0217, 1.0172, 1.0125, 1.0076, 1.0025, 0.9972, 0.9917, 0.9860, 0.9801, 0.9740, 0.9677, 0.9612, 0.9545, 0.9476, 0.9405, 0.9332, 0.9257, 0.9180, 0.9101, 0.9020, 0.8937, 0.8852, 0.8765, 0.8676, 0.8585, 0.8492, 0.8397, 0.8300, 0.8201, 0.8100, 0.7997, 0.7892, 0.7785, 0.7676, 0.7565, 0.7452, 0.7337, 0.7220, 0.7101, 0.6980, 0.6857, 0.6732, 0.6605, 0.6476, 0.6345, 0.6212, 0.6077, 0.5940, 0.5801, 0.5660, 0.5517, 0.5372, 0.5225, 0.5076, 0.4925, 0.4772, 0.4617, 0.4460, 0.4301, 0.4140, 0.3977, 0.3812, 0.3645, 0.3476, 0.3305, 0.3132, 0.2957, 0.2780, 0.2601, 0.2420, 0.2237, 0.2052, 0.1865, 0.1676, 0.1485, 0.1292, 0.1097, 0.09001, 0.07011, 0.05000, 0.0296999999999998, 0.009210, -0.01151, -0.03240, -0.0534999999999999, -0.07480, -0.09631, -0.1180, -0.1399, -0.1620, -0.1843, -0.2068, -0.2295, -0.2524, -0.2755, -0.2988, -0.3223, -0.3460, -0.3699, -0.3940, -0.4183, -0.4428, -0.4675, -0.4924, -0.5175, -0.5428, -0.5683, -0.5940 };

        private List<Factor> factors = new List<Factor>();
        private double[] factorAgeCorrection = { 0.1, 2 };

        private int screeningDate = 30;
        private int startAge = 50;
        private int finishAge = 60;
        private int freqency = 2;
        private int selectedTest = 70;
        private double participationRate = 0.7;


        private double[] testParameters = { 50, 60, 70, 80 };
        private double[] testTP = { 0.6, 0.7, 0.9, 0.95 };
        private double[] testFP = { 0.05, 0.07, 0.1, 0.15 };


        private double testPerPersonPrice = 1;
        private double screeningPrice = 1;
        private double[] stageTreatementPrice = { 1, 2, 3, 4 };
        private double complicationsPrice = 1;
        private double reoccurrenceCost = 1;
        private double[][] stageByAgeRegression;
        private Dictionary<int, Multinomial> stageByAgeGenerator;
        private Dictionary<int, Multinomial> stageByAgeRegGenerator;
        private double[] progressionRegression;

        public int YearsToSimulate { get => yearsToSimulate; set => yearsToSimulate = value; }
        public int InitPopulation { get => initPopulation; set => initPopulation = value; }
         public int UnrealLifeLength { get => unrealLifeLength; set => unrealLifeLength = value; }
        public string TrainDataFilename { get => trainDataFilename; set => trainDataFilename = value; }
        public string TrainStagingDataFilename { get => trainStagingDataFilename; set => trainStagingDataFilename = value; }

        public double[] TrainIncidence { get => trainIncidence; set => trainIncidence = value; }
        public double[] TrainMortality{ get => trainMortality; set => trainMortality = value; }


        
        public double[][] StagingRegression { get => stagingRegression; set => stagingRegression = value; }

        public double[][] StageByAgeRegression { get => stageByAgeRegression; set => stageByAgeRegression = value; }

        public Dictionary<int, Multinomial> StageByAgeRegGenerator { get => stageByAgeRegGenerator; set => stageByAgeRegGenerator = value; }
        public Dictionary<int,Multinomial> StageByAgeGenerator { get => stageByAgeGenerator; set => stageByAgeGenerator = value; }

        public double[] ProgressionRegression { get => progressionRegression; set => progressionRegression = value; }

    public Distribution Aging { get => aging; set => aging = value; }
        public Distribution InitAgeDist { get => initAgeDist; set => initAgeDist = value; }


        // public Hazard MalignancyHazard { get => malignancyHazard; set => malignancyHazard = value; }
        // public Hazard DiagnosisSizeHazard { get => diagnosisSizeHazard; set => diagnosisSizeHazard = value; }
        // public RandomGenerator GrowthRateDistribution { get => growthRateDistribution; set => growthRateDistribution = value; }
        public Hazard DiagnoseHazard { get => diagnoseHazard; set => diagnoseHazard = value; }
        public Hazard CancerDeathHazard { get => сancerDeathHazard; set => сancerDeathHazard = value; }
        public RandomGenerator TimeBeforeDiagnosis { get => timeBeforeDiagnosis; set => timeBeforeDiagnosis = value; }

        public double[] TimeBeforeDiagnosisStageShift { get => timeBeforeDiagnosisStageShift; set => timeBeforeDiagnosisStageShift = value; }

        public double[] StageCriteria { get => stageCriteria; set => stageCriteria = value; }
        public double ReoccurrenceProbability { get => reoccurrenceProbability; set => reoccurrenceProbability = value; }
        public double TreatmentMortalityAdjustment { get => treatmentMortalityAdjustment; set => treatmentMortalityAdjustment = value; }
        public double[] ProportionOfAggressive { get => proportionOfAggressive; set => proportionOfAggressive = value; }

        public double[] StageDistirbution { get => stageDistirbution; set => stageDistirbution = value; }

        public double[] GrowthRateLimits { get => growthRateLimits; set => growthRateLimits = value; }

        public double AggressivenessRateThreshold { get => aggressivenessRateThreshold; set => aggressivenessRateThreshold = value; }



        public double[] TreatmentEfficiency { get => treatmentEfficiency; set => treatmentEfficiency = value; }
        public double[] Complications { get => complications; set => complications = value; }
        public double[] AgeCureConstants { get => ageCureConstants; set => ageCureConstants = value; }
        internal List<Factor> Factors { get => factors; set => factors = value; }
        public double[] FactorAgeCorrection { get => factorAgeCorrection; set => factorAgeCorrection = value; }
        public int ScreeningDate { get => screeningDate; set => screeningDate = value; }
        public int StartAge { get => startAge; set => startAge = value; }
        public int FinishAge { get => finishAge; set => finishAge = value; }
        public int Freqency { get => freqency; set => freqency = value; }
        public int SelectedTest { get => selectedTest; set => selectedTest = value; }
        public double ParticipationRate { get => participationRate; set => participationRate = value; }
        public double[] TestParameters { get => testParameters; set => testParameters = value; }
        public double[] TestTP { get => testTP; set => testTP = value; }
        public double[] TestFP { get => testFP; set => testFP = value; }
        public double TestPerPersonPrice { get => testPerPersonPrice; set => testPerPersonPrice = value; }
        public double ScreeningPrice { get => screeningPrice; set => screeningPrice = value; }
        public double[] StageTreatementPrice { get => stageTreatementPrice; set => stageTreatementPrice = value; }
        public double ComplicationsPrice { get => complicationsPrice; set => complicationsPrice = value; }

        public double ReoccurrenceCost { get => reoccurrenceCost; set => reoccurrenceCost = value; }
        public int StageDistributionLength { get => stageDistributionLength; set => stageDistributionLength = value; }

        public double[] TBDGenerationArray { get; set; }


        public Parameters()
        {
        }


        public Parameters(string filename)
        {

            

            var lis = File.ReadAllText(filename).Split('\n').ToList();
            CultureInfo customCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            foreach (var row in lis)
            {
                if (row.Trim() == "")
                    continue;

                if (row[0] == '#')
                    continue;

                var spl = row.Split(':');

                if (spl[0] == "FactorsRR")
                {
                    var fspl = spl[1].Split(',').Select(a =>
                    {
                        var ffspl = a.Split('@');
                        return new Factor(Convert.ToDouble(ffspl[1]), ffspl[0], true);
                    }).ToList<Factor>();
                }
                else if (spl[0] == "AgeIrrelevantFactorsRR")
                {
                    var fspl = spl[1].Split(',').Select(a =>
                    {
                        var ffspl = a.Split('@');
                        return new Factor(Convert.ToDouble(ffspl[1]), ffspl[0], false);
                    }).ToList<Factor>();
                }
                else
                {
                    PropertyInfo propertyInfo = this.GetType().GetProperty(spl[0].Trim());
                    var type = propertyInfo.PropertyType.FullName;

                    if (type == "System.Double[]")
                    {
                        double[] doubles = spl[1].Split(',').Select(Double.Parse).ToArray();
                        propertyInfo.SetValue(this, doubles, null);
                    }
                    else if(type == "MedicalModel.Hazard")
                    {
                        Hazard hzd;
                        hzd = ParseHazard(spl[1]);


                        propertyInfo.SetValue(this, hzd, null);
                    }
                    else if (type == "MedicalModel.Criteria")
                    {
                        Criteria crt = new Criteria(spl[1]);

                        propertyInfo.SetValue(this, crt, null);
                    }
                    else if(type == "MedicalModel.RandomGenerator")
                    {
                        RandomGenerator gen = ParseGenerator(spl[1]);

                        propertyInfo.SetValue(this, gen, null);
                    }
                    else if (type == "MedicalModel.Distribution")
                    {
                        double[] doubles = spl[1].Split(',').Select(Double.Parse).ToArray();

                        //Three times more often
                        if(spl[0] == "InitAgeDist")
                        {
                            doubles = doubles.Select(a => a *3).ToArray();
                        }

                        if (spl[0] == "Aging")
                        {

                            doubles = FromRisksToDistribution(doubles.Select(a => a / 100000).ToArray());
                        }

                        propertyInfo.SetValue(this, new Distribution(doubles, DistributionInputType.PDF), null);
                    }
                    else
                    {
                        var convdata = Convert.ChangeType(spl[1].Trim(), Type.GetType(type));

                        propertyInfo.SetValue(this, convdata, null);
                    }
                }

            }
            InitFrames();
            LoadStagingTrainData();
            GetTBDGenerationArray();
        }

        private void GetTBDGenerationArray()
        {

            var arr = timeBeforeDiagnosis.Sample(100000);
            TBDGenerationArray = arr.OrderBy(a=>a).ToArray();

        } 

        private void LoadStagingTrainData()
        {
            Environment.LogInfo.Add("Filename: " + trainStagingDataFilename);
            var trainArray = DataFrame.LoadCsv(trainStagingDataFilename, ';');



            //stagingRegression = GetStagingRegression(trainArray);

            Environment.LogInfo.Add("Stage by Age model training.");
            stageByAgeRegression = GetStageByAgeRegression(trainArray);

            GetStageByAgeRegGenerator(stageByAgeRegression, trainArray);
            GetStageByAgeGenerator(trainArray);
            

            proportionOfAggressive =  GetAggressivenessDistribution(trainArray);


            //GetProgressionRegression(trainArray);

        }

        //private void GetProgressionRegression(DataFrame trainArray)
        //{

        //    var len = trainArray["Age"].Length;
        //    var X = new double[len][];
        //    var Y = new double[len];

        //    for (int i = 0; i < len; i++)
        //    {
        //        var tempX = new double[6] { 0, 0, 0, 0, 0, 0 };
        //        var tempY = new double[1] { 0 };

        //        tempX[0] = Convert.ToDouble(trainArray["Age"][i]);
        //        tempX[1] = Convert.ToDouble(trainArray["Aggressiveness"][i]);
        //        tempX[1 + Convert.ToInt32(trainArray["Stage"][i])] += 1;
                
        //        Y[i] = Convert.ToDouble(trainArray["TumorProgression"][i]);
        //        X[i] = tempX;
        //    }

        //    progressionRegression = Fit.MultiDim(X,Y,  intercept: true);


        //}


        

        private void GetStageByAgeGenerator(DataFrame trainArray)
        {
            stageByAgeGenerator = new Dictionary<int, Multinomial>();

            var minGr = Tech.GetAgeGroupInt(trainArray["Age"].Min());
            var maxGr = Tech.GetAgeGroupInt(trainArray["Age"].Max());

            var agrs = Enumerable.Range(0, (int)trainArray.Rows.Count)
                        .Select(x => Tech.GetAgeGroupInt(trainArray["Age"][x])).ToArray();

            PrimitiveDataFrameColumn<int> ageGroupCol = new PrimitiveDataFrameColumn<int>("AgeGroup", agrs);
            trainArray.Columns.Add(ageGroupCol);


            for (int i = 0; i < 12; i++)
            {
                double agr = i * 10;

                if (i * 10 <= minGr)
                {
                    agr = minGr;
                }

                if (i * 10 >= maxGr)
                {
                    agr = maxGr;
                }


                var farr = trainArray.Filter(trainArray.Columns["AgeGroup"].ElementwiseEquals(agr));

                var distr = new double[] { 0, 0, 0, 0 };

                foreach (var st in farr["Stage"])
                {
                    var int_st = Convert.ToInt32(st);

                    distr[int_st-1]++;

                }


                distr = distr.Select(a=> a/distr.Sum()).ToArray();

                Console.WriteLine(i.ToString() +"\t" +String.Join("\t ", distr));

                stageByAgeGenerator[i * 10] = new Multinomial(distr, 1);

            }

        }

        private void GetStageByAgeRegGenerator(double[][] reg, DataFrame trainArray)
        {
            stageByAgeRegGenerator = new Dictionary<int, Multinomial>();

            var minGr = Tech.GetAgeGroupInt(trainArray["Age"].Min());
            var maxGr = Tech.GetAgeGroupInt(trainArray["Age"].Max());

            for (int i = 0; i < 12; i++)
            {
                double[] arr;
                double[] agr = Tech.GetAgeGroup(i*10);

                if (i*10 <= minGr)
                {
                    agr = Tech.GetAgeGroup(minGr);
                }

                if (i * 10 >= maxGr)
                {
                    agr = Tech.GetAgeGroup(maxGr);
                }

                arr = LogisticMultiRegression.ComputeOutput(agr, reg, false);

                Console.WriteLine(i.ToString() + "\t" + String.Join("\t ", arr));

                stageByAgeRegGenerator[i * 10] = new Multinomial(arr, 1);


            }

        }


        //private double[][] GetStagingRegression(DataFrame trainArray)
        //{
        //    var len = trainArray["Age"].Length;
        //    var trainStagingX = new double[len][];
        //    var trainStagingY = new int[len][];

        //    for (int i = 0; i < len; i++)
        //    {
        //        var tempX = new double[3] { 0, 0, 0 };
        //        var tempY = new int[4] { 0, 0, 0, 0 };

        //        tempX[0] = Convert.ToDouble(trainArray["Age"][i]);
        //        tempX[1] = Convert.ToDouble(trainArray["TumorProgression"][i]);
        //        tempX[2] = Convert.ToDouble(trainArray["Aggressiveness"][i]);
        //        tempY[Convert.ToInt32(trainArray["Stage"][i]) - 1] = 1;

        //        trainStagingX[i] = tempX;
        //        trainStagingY[i] = tempY;
        //    }

        //    var reg = LogisticMultiRegression.GetModelByTrainData(trainStagingX, trainStagingY);

        //    return reg;

        //}

        private double[][] GetStageByAgeRegression(DataFrame trainArray)
        {
            var len = trainArray["Age"].Length;
            var trainStagingX = new double[len][];
            var trainStagingY = new int[len][];


            for (int i = 0; i < len; i++)
            {
                var tempY = new int[4] { 0, 0, 0, 0 };
                tempY[Convert.ToInt32(trainArray["Stage"][i]) - 1] = 1;

                trainStagingX[i] = Tech.GetAgeGroup(trainArray["Age"][i]);
                trainStagingY[i] = tempY;
            }


            var reg = LogisticMultiRegression.GetModelByTrainData(trainStagingX, trainStagingY);
            return reg;

        }

        private double[] GetAggressivenessDistribution(DataFrame data)
        {
            var all_sum = new double[] { 0, 0, 0, 0 };
            var agg_sum = new double[] { 0, 0, 0, 0 };

            for (int i = 0; i < data["Age"].Length; i++)
            {
                all_sum[Convert.ToInt32(data["Stage"][i]) - 1] += 1;
                agg_sum[Convert.ToInt32(data["Stage"][i]) - 1] += Convert.ToInt32(data["Aggressiveness"][i]);

            }

            agg_sum = agg_sum.Select((v, i) => v / all_sum[i]).ToArray();


            return agg_sum;
        }


        private void InitFrames()
        {
            TrainData = DataFrame.LoadCsv(trainDataFilename, ';');

            var fsum = Convert.ToDouble(TrainData.Columns["population"].Sum());

            var rate = DFCtoArray(TrainData.Columns["population"]/ fsum);
            initAgeDist  =  new Distribution(rate, DistributionInputType.PDF);

            
            rate = DFCtoArray(TrainData.Columns["deaths all"] / fsum);
            Aging = new Distribution(rate, DistributionInputType.PDF);



            TrainIncidence = DFCtoArray(TrainData.Columns["cases"] / TrainData.Columns["population"]);
            TrainMortality = DFCtoArray(TrainData.Columns["deaths cancer"] / (TrainData.Columns["population"]));

        }

        
        public double[] DFCtoArray(DataFrameColumn dfc)
        {
            var li = new List<double>();

            foreach (var item in dfc)
            {
                li.Add( Convert.ToDouble(item));
            }

            return li.ToArray();
        }


        private RandomGenerator ParseGenerator(string spl)
        {
            var values = spl.Split(',').Select(a=>a.Trim()).ToArray();
            var dbls = new List<double>();

            for (int i = 1; i < values.Count(); i++)
            {
                dbls.Add(Convert.ToDouble(values[i]));
            }

            RandomGenerator rg = new RandomGenerator(dbls.ToArray(), values[0]);

            return rg;
        }

        private Hazard ParseHazard(string spl)
        {
            var values = spl.Split(',').Select(a => a.Trim()).ToArray();
            Hazard hz;

            if (values[0] == "gomp")
            {
                var L = Convert.ToDouble(values[1]);
                var B = Convert.ToDouble(values[2]);

                hz = new GopmHazard(L, B);
            }
            else if(values[0] == "loglog")
            {
                var O = Convert.ToDouble(values[1]);
                var K = Convert.ToDouble(values[2]);

                hz = new LogLogisticHazard(O, K);
            }
            else if (values[0] == "piecewise")
            {
                var C = new List<double>();
                var Y = new List<double>();

                var splt = values[1].Split(';');


                foreach (var item in splt)
                {
                    var YC = item.Split('@');
                    Y.Add(Convert.ToDouble(YC[0]));
                    C.Add(Convert.ToDouble(YC[1]));
                } 

                hz = new Piecewise(C.ToArray(),Y.ToArray());
            }
            else
            {
                var L = Convert.ToDouble(values[1]);

                hz = new ExpHazard(L);
            }

            return hz;
        }


        private double[] FromRisksToDistribution(double[] vals)
        {
            var inverse = vals.Select(a => 1 - a).ToArray();
            List<double> res = new List<double>();

            for (int i = 0; i < vals.Length; i++)
            {
                if (i == 0) 
                {
                    res.Add(vals[0]);
                    continue;
                }

                var prob = inverse.Take(i).Aggregate(1.0, (result, element) => result * element)*vals[i];
                res.Add(prob);
            }

            return res.ToArray();

        }

        public Parameters Clone()
        {
            return (Parameters)this.MemberwiseClone();
        }
    }
}
