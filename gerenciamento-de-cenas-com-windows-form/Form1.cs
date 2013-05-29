using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace Cenas
{
    public partial class Form1 : Form
    {
        GerenciadorCena cacique;

        DateTime start;

        System.Windows.Forms.Timer timer;

        public Form1()
        {
            InitializeComponent();
            Console.WriteLine("---- inicio");
            cacique = new GerenciadorCena(ClientSize);
            cacique.cenaAtual.start();

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 40;
            timer.Tick += new EventHandler(update);
            timer.Enabled = true;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            cacique.cenaAtual.draw(g);
        }

        private void update(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            double deltaTime = (now - start).Milliseconds / 1000.0;
            start = now;

            cacique.cenaAtual.update(deltaTime);

            Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //labelTecla.Text = "tecla : " + e.KeyCode;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            cacique.cenaAtual.mouseMove(e);
        }

    }
}
