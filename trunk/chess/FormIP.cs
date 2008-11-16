using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace chess
{
    public partial class FormIP : Form
    {
        public FormIP()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                IPAddress ipa = IPAddress.Parse("192.168.0.1");
                if (IPAddress.TryParse(textBox1.Text, out ipa))
                {
                    MainForm frm1 = new MainForm(textBox1.Text, true);
                    frm1.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Please enter a valid IP-Address");
                }
            }
            else
            {
                MessageBox.Show("Please Enter an IP Address");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainForm frm1 = new MainForm("Server",true);
            frm1.Show();
            this.Hide();
        }
        
    }
}