using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MedicalModel
{
    public partial class Form1 : Form
    {

        string[] ColourValues = new string[] {
        "FF0000", "00FF00", "0000FF", "FFFF00", "FF00FF", "00FFFF", "000000",
        "800000", "008000", "000080", "808000", "800080", "008080", "808080",
        "C00000", "00C000", "0000C0", "C0C000", "C000C0", "00C0C0", "C0C0C0",
        "400000", "004000", "000040", "404000", "400040", "004040", "404040",
        "200000", "002000", "000020", "202000", "200020", "002020", "202020",
        "600000", "006000", "000060", "606000", "600060", "006060", "606060",
        "A00000", "00A000", "0000A0", "A0A000", "A000A0", "00A0A0", "A0A0A0",
        "E00000", "00E000", "0000E0", "E0E000", "E000E0", "00E0E0", "E0E0E0",
        };
        public Form1()
        {
            InitializeComponent();

            Environment.Init("parameters.txt");
            Environment.LogInfo.ForEach(a=>AddLog(a));
            PrintParams();

            //Tests.HazardTest.Test(new LogLogisticHazard(-20, 6),this);
            //Tests.HazardTest.Test(new ExpHazard(-4), this);
            //Tests.HazardTest.Test(new GopmHazard(-7,0.05), this);

        }

        void DrawDemography()
        {
            var ad = Environment.Stats.Stats[StatsType.AgeDistributions];
            DemoChart.Series.Clear();

            var counter = 0;
            foreach (var key in ad.Keys)
            {
                if (key%3 == 0)
                {

                    var title = "Year " + key.ToString();
                    ChartIt(title, SeriesChartType.Line, DemoChart, ad[key].Select(a=>(double)a).ToArray());

                    counter++;
                }
            }

            DemoChart.ChartAreas[0].AxisX.Title = "Age";
            DemoChart.ChartAreas[0].AxisY.Title = "People per age group";


        }

        void DrawIncidence(Dictionary<AggStatsType,double[]> agg, bool clear, string Title)
        {
            var data = agg[AggStatsType.IncidenceRates].Select(a=> sfLog(a)).ToArray() ;
            var diagData = agg[AggStatsType.DiagnosisRates].Select(a => sfLog(a)).ToArray();
            var trinc = Environment.Params.TrainIncidence.Select(a => sfLog(a)).ToArray();
            var x = Enumerable.Range(0, 95).Select(a => (double)a).ToArray();

            if (clear)
                IncChart.Series.Clear();

            var title = "Simulated Incidence Rates " + Title;
            ChartIt(title, SeriesChartType.Line, IncChart, data,x);
            ChartIt("Simulated Diagnosis Rates", SeriesChartType.Line, IncChart, diagData, x);
            ChartIt("Train Diagnosis Rates", SeriesChartType.Line, IncChart, trinc, x);


            IncChart.ChartAreas[0].AxisX.Title = "Age";
            IncChart.ChartAreas[0].AxisY.Title = "log10(Rates)";

            data = data.Where(a => a != 0).ToArray();

            IncChart.ChartAreas[0].AxisY.Maximum = data.Max() + 0.1;
            IncChart.ChartAreas[0].AxisY.Minimum = data.Min() - 0.1;
  
        }


        private double sfLog(double val)
        {
            if (val <= 0)
                return 0;

            return Math.Log10(val);
        }

        void DrawMortality(Dictionary<AggStatsType, double[]> agg, bool clear, string Title)
        {
            var data = agg[AggStatsType.MortalityRates].Select(a => sfLog(a)).ToArray();
            var screen = agg[AggStatsType.ScreenedMortalityRates].Select(a => sfLog(a) ).ToArray();
            var trdata = Environment.Params.TrainMortality.Select(a => sfLog(a)).ToArray();
            var x = Enumerable.Range(0, 95).Select(a => (double)a).ToArray();



            if (clear) 
                MortChart.Series.Clear();

            var title = "Simulated Mortality Rates " + Title;
            ChartIt(title, SeriesChartType.Line, MortChart, data,x);

            title = "Screening Affected Mortality Rates (Screening) "+ Title;
            ChartIt(title, SeriesChartType.Line, MortChart, screen,x);

            
             ChartIt("Train Mortality", SeriesChartType.Line, MortChart, trdata, x);
            

            MortChart.ChartAreas[0].AxisX.Title = "Age";
            MortChart.ChartAreas[0].AxisY.Title = "log10(Rates)";

            data = data.Where(a => a != 0).ToArray();

            MortChart.ChartAreas[0].AxisY.Maximum = data.Max() + 0.1;
            MortChart.ChartAreas[0].AxisY.Minimum = data.Min() - 0.1;
        }

        void DrawDiagnose()
        {
            var data = Environment.Stats.AggStats[AggStatsType.DiagnoseStagesDistribution];
            var screen = Environment.Stats.AggStats[AggStatsType.ScreeningStagesDistribution];
            var dsm = data.Sum();
            var ssm = screen.Sum();
            var fdata = data.Select(a => (double)a / (double)dsm).ToArray();
            var fscreen = screen.Select(a => (double)a / (double)ssm).ToArray();
            var stages = Enumerable.Range(0, fscreen.Length).Select(a => (double)(a+1)).ToArray();

            DiagChart.Series.Clear();


            ChartIt("Clinical phase stages distribution", SeriesChartType.Column, DiagChart, fdata,stages);
            ChartIt("Screening affected stages distribution", SeriesChartType.Column, DiagChart, fscreen, stages);


            DiagChart.ChartAreas[0].AxisX.Title = "Cancer stage";
            DiagChart.ChartAreas[0].AxisY.Title = "Frequency distribution";

        }


        public void ChartTest(string Title, double[] data, double[] x)
        {
            //testChart.Series.Add(Title);
            //testChart.Series[Title].ChartType = SeriesChartType.Line;

            //for (int j = 0; j < x.Length; j++)
            //{
            //    if (data[j] == 0)
            //        continue;
            //    testChart.Series[Title].Points.AddXY(x[j], data[j]);
            //}
            //testChart.Series[Title].BorderWidth = 2;
            //testChart.ChartAreas[0].AxisX.Minimum = 0;
            //testChart.ChartAreas[0].AxisY.IsLogarithmic = true;
        }

        public void ChartIt(string Title, SeriesChartType ctype, Chart ThisChart, double[] data, double[] x)
        {
            ThisChart.Series.Add(Title);
            ThisChart.Series[Title].ChartType = ctype;

            for (int j = 0; j < x.Length; j++)
            {
                if (data[j] != 0)
                    ThisChart.Series[Title].Points.AddXY(x[j], data[j]);
            }

            

            ThisChart.Series[Title].BorderWidth = 2;
            ThisChart.ChartAreas[0].AxisX.Minimum = 0;
            ThisChart.ChartAreas[0].AxisY.LabelStyle.Format = "{##.####}";
        }

        private void ChartIt(string Title, SeriesChartType ctype, Chart ThisChart, double[] data)
        {

            var x = Enumerable.Range(0, data.Length).Select(a=>(double)a).ToArray();

            ChartIt(Title, ctype, ThisChart, data, x);
        }

        void DrawSaved()
        {
            //var pep = Environment.Stats.AggStats[AggStatsType.PeopleSaved];
            var yer = Environment.Stats.AggStats[AggStatsType.YearsSaved];

            //var data = pep.Zip(yer, (p, y) => (double)y / (double)p).ToArray();

            SaveChart.Series.Clear();

            var title = "Saved years";

            ChartIt(title, SeriesChartType.Column, SaveChart, yer);


            SaveChart.ChartAreas[0].AxisX.Title = "Age";
            SaveChart.ChartAreas[0].AxisY.Title = "Saved years";
        }

        void DrawSurvival()
        {
            var cancer = Tech.CutByZero(Environment.Stats.AggStats[AggStatsType.Survival]).Take(20).ToArray();
            var screening = Tech.CutByZero(Environment.Stats.AggStats[AggStatsType.SurvivalScreening]).Take(20).ToArray();
            var x = Enumerable.Range(0, screening.Length).Select(a => (double)a+0.1).ToArray();

            SurvChart.Series.Clear();

            var title = "No Screening";
            ChartIt(title, SeriesChartType.Line, SurvChart, cancer);

            title = "Screening";
            ChartIt(title, SeriesChartType.Line, SurvChart, screening,x);


            SurvChart.ChartAreas[0].AxisX.Title = "Years";
            SurvChart.ChartAreas[0].AxisY.Title = "Cause specific surviaval function";
        }

        private void bSim_Click(object sender, EventArgs e)
        {
            var d = DateTime.Now;

            SplashUtility<Waitbar>.Show();
            Environment.CurrentDate = 0;
            AddLog("Simulation started.");

            Environment.Start();

            for (int i = 0; i < Environment.Params.YearsToSimulate - 1; i++)
            {
                Environment.CurrentDate++;
                SplashUtility<Waitbar>.SetStatusText("Simulating year: " + Environment.CurrentDate.ToString());
            }

            SplashUtility<Waitbar>.Close();
            this.Show();
            this.BringToFront();
            Environment.Stats.GatherStats();
            

            DrawDemography();
            DrawIncidence(Environment.Stats.AggStats,true,"");
            DrawDiagnose();
            DrawMortality(Environment.Stats.AggStats, true, "");
            DrawSaved();
            DrawSurvival();

            AddLog(string.Format("Simulation Finished. Duration: {0} s.", (DateTime.Now - d).Seconds));

        }

        private void bFit_Click(object sender, EventArgs e)

        {
            AddLog("Fit started.");
            LogAdjustParams();
            SplashUtility<Waitbar>.Show();
            SplashUtility<Waitbar>.SetStatusText("Diagnosis data fitting");
            AdjustParamsDiag.Adjust(this);
            SplashUtility<Waitbar>.SetStatusText("Mortality data fitting");
            AdjustParamsMort.Adjust(this);
            SplashUtility<Waitbar>.Close();
            AddLog("Fit finnished.");
            LogAdjustParams();
        }

        public void AddLog(string text)
        {
            var d = DateTime.Now;

            tbLog.AppendText(string.Format("{0:MM/dd/yyyy HH:mm}: {1}\r\n", d, text));

        }


        private void LogAdjustParams()
        {
            List<string> res = new List<string>();

            //res.Add("GrowthRateLimits:" + string.Join(", ", Environment.Params.GrowthRateLimits));
            res.Add("ProportionOfAggressive:" + string.Join(", ", Environment.Params.ProportionOfAggressive));
            res.Add("AggressivenessRateThreshold:" + string.Join(", ", Environment.Params.AggressivenessRateThreshold));
            res.Add("DiagnoseHazard:" + string.Join(", ", Environment.Params.DiagnoseHazard.Constants));
            res.Add("CancerDeathHazard:" + string.Join(", ", Environment.Params.CancerDeathHazard.Constants));



            AddLog(string.Join("\r\n", res));
          
        }

        private void PrintParams()
        {
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(Environment.Params))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(Environment.Params);

                if (value == null)
                {
                    continue;
                }

                var row = new DataGridViewRow();
                row.CreateCells(dgvParams);
                if (value.GetType() == typeof(double[]))
                {
                    var vals = string.Join(", ",((double[])value).Select(a => a.ToString()).ToArray());
                    row.SetValues(new object[] { name, vals });
                }
                else 
                {
                    row.SetValues(new object[] { name, value });
                }
                dgvParams.Rows.Add(row);
            }



        }

        private void bEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("notepad++", "parameters.txt");
            }
            catch
            {
                Process.Start("notepad.exe", "parameters.txt");
            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void MortChart_Click(object sender, EventArgs e)
        {

        }

        private void DiagChart_Click(object sender, EventArgs e)
        {

        }
    }
}
