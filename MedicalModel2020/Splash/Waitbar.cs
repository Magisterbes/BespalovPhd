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
        public Waitbar()
        {
            InitializeComponent();
        }

        public void SetStatusText(string text)
        {
            label2.Text = text;
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }
    }
}
