namespace MedicalModel
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Demo = new System.Windows.Forms.TabPage();
            this.DemoChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Incid = new System.Windows.Forms.TabPage();
            this.IncChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Diag = new System.Windows.Forms.TabPage();
            this.DiagChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Mort = new System.Windows.Forms.TabPage();
            this.MortChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Saved = new System.Windows.Forms.TabPage();
            this.SaveChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabControl1.SuspendLayout();
            this.Demo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DemoChart)).BeginInit();
            this.Incid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IncChart)).BeginInit();
            this.Diag.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DiagChart)).BeginInit();
            this.Mort.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MortChart)).BeginInit();
            this.Saved.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveChart)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Demo);
            this.tabControl1.Controls.Add(this.Incid);
            this.tabControl1.Controls.Add(this.Diag);
            this.tabControl1.Controls.Add(this.Mort);
            this.tabControl1.Controls.Add(this.Saved);
            this.tabControl1.Location = new System.Drawing.Point(22, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1252, 612);
            this.tabControl1.TabIndex = 2;
            // 
            // Demo
            // 
            this.Demo.Controls.Add(this.DemoChart);
            this.Demo.Location = new System.Drawing.Point(4, 22);
            this.Demo.Name = "Demo";
            this.Demo.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.Demo.Size = new System.Drawing.Size(1244, 586);
            this.Demo.TabIndex = 0;
            this.Demo.Text = "Demogaphics";
            this.Demo.UseVisualStyleBackColor = true;
            // 
            // DemoChart
            // 
            chartArea1.Name = "ChartArea1";
            this.DemoChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.DemoChart.Legends.Add(legend1);
            this.DemoChart.Location = new System.Drawing.Point(19, 6);
            this.DemoChart.Name = "DemoChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.DemoChart.Series.Add(series1);
            this.DemoChart.Size = new System.Drawing.Size(1195, 560);
            this.DemoChart.TabIndex = 1;
            this.DemoChart.Text = "chart1";
            // 
            // Incid
            // 
            this.Incid.Controls.Add(this.IncChart);
            this.Incid.Location = new System.Drawing.Point(4, 22);
            this.Incid.Name = "Incid";
            this.Incid.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.Incid.Size = new System.Drawing.Size(1244, 586);
            this.Incid.TabIndex = 2;
            this.Incid.Text = "Incidence";
            this.Incid.UseVisualStyleBackColor = true;
            // 
            // IncChart
            // 
            chartArea2.Name = "ChartArea1";
            this.IncChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.IncChart.Legends.Add(legend2);
            this.IncChart.Location = new System.Drawing.Point(21, 17);
            this.IncChart.Name = "IncChart";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.IncChart.Series.Add(series2);
            this.IncChart.Size = new System.Drawing.Size(1173, 532);
            this.IncChart.TabIndex = 2;
            this.IncChart.Text = "chart1";
            // 
            // Diag
            // 
            this.Diag.Controls.Add(this.DiagChart);
            this.Diag.Location = new System.Drawing.Point(4, 22);
            this.Diag.Name = "Diag";
            this.Diag.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.Diag.Size = new System.Drawing.Size(1244, 586);
            this.Diag.TabIndex = 1;
            this.Diag.Text = "Diagnose";
            this.Diag.UseVisualStyleBackColor = true;
            // 
            // DiagChart
            // 
            chartArea3.Name = "ChartArea1";
            this.DiagChart.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.DiagChart.Legends.Add(legend3);
            this.DiagChart.Location = new System.Drawing.Point(16, 6);
            this.DiagChart.Name = "DiagChart";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.DiagChart.Series.Add(series3);
            this.DiagChart.Size = new System.Drawing.Size(1198, 556);
            this.DiagChart.TabIndex = 2;
            this.DiagChart.Text = "chart1";
            // 
            // Mort
            // 
            this.Mort.Controls.Add(this.MortChart);
            this.Mort.Location = new System.Drawing.Point(4, 22);
            this.Mort.Name = "Mort";
            this.Mort.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.Mort.Size = new System.Drawing.Size(1244, 586);
            this.Mort.TabIndex = 3;
            this.Mort.Text = "Mortality";
            this.Mort.UseVisualStyleBackColor = true;
            // 
            // MortChart
            // 
            chartArea4.Name = "ChartArea1";
            this.MortChart.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.MortChart.Legends.Add(legend4);
            this.MortChart.Location = new System.Drawing.Point(25, 20);
            this.MortChart.Name = "MortChart";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.MortChart.Series.Add(series4);
            this.MortChart.Size = new System.Drawing.Size(1173, 532);
            this.MortChart.TabIndex = 3;
            this.MortChart.Text = "chart1";
            // 
            // Saved
            // 
            this.Saved.Controls.Add(this.SaveChart);
            this.Saved.Location = new System.Drawing.Point(4, 22);
            this.Saved.Name = "Saved";
            this.Saved.Padding = new System.Windows.Forms.Padding(3);
            this.Saved.Size = new System.Drawing.Size(1244, 586);
            this.Saved.TabIndex = 4;
            this.Saved.Text = "Saved years by incidence age";
            this.Saved.UseVisualStyleBackColor = true;
            // 
            // SaveChart
            // 
            chartArea5.Name = "ChartArea1";
            this.SaveChart.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.SaveChart.Legends.Add(legend5);
            this.SaveChart.Location = new System.Drawing.Point(36, 27);
            this.SaveChart.Name = "SaveChart";
            this.SaveChart.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.SaveChart.Series.Add(series5);
            this.SaveChart.Size = new System.Drawing.Size(1173, 532);
            this.SaveChart.TabIndex = 4;
            this.SaveChart.Text = "Saved years";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 456);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tabControl1.ResumeLayout(false);
            this.Demo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DemoChart)).EndInit();
            this.Incid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.IncChart)).EndInit();
            this.Diag.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DiagChart)).EndInit();
            this.Mort.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MortChart)).EndInit();
            this.Saved.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SaveChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Demo;
        private System.Windows.Forms.DataVisualization.Charting.Chart DemoChart;
        private System.Windows.Forms.TabPage Diag;
        private System.Windows.Forms.DataVisualization.Charting.Chart DiagChart;
        private System.Windows.Forms.TabPage Incid;
        private System.Windows.Forms.DataVisualization.Charting.Chart IncChart;
        private System.Windows.Forms.TabPage Mort;
        private System.Windows.Forms.DataVisualization.Charting.Chart MortChart;
        private System.Windows.Forms.TabPage Saved;
        private System.Windows.Forms.DataVisualization.Charting.Chart SaveChart;
    }
}

