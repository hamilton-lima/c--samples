using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gravidade
{
    public partial class Form1 : Form
    {
        DateTime start;

        int PULO_PADRAO = 300;
        int gravidade = 175;
        int velocidade = 175; 
        double pulo = 0;

        Image chao;
        Image red;

        Point posChao;
        Point posRed;
        Rectangle rectangleChao;
        bool direita = false;
        bool esquerda = false;

        System.Windows.Forms.Timer timer;

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            chao = new Bitmap("images/chao.jpg");
            red = new Bitmap("images/red.jpg");

            posRed = new Point(10, 10);
            posChao = new Point(0, ClientSize.Height - chao.Height );
            rectangleChao = new Rectangle(posChao, chao.Size);

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 40;
            timer.Tick += new EventHandler(update);
            timer.Enabled = true;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.DrawImage(chao, posChao);
            g.DrawImage(red, posRed);
        }

        private void update(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            double deltaTime = (now - start).Milliseconds / 1000.0;
            start = now;

            double parteDoPulo = pulo * deltaTime;
            pulo -= parteDoPulo;
            if (pulo < 0)
            {
                pulo = 0;
            }

            Point target = new Point(posRed.X, posRed.Y);
            target.Y += (int)(gravidade * deltaTime);
            target.Y -= (int)(parteDoPulo);
           

            if (direita)
            {
                target.X += (int)(velocidade * deltaTime);
            }

            if (esquerda)
            {
                target.X -= (int)(velocidade * deltaTime);
            }

            Rectangle from = new Rectangle(target, red.Size);

            if (from.IntersectsWith(rectangleChao))
            {
                posRed = new Point(posRed.X, posChao.Y - red.Height); 
            }
            else
            {
                posRed = target;                
            }
            
            Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Space)) {
                pulo = PULO_PADRAO;
            }
            
            if (e.KeyCode.Equals(Keys.Right)){
                direita = true;
            }

            if (e.KeyCode.Equals(Keys.Left))
            {
                esquerda = true;
            }
            
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Right))
            {
                direita = false;
            }

            if (e.KeyCode.Equals(Keys.Left))
            {
                esquerda = false;
            }
        }

        
    }
}
