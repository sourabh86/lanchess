using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using chess;

//namespace WindowsFormsApplication1
namespace chess
{
    public partial class Frmwelcome : Form
    {
        public Frmwelcome()
        {
            InitializeComponent();
        }       

        private void button3_Click(object sender, EventArgs e)
        {
            MainForm tplyr = new MainForm(null,false);
            tplyr.Show();
            this.Hide();
        }

        private void btnconnectlan_Click(object sender, EventArgs e)
        {
            FormIP frmip = new FormIP();
            frmip.Show();
            this.Hide();
        }

       
    }
}
