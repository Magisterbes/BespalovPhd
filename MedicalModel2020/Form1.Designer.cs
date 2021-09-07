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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea19 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend19 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series19 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea20 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend20 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series20 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea21 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend21 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series21 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea22 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend22 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series22 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea23 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend23 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series23 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea24 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend24 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series24 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.fitResults = new System.Windows.Forms.TabPage();
            this.dgvParams = new System.Windows.Forms.DataGridView();
            this.bEdit = new System.Windows.Forms.Button();
            this.lParams = new System.Windows.Forms.Label();
            this.bSim = new System.Windows.Forms.Button();
            this.bFit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.Demo = new System.Windows.Forms.TabPage();
            this.DemoChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Incid = new System.Windows.Forms.TabPage();
            this.IncChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Diag = new System.Windows.Forms.TabPage();
            this.DiagChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Mort = new System.Windows.Forms.TabPage();
            this.MortChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Surv = new System.Windows.Forms.TabPage();
            this.SurvChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Saved = new System.Windows.Forms.TabPage();
            this.SaveChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ParamName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParamValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.fitResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).BeginInit();
            this.Demo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DemoChart)).BeginInit();
            this.Incid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IncChart)).BeginInit();
            this.Diag.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DiagChart)).BeginInit();
            this.Mort.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MortChart)).BeginInit();
            this.Surv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SurvChart)).BeginInit();
            this.Saved.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveChart)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.fitResults);
            this.tabControl1.Controls.Add(this.Demo);
            this.tabControl1.Controls.Add(this.Incid);
            this.tabControl1.Controls.Add(this.Diag);
            this.tabControl1.Controls.Add(this.Mort);
            this.tabControl1.Controls.Add(this.Surv);
            this.tabControl1.Controls.Add(this.Saved);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1284, 702);
            this.tabControl1.TabIndex = 2;
            // 
            // fitResults
            // 
            this.fitResults.Controls.Add(this.dgvParams);
            this.fitResults.Controls.Add(this.bEdit);
            this.fitResults.Controls.Add(this.lParams);
            this.fitResults.Controls.Add(this.bSim);
            this.fitResults.Controls.Add(this.bFit);
            this.fitResults.Controls.Add(this.label1);
            this.fitResults.Controls.Add(this.tbLog);
            this.fitResults.Location = new System.Drawing.Point(4, 29);
            this.fitResults.Name = "fitResults";
            this.fitResults.Padding = new System.Windows.Forms.Padding(3);
            this.fitResults.Size = new System.Drawing.Size(1276, 669);
            this.fitResults.TabIndex = 6;
            this.fitResults.Text = "Control";
            this.fitResults.UseVisualStyleBackColor = true;
            // 
            // dgvParams
            // 
            this.dgvParams.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParams.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ParamName,
            this.ParamValue});
            this.dgvParams.Location = new System.Drawing.Point(638, 42);
            this.dgvParams.Name = "dgvParams";
            this.dgvParams.RowHeadersWidth = 30;
            this.dgvParams.RowTemplate.Height = 28;
            this.dgvParams.Size = new System.Drawing.Size(630, 619);
            this.dgvParams.TabIndex = 7;
            this.dgvParams.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // bEdit
            // 
            this.bEdit.Location = new System.Drawing.Point(494, 166);
            this.bEdit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bEdit.Name = "bEdit";
            this.bEdit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bEdit.Size = new System.Drawing.Size(112, 35);
            this.bEdit.TabIndex = 6;
            this.bEdit.Text = "Edit Params";
            this.bEdit.UseVisualStyleBackColor = true;
            this.bEdit.Click += new System.EventHandler(this.bEdit_Click);
            // 
            // lParams
            // 
            this.lParams.AutoSize = true;
            this.lParams.Location = new System.Drawing.Point(634, 15);
            this.lParams.Name = "lParams";
            this.lParams.Size = new System.Drawing.Size(91, 20);
            this.lParams.TabIndex = 5;
            this.lParams.Text = "Parameters";
            // 
            // bSim
            // 
            this.bSim.Location = new System.Drawing.Point(494, 100);
            this.bSim.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bSim.Name = "bSim";
            this.bSim.Size = new System.Drawing.Size(112, 35);
            this.bSim.TabIndex = 3;
            this.bSim.Text = "Simulate";
            this.bSim.UseVisualStyleBackColor = true;
            this.bSim.Click += new System.EventHandler(this.bSim_Click);
            // 
            // bFit
            // 
            this.bFit.Location = new System.Drawing.Point(494, 38);
            this.bFit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bFit.Name = "bFit";
            this.bFit.Size = new System.Drawing.Size(112, 35);
            this.bFit.TabIndex = 2;
            this.bFit.Text = "Fit";
            this.bFit.UseVisualStyleBackColor = true;
            this.bFit.Click += new System.EventHandler(this.bFit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Log";
            // 
            // tbLog
            // 
            this.tbLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tbLog.Location = new System.Drawing.Point(28, 42);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.Size = new System.Drawing.Size(439, 619);
            this.tbLog.TabIndex = 0;
            // 
            // Demo
            // 
            this.Demo.Controls.Add(this.DemoChart);
            this.Demo.Location = new System.Drawing.Point(4, 29);
            this.Demo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Demo.Name = "Demo";
            this.Demo.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Demo.Size = new System.Drawing.Size(1276, 669);
            this.Demo.TabIndex = 0;
            this.Demo.Text = "Demogaphics";
            this.Demo.UseVisualStyleBackColor = true;
            // 
            // DemoChart
            // 
            chartArea19.Name = "ChartArea1";
            this.DemoChart.ChartAreas.Add(chartArea19);
            this.DemoChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend19.Name = "Legend1";
            this.DemoChart.Legends.Add(legend19);
            this.DemoChart.Location = new System.Drawing.Point(4, 5);
            this.DemoChart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DemoChart.Name = "DemoChart";
            series19.ChartArea = "ChartArea1";
            series19.Legend = "Legend1";
            series19.Name = "Series1";
            this.DemoChart.Series.Add(series19);
            this.DemoChart.Size = new System.Drawing.Size(1268, 659);
            this.DemoChart.TabIndex = 1;
            this.DemoChart.Text = "chart1";
            // 
            // Incid
            // 
            this.Incid.Controls.Add(this.IncChart);
            this.Incid.Location = new System.Drawing.Point(4, 29);
            this.Incid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Incid.Name = "Incid";
            this.Incid.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Incid.Size = new System.Drawing.Size(1276, 669);
            this.Incid.TabIndex = 2;
            this.Incid.Text = "Incidence";
            this.Incid.UseVisualStyleBackColor = true;
            // 
            // IncChart
            // 
            chartArea20.Name = "ChartArea1";
            this.IncChart.ChartAreas.Add(chartArea20);
            this.IncChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend20.Name = "Legend1";
            this.IncChart.Legends.Add(legend20);
            this.IncChart.Location = new System.Drawing.Point(4, 5);
            this.IncChart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.IncChart.Name = "IncChart";
            series20.ChartArea = "ChartArea1";
            series20.Legend = "Legend1";
            series20.Name = "Series1";
            this.IncChart.Series.Add(series20);
            this.IncChart.Size = new System.Drawing.Size(1268, 659);
            this.IncChart.TabIndex = 2;
            this.IncChart.Text = "chart1";
            // 
            // Diag
            // 
            this.Diag.Controls.Add(this.DiagChart);
            this.Diag.Location = new System.Drawing.Point(4, 29);
            this.Diag.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Diag.Name = "Diag";
            this.Diag.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Diag.Size = new System.Drawing.Size(1276, 669);
            this.Diag.TabIndex = 1;
            this.Diag.Text = "Diagnose";
            this.Diag.UseVisualStyleBackColor = true;
            // 
            // DiagChart
            // 
            chartArea21.Name = "ChartArea1";
            this.DiagChart.ChartAreas.Add(chartArea21);
            this.DiagChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend21.Name = "Legend1";
            this.DiagChart.Legends.Add(legend21);
            this.DiagChart.Location = new System.Drawing.Point(4, 5);
            this.DiagChart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DiagChart.Name = "DiagChart";
            series21.ChartArea = "ChartArea1";
            series21.Legend = "Legend1";
            series21.Name = "Series1";
            this.DiagChart.Series.Add(series21);
            this.DiagChart.Size = new System.Drawing.Size(1268, 659);
            this.DiagChart.TabIndex = 2;
            this.DiagChart.Text = "chart1";
            // 
            // Mort
            // 
            this.Mort.Controls.Add(this.MortChart);
            this.Mort.Location = new System.Drawing.Point(4, 29);
            this.Mort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Mort.Name = "Mort";
            this.Mort.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Mort.Size = new System.Drawing.Size(1276, 669);
            this.Mort.TabIndex = 3;
            this.Mort.Text = "Mortality";
            this.Mort.UseVisualStyleBackColor = true;
            // 
            // MortChart
            // 
            chartArea22.Name = "ChartArea1";
            this.MortChart.ChartAreas.Add(chartArea22);
            this.MortChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend22.Name = "Legend1";
            this.MortChart.Legends.Add(legend22);
            this.MortChart.Location = new System.Drawing.Point(4, 5);
            this.MortChart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MortChart.Name = "MortChart";
            series22.ChartArea = "ChartArea1";
            series22.Legend = "Legend1";
            series22.Name = "Series1";
            this.MortChart.Series.Add(series22);
            this.MortChart.Size = new System.Drawing.Size(1268, 659);
            this.MortChart.TabIndex = 3;
            this.MortChart.Text = "chart1";
            // 
            // Surv
            // 
            this.Surv.Controls.Add(this.SurvChart);
            this.Surv.Location = new System.Drawing.Point(4, 29);
            this.Surv.Name = "Surv";
            this.Surv.Size = new System.Drawing.Size(1276, 669);
            this.Surv.TabIndex = 5;
            this.Surv.Text = "Survival curves";
            this.Surv.UseVisualStyleBackColor = true;
            // 
            // SurvChart
            // 
            chartArea23.Name = "ChartArea1";
            this.SurvChart.ChartAreas.Add(chartArea23);
            this.SurvChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend23.Name = "Legend1";
            this.SurvChart.Legends.Add(legend23);
            this.SurvChart.Location = new System.Drawing.Point(0, 0);
            this.SurvChart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SurvChart.Name = "SurvChart";
            this.SurvChart.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            series23.ChartArea = "ChartArea1";
            series23.Legend = "Legend1";
            series23.Name = "Series1";
            this.SurvChart.Series.Add(series23);
            this.SurvChart.Size = new System.Drawing.Size(1276, 669);
            this.SurvChart.TabIndex = 5;
            this.SurvChart.Text = "Survival";
            // 
            // Saved
            // 
            this.Saved.Controls.Add(this.SaveChart);
            this.Saved.Location = new System.Drawing.Point(4, 29);
            this.Saved.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Saved.Name = "Saved";
            this.Saved.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Saved.Size = new System.Drawing.Size(1276, 669);
            this.Saved.TabIndex = 4;
            this.Saved.Text = "Saved years by incidence age";
            this.Saved.UseVisualStyleBackColor = true;
            // 
            // SaveChart
            // 
            chartArea24.Name = "ChartArea1";
            this.SaveChart.ChartAreas.Add(chartArea24);
            this.SaveChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend24.Name = "Legend1";
            this.SaveChart.Legends.Add(legend24);
            this.SaveChart.Location = new System.Drawing.Point(4, 5);
            this.SaveChart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SaveChart.Name = "SaveChart";
            this.SaveChart.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            series24.ChartArea = "ChartArea1";
            series24.Legend = "Legend1";
            series24.Name = "Series1";
            this.SaveChart.Series.Add(series24);
            this.SaveChart.Size = new System.Drawing.Size(1268, 659);
            this.SaveChart.TabIndex = 4;
            this.SaveChart.Text = "Saved years";
            // 
            // ParamName
            // 
            this.ParamName.HeaderText = "Parameter";
            this.ParamName.MinimumWidth = 8;
            this.ParamName.Name = "ParamName";
            this.ParamName.Width = 170;
            // 
            // ParamValue
            // 
            this.ParamValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ParamValue.HeaderText = "Value";
            this.ParamValue.MinimumWidth = 8;
            this.ParamValue.Name = "ParamValue";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 702);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tabControl1.ResumeLayout(false);
            this.fitResults.ResumeLayout(false);
            this.fitResults.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).EndInit();
            this.Demo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DemoChart)).EndInit();
            this.Incid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.IncChart)).EndInit();
            this.Diag.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DiagChart)).EndInit();
            this.Mort.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MortChart)).EndInit();
            this.Surv.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SurvChart)).EndInit();
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
        private System.Windows.Forms.TabPage Surv;
        private System.Windows.Forms.DataVisualization.Charting.Chart SurvChart;
        private System.Windows.Forms.TabPage fitResults;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.Button bSim;
        private System.Windows.Forms.Button bFit;
        private System.Windows.Forms.Label lParams;
        private System.Windows.Forms.Button bEdit;
        private System.Windows.Forms.DataGridView dgvParams;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParamName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParamValue;
    }
}

