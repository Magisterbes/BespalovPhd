using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalModel
{
    class Cancer
    {
        public int IncidenceAge { get; set; }
        public int[] StagesAges { get; set; }
        public int DiagnosisAge { get; set; }
        public int AfterDiagYears { get; set; }
        public bool IsCured { get; set; }
        public int DiagnoseStage { get; set; }
        public int MalignancyAge { get; set; }
        public int ScreeningAge { get; set; }

        public bool ScreeningFound { get; set; }
        public int ScreeningStage { get; set; }
        public bool IsScreeningCured { get; set; }
        public double GrowthRate { get; set; }
        public double Reoccurred { get; set; }
        public double CancerDeathAgeInit { get; set; }
        public double CancerDeathAgeCure { get; set; }
        public double CancerDeathAgeScreen { get; set; }

        public int IsAggressive { get; set; }

        private Hazard DiagnoseHazard;
        private Hazard CancerDeathHazard;
        private Hazard MalignancyHazard;

        public Cancer(Person p)
        {
            DiagnosisAge = p.DiagnosisAge;
            DiagnoseStage = -1;
            ScreeningStage = -1;
            ScreeningAge = -1;
            MalignancyAge = -1;
            ScreeningFound = false;
            CancerDeathAgeInit = 1000;
            CancerDeathAgeCure = 1000;
            CancerDeathAgeScreen = 1000;

            DiagnoseHazard = Environment.Params.DiagnoseHazard;
            CancerDeathHazard = Environment.Params.CancerDeathHazard;
            // MalignancyHazard = Environment.Params.MalignancyHazard;

            CalculateHistory(p);

        }



        internal void CalculateHistory(Person p)
        {
            IsCured = false;
            IsScreeningCured = false;
            int stages = Environment.Params.StageDistributionLength;
            IncidenceAge = DiagnosisAge - SimulateIncidenceAge();
            if (IncidenceAge < 0)
            {
                IncidenceAge = 0;
            }

            if (p.DateBirth + IncidenceAge < Environment.Params.YearsToSimulate && (p.DateBirth + IncidenceAge >= 0) && IncidenceAge < Environment.Params.UnrealLifeLength)
            {
                Environment.Stats.UpdateStats(StatsType.Inicdence, IncidenceAge + p.DateBirth, IncidenceAge);
            }

            StagesAges = GetStagesAges(GrowthRate,IncidenceAge);
            
            StagesAges[stages] = StagesAges[stages - 1] + (int)CancerDeathHazard.T(0, 0);
            Reoccurred = Tech.CheckByProb(Environment.Params.ReoccurrenceProbability); 

            //See if person has died earier or before diagnose
            CancerDeathAgeInit = StagesAges[stages];
            if (DiagnosisAge > p.NaturalDeathAge && StagesAges[stages] > p.NaturalDeathAge)
            {
                return;
            }


            if (p.DateBirth + DiagnosisAge < Environment.Params.YearsToSimulate && (p.DateBirth + DiagnosisAge>=0)&& DiagnosisAge<Environment.Params.UnrealLifeLength)
                Environment.Stats.UpdateStats(StatsType.Diagnosis, p.DateBirth+ DiagnosisAge, DiagnosisAge);

 
            var cured = CureIt(DiagnoseStage, DiagnosisAge);

            if (cured)
            {
                IsCured = true;
                CancerDeathAgeCure = Convert.ToInt32(GetCuredAge());
            }
            

        }

 
        double GetProgressionRate(double stage)
        {
            
            var low = Environment.Params.GrowthRateLimits[0];
            var up = Environment.Params.GrowthRateLimits[1];
            var thr = Environment.Params.AggressivenessRateThreshold;

            if (IsAggressive==1)
            {
                return low + (1 - thr) * (up - low) * Tech.NextDouble(false);
            }
            else
            {
                return low + (1 - thr) * (up - low) + thr *(up - low) * Tech.NextDouble(false);
            }

        }

        double GetTumorProgression(int stage, int age, int agressiveness)
        {
            
            var reg = Environment.Params.ProgressionRegression;
            var vals = new double[] {1,0,0,0,0,0,0};

            vals[1] = age;
            vals[2] = agressiveness;
            vals[2 + stage] = 1;

            var progression = Tech.ComputeRegression(reg,vals);

            return progression;

        }

        int SimulateIncidenceAge()
        {
            this.DiagnoseStage = Environment.Params
                                    .StageByAgeRegGenerator[Tech.GetAgeGroupInt(DiagnosisAge)]
                                    .Sample().ToList()
                                    .IndexOf(1)+1;

            var propAgg = Environment.Params.ProportionOfAggressive[(int)DiagnoseStage - 1];
            this.IsAggressive = Tech.CheckByProb(propAgg);

            double ttd;

            if (IsAggressive == 1) {
                ttd = Tech.Rnd.Next(0, Convert.ToInt32(propAgg * Environment.Params.TBDGenerationArray.Length));
            }
            else
            {
                ttd = Tech.Rnd.Next(Convert.ToInt32(propAgg * Environment.Params.TBDGenerationArray.Length), Environment.Params.TBDGenerationArray.Length-1);
            }

            ttd = Environment.Params.TBDGenerationArray[(int)ttd];

            ttd = Math.Ceiling(ttd * Environment.Params.TimeBeforeDiagnosisStageShift[(int)DiagnoseStage - 1]);
            //ttd = Math.Ceiling(ttd*(-(1/60)*IncidenceAge+(5/3)));

            if (DiagnoseStage == 1)
            {

                GrowthRate = Math.Exp((Math.Log(DiagnoseStage + Tech.Rnd.NextDouble())) / ttd);
            }
            else
            {
                GrowthRate = Math.Exp((Math.Log(DiagnoseStage)) / ttd);
            }
            return Convert.ToInt32(ttd);

        }

        public bool CureIt(int Stage,int Age)
        {
            if (Tech.NextDouble(true) < AgeCureEff(Age)*Environment.Params.TreatmentEfficiency[Stage-1])
            {
                return true;
            }

            return false;
        }

        private double AgeCureEff(int Age)
        {
            //Ages array 0-40,40-50,50-60,70-80,80+
            var acc = Environment.Params.AgeCureConstants;

            var ag = Tech.GetAgeGroupInt(Age);


            if (ag <= 40) { return acc[0]; }
            if (ag == 50) { return acc[1]; }
            if (ag == 60) { return acc[2]; }
            if (ag == 70) { return acc[3]; }
            if (ag >= 80) { return acc[4]; }

            return 1;
        }


        public void FromScreening(Person p)
        {
            ScreeningAge = p.Age;
            ScreeningStage = GetStageByAge(ScreeningAge);
            ScreeningFound = true;
            var cured = CureIt(ScreeningStage, ScreeningAge);

            if (cured)
            {
                IsScreeningCured = true;

                CancerDeathAgeScreen = Convert.ToInt32(GetCuredAge());
            }
            else
            {
                IsScreeningCured = false;
                IsCured = false;
                CancerDeathAgeInit = StagesAges[StagesAges.Length-1];
            }
             

        }

        private double GetCuredAge()
        {

            var newAge = DiagnosisAge + +5 + Tech.Rnd.NextDouble() * 5;

            if (newAge < CancerDeathAgeInit)
            {
                newAge = CancerDeathAgeInit + Tech.Rnd.NextDouble() * 5;
            }

            return newAge;
        }

        int[] GetStagesAges(double GrowthRate, int IncidenceAge)
        {
            var stages = Enumerable.Repeat(0, Environment.Params.StageDistributionLength + 1).ToArray();

            var refAge = IncidenceAge;

            stages[0] = IncidenceAge;
            stages[1] = IncidenceAge+ Convert.ToInt32(Math.Log(2)/Math.Log(GrowthRate));
            stages[2] = IncidenceAge + Convert.ToInt32(Math.Log(3) / Math.Log(GrowthRate));
            stages[3] = IncidenceAge + Convert.ToInt32(Math.Log(4) / Math.Log(GrowthRate));

           
            return stages;

        }

        int GetStageByAge(int age)
        {

            var filterArr = this.StagesAges.Select(a => a - age).Where(a=>a<=0).ToArray();

            if (filterArr.Length == 5)
            {
                return 4;
            }

            return filterArr.Length;

        }

        //int[] GetStagesAges(double GrowthRate, int IncidenceAge)
        //{
        //    var stages = Enumerable.Repeat(0, Environment.Params.StageDistributionLength+1).ToArray();

        //    var refAge = IncidenceAge;

        //    stages[0] = IncidenceAge;


        //    var reg = Environment.Params.StagingRegression;
        //    for (int i = refAge+1; i < Environment.Params.UnrealLifeLength; i++)
        //    {
        //        var newAge = i;
        //        var newProgression = Math.Pow(GrowthRate, i - refAge);

        //        var vals = new double[] { newAge, newProgression, IsAggressive };
        //        var regRes = LogisticMultiRegression.ComputeOutput(vals, reg, false).ToList();
        //        var newStage = regRes.IndexOf(regRes.Max());

        //        if (newStage == 3)
        //        {
        //            stages[3] = newAge;
        //            break;
        //        }

        //    }

        //    if (stages[3] == 0)
        //    {
        //        stages[3] = 121;
        //    }

        //    return stages;

        //}

        //old
        //double GetTumorSize(double stage, int age, int agressiveness)
        //{
        //    var tumorSize = 1.0;
        //    var crit = Environment.Params.StageCriteria;

        //    switch (DiagnoseStage)
        //    {
        //        case 1:
        //            tumorSize = Tech.GetUni(0, crit[0]);
        //            break;
        //        case 2:
        //            tumorSize = Tech.GetUni(crit[0], crit[1]);
        //            break;
        //        case 3:
        //            tumorSize = Tech.GetUni(crit[1], crit[2]);
        //            break;
        //        case 4:
        //            tumorSize = Tech.GetUni(crit[2], 2 * crit[2]);
        //            break;
        //    }

        //    return tumorSize;

        //}


    }
}
