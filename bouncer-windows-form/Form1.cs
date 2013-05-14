using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace Gameclient
{
    public partial class Form1 : Form
    {
        DateTime start;
        int speedX = 128;
        int speedY = 128;

        Image image;
        Image image2;

        SoundPlayer soundHit;
        SoundPlayer soundBounce;

        Point point;
        Point mousePoint;

        System.Windows.Forms.Timer timer;

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            image = new Bitmap("images/personagem.png");
            image2 = new Bitmap("images/red.jpg");

            soundHit = new SoundPlayer("sounds/42899__freqman__canon-dos-d30-no-focus.wav");
            soundBounce = new SoundPlayer("sounds/13959__adcbicycle__7.wav");

            soundHit.Load();
            soundBounce.Load();

            point = new Point(0, 0);
            mousePoint = new Point(0, 0);

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 40;
            timer.Tick += new EventHandler(update);
            timer.Enabled = true;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(image, point); 
            g.DrawImage(image2, mousePoint);

        }

        private void update(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            double deltaTime = (now - start).Milliseconds / 1000.0;
            start = now;

            Point target = new Point(point.X, point.Y);

            target.X += (int)(speedX * deltaTime);
            target.Y += (int)(speedY * deltaTime);

            if (target.X + image.Width > ClientSize.Width || target.X < 0)
            {
                soundBounce.Play();
                speedX *= -1;
            } else {
                point.X = target.X;
            }

            if (target.Y + image.Height > ClientSize.Height || target.Y < 0){
                soundBounce.Play(); 
                speedY *= -1;
            } else {
                point.Y = target.Y;
            }

            Rectangle from = new Rectangle(point, image.Size );
            Rectangle to = new Rectangle(mousePoint, image2.Size );

            if (from.IntersectsWith(to)) {
                soundHit.Play();
            }

            labelDebug.Text = "x=" + point.X + " y=" + point.Y;
            Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            labelTecla.Text = "tecla : " + e.KeyCode;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            label2.Text = "mouse=" + e.X + "," + e.Y;

            mousePoint.X = e.X - (image2.Width/2);
            mousePoint.Y = e.Y - (image2.Height/2);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
