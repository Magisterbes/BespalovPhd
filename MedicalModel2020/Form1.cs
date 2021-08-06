using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var d = DateTime.Now;

            Environment.Init("parameters.txt");

            for (int i = 0; i < Environment.Params.YearsToSimulate-1; i++)
            {
                Environment.CurrentDate++;
            }

            Environment.Stats.GatherStats();


            DrawDemography();
            DrawIncidence();
            DrawDiagnose();
            DrawMortality();
            DrawSaved();
            DrawSurvival();

            Console.WriteLine("{0}", (d - DateTime.Now).Seconds);
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

        void DrawIncidence()
        {
            var data = Environment.Stats.AggStats[AggStatsType.IncidenceRates].Select(a=>a*100000).ToArray() ;
            var x = Enumerable.Range(0, 95).Select(a => (double)a).ToArray();
            IncChart.Series.Clear();

            var title = "Incidence Rates";
            ChartIt(title, SeriesChartType.Line, IncChart, data,x);

            IncChart.ChartAreas[0].AxisX.Title = "Age";
            IncChart.ChartAreas[0].AxisY.Title = "Rates*100000";

        }


        void DrawMortality()
        {
            var data = Environment.Stats.AggStats[AggStatsType.MortalityRates].Select(a => a * 100000).ToArray();
            var screen = Environment.Stats.AggStats[AggStatsType.ScreenedMortalityRates].Select(a => a * 100000).ToArray();
            var x = Enumerable.Range(0, 95).Select(a => (double)a).ToArray();
            MortChart.Series.Clear();

            var title = "Mortality Rates";
            ChartIt(title, SeriesChartType.Line, MortChart, data,x);

            title = "Mortality Rates (Screening)";
            ChartIt(title, SeriesChartType.Line, MortChart, screen,x);

            MortChart.ChartAreas[0].AxisX.Title = "Age";
            MortChart.ChartAreas[0].AxisY.Title = "Rates*100000";
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


            ChartIt("Overall diagnose stages distribution", SeriesChartType.Column, DiagChart, fdata,stages);
            ChartIt("Overall screening stages distribution", SeriesChartType.Column, DiagChart, fscreen, stages);


            DiagChart.ChartAreas[0].AxisX.Title = "Cancer stage";
            DiagChart.ChartAreas[0].AxisY.Title = "Frequency distribution";

        }


        private void ChartIt(string Title, SeriesChartType ctype, Chart ThisChart, double[] data, double[] x)
        {
            ThisChart.Series.Add(Title);
            ThisChart.Series[Title].ChartType = ctype;

            for (int j = 0; j < x.Length; j++)
            {
                ThisChart.Series[Title].Points.AddXY(x[j], data[j]);
            }
            ThisChart.Series[Title].BorderWidth = 2;
            ThisChart.ChartAreas[0].AxisX.Minimum = 0;
        }

        private void ChartIt(string Title, SeriesChartType ctype, Chart ThisChart, double[] data)
        {

            var x = Enumerable.Range(0, data.Length).Select(a=>(double)a).ToArray();

            ChartIt(Title, ctype, ThisChart, data, x);
        }

        void DrawSaved()
        {
            var pep = Environment.Stats.AggStats[AggStatsType.PeopleSaved];
            var yer = Environment.Stats.AggStats[AggStatsType.YearsSaved];

            var data = pep.Zip(yer, (p, y) => (double)y / (double)p).ToArray();

            SaveChart.Series.Clear();

            var title = "Mean saved years by person for different incidence ages";

            ChartIt(title, SeriesChartType.Line, SaveChart, data);


            SaveChart.ChartAreas[0].AxisX.Title = "Age";
            SaveChart.ChartAreas[0].AxisY.Title = "Saved years";
        }

        void DrawSurvival()
        {
            var cancer = Tech.CutByZero(Environment.Stats.AggStats[AggStatsType.Survival]);
            var screening = Tech.CutByZero(Environment.Stats.AggStats[AggStatsType.SurvivalScreening]);
            var x = Enumerable.Range(0, screening.Length).Select(a => (double)a+0.1).ToArray();

            SurvChart.Series.Clear();

            var title = "No Screening";
            ChartIt(title, SeriesChartType.StepLine, SurvChart, cancer);

            title = "Screening";
            ChartIt(title, SeriesChartType.StepLine, SurvChart, screening,x);


            SurvChart.ChartAreas[0].AxisX.Title = "Years";
            SurvChart.ChartAreas[0].AxisY.Title = "Percent surviving";
        }
    }
}
