using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MedicalModel
{
    public partial class Waitbar : Form, ISplashForm
    {

        private System.Windows.Forms.DataVisualization.Charting.Chart Ct;

        public Waitbar()
        {
            InitializeComponent();
        }

        public void SetStatusText(string text)
        {
            label2.Text = text;
        }

        public void DrawPlot(double[] data)
        {
            if (Ct == null)
            {
                Ct = new System.Windows.Forms.DataVisualization.Charting.Chart();
                this.Height = 250;
                Ct.Height = 190;
                Ct.Width = this.Width-10;
                Ct.Location = new System.Drawing.Point(0, 50);

                var car = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
                Ct.ChartAreas.Add(car);
            }

            Ct.Series.Clear();
            Ct.Series.Add("Error");
            Ct.Series["Error"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            for (int j = 0; j < data.Length; j++)
            {
                Ct.Series["Error"].Points.AddXY(j, data[j]);
            }
            Ct.Series["Error"].BorderWidth = 2;
            Ct.ChartAreas[0].AxisX.Minimum = 0;

            this.Controls.Add(Ct);
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

    }
}
