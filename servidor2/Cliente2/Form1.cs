using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections;

namespace Cliente2
{
    public partial class Form1 : Form
    {
        private ArrayList pontos;
        private ArrayList pontosNovos;
        private StreamReader reader;
        private StreamWriter writer;

        DateTime start;

        Image image;
        bool redeOcupada;
        System.Windows.Forms.Timer timer;

        public Form1()
        {
            pontos = new ArrayList();
            pontosNovos = new ArrayList();
            redeOcupada = false;

            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer 
                | ControlStyles.UserPaint 
                | ControlStyles.AllPaintingInWmPaint, true);

            image = new Bitmap("ponto.bmp");

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 40;
            timer.Tick += new EventHandler(update);
            timer.Enabled = true;

            TcpClient client = new TcpClient("127.0.0.1", 1337);
            Stream s = client.GetStream();
            reader = new StreamReader(s);
            writer = new StreamWriter(s);

            Thread thread = new Thread(this.run);
            thread.Start();

        }

        public void run()
        {
            String linha = reader.ReadLine();
            while (linha != null)
            {
                if (pontosNovos.Count == 0)
                {

                    redeOcupada = true;

                    char[] separador = { ';' };
                    char[] separador1 = { ',' };

                    string[] pontosA = linha.Split(separador);
                    foreach (string ponto in pontosA)
                    {
                        if (ponto.Length > 0) {
                            string[] xy = ponto.Split(separador1);
                            int x = Convert.ToInt32(xy[0]);
                            int y = Convert.ToInt32(xy[1]);
                            pontosNovos.Add(new Point(x, y));
                        }
                            
                    }

                    redeOcupada = false;
                }

                linha = reader.ReadLine();
            }
        }

        public void send(string s)
        {
            writer.Write(s);
            writer.WriteLine();
            writer.Flush();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (Point p in pontos)
            {
                g.DrawImage(image, p);
            }
           
        }

        private void update(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            double deltaTime = (now - start).Milliseconds / 1000.0;
            start = now;

            if (pontosNovos.Count > 0 && !redeOcupada)
            {
                pontos.Clear();
                foreach (Point p in pontosNovos)
                {
                    pontos.Add(p);
                }
                pontosNovos.Clear();
            }

            Invalidate();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            send("click=" + e.X + "," + e.Y);
        }
    }

}
