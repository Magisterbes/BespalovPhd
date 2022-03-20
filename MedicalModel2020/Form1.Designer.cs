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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.Saved = new System.Windows.Forms.TabPage();
            this.SaveChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Surv = new System.Windows.Forms.TabPage();
            this.SurvChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Incid = new System.Windows.Forms.TabPage();
            this.IncChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label2 = new System.Windows.Forms.Label();
            this.MortChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DiagChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Demo = new System.Windows.Forms.TabPage();
            this.DemoChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.fitResults = new System.Windows.Forms.TabPage();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bFit = new System.Windows.Forms.Button();
            this.bSim = new System.Windows.Forms.Button();
            this.lParams = new System.Windows.Forms.Label();
            this.bEdit = new System.Windows.Forms.Button();
            this.dgvParams = new System.Windows.Forms.DataGridView();
            this.ParamValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParamName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Saved.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveChart)).BeginInit();
            this.Surv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SurvChart)).BeginInit();
            this.Incid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IncChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MortChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DiagChart)).BeginInit();
            this.Demo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DemoChart)).BeginInit();
            this.fitResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Saved
            // 
            this.Saved.Controls.Add(this.SaveChart);
            this.Saved.Location = new System.Drawing.Point(4, 25);
            this.Saved.Margin = new System.Windows.Forms.Padding(4);
            this.Saved.Name = "Saved";
            this.Saved.Padding = new System.Windows.Forms.Padding(4);
            this.Saved.Size = new System.Drawing.Size(1357, 721);
            this.Saved.TabIndex = 4;
            this.Saved.Text = "Saved years by incidence age";
            this.Saved.UseVisualStyleBackColor = true;
            // 
            // SaveChart
            // 
            chartArea1.Name = "ChartArea1";
            this.SaveChart.ChartAreas.Add(chartArea1);
            this.SaveChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.SaveChart.Legends.Add(legend1);
            this.SaveChart.Location = new System.Drawing.Point(4, 4);
            this.SaveChart.Margin = new System.Windows.Forms.Padding(4);
            this.SaveChart.Name = "SaveChart";
            this.SaveChart.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.SaveChart.Series.Add(series1);
            this.SaveChart.Size = new System.Drawing.Size(1349, 713);
            this.SaveChart.TabIndex = 4;
            this.SaveChart.Text = "Saved years";
            // 
            // Surv
            // 
            this.Surv.Controls.Add(this.SurvChart);
            this.Surv.Location = new System.Drawing.Point(4, 25);
            this.Surv.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Surv.Name = "Surv";
            this.Surv.Size = new System.Drawing.Size(1357, 721);
            this.Surv.TabIndex = 5;
            this.Surv.Text = "Survival curves";
            this.Surv.UseVisualStyleBackColor = true;
            // 
            // SurvChart
            // 
            chartArea2.Name = "ChartArea1";
            this.SurvChart.ChartAreas.Add(chartArea2);
            this.SurvChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.SurvChart.Legends.Add(legend2);
            this.SurvChart.Location = new System.Drawing.Point(0, 0);
            this.SurvChart.Margin = new System.Windows.Forms.Padding(4);
            this.SurvChart.Name = "SurvChart";
            this.SurvChart.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.SurvChart.Series.Add(series2);
            this.SurvChart.Size = new System.Drawing.Size(1357, 721);
            this.SurvChart.TabIndex = 5;
            this.SurvChart.Text = "Survival";
            // 
            // Incid
            // 
            this.Incid.Controls.Add(this.DiagChart);
            this.Incid.Controls.Add(this.label4);
            this.Incid.Controls.Add(this.label3);
            this.Incid.Controls.Add(this.MortChart);
            this.Incid.Controls.Add(this.label2);
            this.Incid.Controls.Add(this.IncChart);
            this.Incid.Location = new System.Drawing.Point(4, 25);
            this.Incid.Margin = new System.Windows.Forms.Padding(4);
            this.Incid.Name = "Incid";
            this.Incid.Padding = new System.Windows.Forms.Padding(4);
            this.Incid.Size = new System.Drawing.Size(1282, 776);
            this.Incid.TabIndex = 2;
            this.Incid.Text = "Results";
            this.Incid.UseVisualStyleBackColor = true;
            // 
            // IncChart
            // 
            chartArea5.Name = "ChartArea1";
            this.IncChart.ChartAreas.Add(chartArea5);
            legend5.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend5.Name = "Legend1";
            this.IncChart.Legends.Add(legend5);
            this.IncChart.Location = new System.Drawing.Point(9, 58);
            this.IncChart.Margin = new System.Windows.Forms.Padding(4);
            this.IncChart.Name = "IncChart";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.IncChart.Series.Add(series5);
            this.IncChart.Size = new System.Drawing.Size(677, 365);
            this.IncChart.TabIndex = 3;
            this.IncChart.Text = "chart1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.84615F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(223, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 29);
            this.label2.TabIndex = 4;
            this.label2.Text = "Icidence rates";
            // 
            // MortChart
            // 
            chartArea4.Name = "ChartArea1";
            this.MortChart.ChartAreas.Add(chartArea4);
            legend4.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend4.Name = "Legend1";
            this.MortChart.Legends.Add(legend4);
            this.MortChart.Location = new System.Drawing.Point(698, 67);
            this.MortChart.Margin = new System.Windows.Forms.Padding(4);
            this.MortChart.Name = "MortChart";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.MortChart.Series.Add(series4);
            this.MortChart.Size = new System.Drawing.Size(649, 356);
            this.MortChart.TabIndex = 5;
            this.MortChart.Text = "chart1";
            this.MortChart.Click += new System.EventHandler(this.MortChart_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.84615F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(957, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(172, 29);
            this.label3.TabIndex = 6;
            this.label3.Text = "Mortality rates";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.84615F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(564, 427);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(222, 29);
            this.label4.TabIndex = 7;
            this.label4.Text = "Stages distribution";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // DiagChart
            // 
            chartArea3.Name = "ChartArea1";
            this.DiagChart.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.DiagChart.Legends.Add(legend3);
            this.DiagChart.Location = new System.Drawing.Point(332, 460);
            this.DiagChart.Margin = new System.Windows.Forms.Padding(4);
            this.DiagChart.Name = "DiagChart";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.DiagChart.Series.Add(series3);
            this.DiagChart.Size = new System.Drawing.Size(797, 307);
            this.DiagChart.TabIndex = 8;
            this.DiagChart.Text = "chart1";
            // 
            // Demo
            // 
            this.Demo.Controls.Add(this.DemoChart);
            this.Demo.Location = new System.Drawing.Point(4, 25);
            this.Demo.Margin = new System.Windows.Forms.Padding(4);
            this.Demo.Name = "Demo";
            this.Demo.Padding = new System.Windows.Forms.Padding(4);
            this.Demo.Size = new System.Drawing.Size(1357, 721);
            this.Demo.TabIndex = 0;
            this.Demo.Text = "Demographics";
            this.Demo.UseVisualStyleBackColor = true;
            // 
            // DemoChart
            // 
            chartArea6.Name = "ChartArea1";
            this.DemoChart.ChartAreas.Add(chartArea6);
            this.DemoChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend6.Name = "Legend1";
            this.DemoChart.Legends.Add(legend6);
            this.DemoChart.Location = new System.Drawing.Point(4, 4);
            this.DemoChart.Margin = new System.Windows.Forms.Padding(4);
            this.DemoChart.Name = "DemoChart";
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.DemoChart.Series.Add(series6);
            this.DemoChart.Size = new System.Drawing.Size(1349, 713);
            this.DemoChart.TabIndex = 1;
            this.DemoChart.Text = "chart1";
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
            this.fitResults.Location = new System.Drawing.Point(4, 25);
            this.fitResults.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.fitResults.Name = "fitResults";
            this.fitResults.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.fitResults.Size = new System.Drawing.Size(1357, 721);
            this.fitResults.TabIndex = 6;
            this.fitResults.Text = "Control";
            this.fitResults.UseVisualStyleBackColor = true;
            // 
            // tbLog
            // 
            this.tbLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tbLog.Location = new System.Drawing.Point(25, 34);
            this.tbLog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.Size = new System.Drawing.Size(391, 684);
            this.tbLog.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Log";
            // 
            // bFit
            // 
            this.bFit.Location = new System.Drawing.Point(439, 30);
            this.bFit.Margin = new System.Windows.Forms.Padding(4);
            this.bFit.Name = "bFit";
            this.bFit.Size = new System.Drawing.Size(100, 28);
            this.bFit.TabIndex = 2;
            this.bFit.Text = "Fit";
            this.bFit.UseVisualStyleBackColor = true;
            this.bFit.Click += new System.EventHandler(this.bFit_Click);
            // 
            // bSim
            // 
            this.bSim.Location = new System.Drawing.Point(439, 80);
            this.bSim.Margin = new System.Windows.Forms.Padding(4);
            this.bSim.Name = "bSim";
            this.bSim.Size = new System.Drawing.Size(100, 28);
            this.bSim.TabIndex = 3;
            this.bSim.Text = "Simulate";
            this.bSim.UseVisualStyleBackColor = true;
            this.bSim.Click += new System.EventHandler(this.bSim_Click);
            // 
            // lParams
            // 
            this.lParams.AutoSize = true;
            this.lParams.Location = new System.Drawing.Point(564, 12);
            this.lParams.Name = "lParams";
            this.lParams.Size = new System.Drawing.Size(78, 16);
            this.lParams.TabIndex = 5;
            this.lParams.Text = "Parameters";
            // 
            // bEdit
            // 
            this.bEdit.Location = new System.Drawing.Point(439, 133);
            this.bEdit.Margin = new System.Windows.Forms.Padding(4);
            this.bEdit.Name = "bEdit";
            this.bEdit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bEdit.Size = new System.Drawing.Size(100, 28);
            this.bEdit.TabIndex = 6;
            this.bEdit.Text = "Edit Params";
            this.bEdit.UseVisualStyleBackColor = true;
            this.bEdit.Click += new System.EventHandler(this.bEdit_Click);
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
            this.dgvParams.Location = new System.Drawing.Point(567, 34);
            this.dgvParams.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvParams.Name = "dgvParams";
            this.dgvParams.RowHeadersWidth = 30;
            this.dgvParams.RowTemplate.Height = 28;
            this.dgvParams.Size = new System.Drawing.Size(784, 683);
            this.dgvParams.TabIndex = 7;
            this.dgvParams.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // ParamValue
            // 
            this.ParamValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ParamValue.HeaderText = "Value";
            this.ParamValue.MinimumWidth = 8;
            this.ParamValue.Name = "ParamValue";
            // 
            // ParamName
            // 
            this.ParamName.HeaderText = "Parameter";
            this.ParamName.MinimumWidth = 8;
            this.ParamName.Name = "ParamName";
            this.ParamName.Width = 170;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.fitResults);
            this.tabControl1.Controls.Add(this.Demo);
            this.tabControl1.Controls.Add(this.Incid);
            this.tabControl1.Controls.Add(this.Surv);
            this.tabControl1.Controls.Add(this.Saved);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1290, 805);
            this.tabControl1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1290, 805);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Saved.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SaveChart)).EndInit();
            this.Surv.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SurvChart)).EndInit();
            this.Incid.ResumeLayout(false);
            this.Incid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IncChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MortChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DiagChart)).EndInit();
            this.Demo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DemoChart)).EndInit();
            this.fitResults.ResumeLayout(false);
            this.fitResults.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage Saved;
        private System.Windows.Forms.DataVisualization.Charting.Chart SaveChart;
        private System.Windows.Forms.TabPage Surv;
        private System.Windows.Forms.DataVisualization.Charting.Chart SurvChart;
        private System.Windows.Forms.TabPage Incid;
        private System.Windows.Forms.DataVisualization.Charting.Chart DiagChart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataVisualization.Charting.Chart MortChart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataVisualization.Charting.Chart IncChart;
        private System.Windows.Forms.TabPage Demo;
        private System.Windows.Forms.DataVisualization.Charting.Chart DemoChart;
        private System.Windows.Forms.TabPage fitResults;
        private System.Windows.Forms.DataGridView dgvParams;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParamName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParamValue;
        private System.Windows.Forms.Button bEdit;
        private System.Windows.Forms.Label lParams;
        private System.Windows.Forms.Button bSim;
        private System.Windows.Forms.Button bFit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.TabControl tabControl1;
    }
}

