using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace chatclient
{
    public partial class networking : Form
    {
        private TcpClient client;
        private NetworkStream ons;
        private StreamReader osr;
        private StreamWriter osw;
        private TcpListener listener;
        private Socket socket;
        private bool isclient = true;
        public networking()
        {
            InitializeComponent();            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (isclient)
                {
                    string s;
                    osw.WriteLine(textBox2.Text);
                    osw.Flush();
                    s = osr.ReadLine();
                    textBox1.Text += s;
                }
                else
                {
                    if (socket.Connected)
                    {
                        string line = osr.ReadLine();
                        textBox1.Text += line;
                        osw.WriteLine(textBox2.Text);
                        osw.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                client = new TcpClient(textBox1.Text, 1234);
                ons = client.GetStream();
                osr = new StreamReader(ons);
                osw = new StreamWriter(ons);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No Active Server Present.Attempting to create server");
                isclient = false;
                try
                {
                    listener = new TcpListener(1234);
                    listener.Start();
                    socket = listener.AcceptSocket();
                    ons = new NetworkStream(socket);
                    osr = new StreamReader(ons);
                    osw = new StreamWriter(ons);
                    if (socket.Connected)
                        MessageBox.Show("Connected");
                }
                catch (Exception ext)
                {
                    MessageBox.Show(ext.Message);
                }
            }
        }
    }
}