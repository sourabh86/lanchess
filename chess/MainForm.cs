using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace chess
{
    public partial class MainForm : Form
    {
        private CFunc comfun;
        public MainForm(string ip,bool n)
        {
            InitializeComponent();
            this.Click += new EventHandler(MainForm_Click);
            this.FormClosed += new FormClosedEventHandler(MainForm_FormClosed);
            comfun = new CFunc(this, ip,n);
        }

        void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //osw.WriteLine("Exit");
            Application.Exit();
        }

        
        void MainForm_Click(object sender, EventArgs e)
        {
            char s = 'A';
            int x = ((((MouseEventArgs)e).X) / 90) + 1;
            for (int i = 0; i < x - 1; i++)
                s++;
            int y = ((((MouseEventArgs)e).Y) / 90) + 1;
            comfun.mainFormClick(s, y);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics gc = e.Graphics;
            Pen redpen = new Pen(Color.Red, 5);
            if (comfun.older != null)
                gc.DrawRectangle(redpen, comfun.older.Left, comfun.older.Top, comfun.older.Width, comfun.older.Height);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comfun.mainFormLoad();
            
        }
    }
}