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
    class CenaDeJogo : Cena
    {
        int speedX = 128;
        int speedY = 128;

        Image image;
        Image image2;

        SoundPlayer soundHit;
        SoundPlayer soundBounce;

        Point point;
        Point mousePoint;

        public CenaDeJogo(GerenciadorCena d):base(d)
        {
        }

        public override void update(double deltaTime) {

            Console.WriteLine("update da cena de jogo");
            Point target = new Point(point.X, point.Y);

            target.X += (int)(speedX * deltaTime);
            target.Y += (int)(speedY * deltaTime);

            if (target.X + image.Width > dono.dimensao.Width || target.X < 0)
            {
                soundBounce.Play();
                speedX *= -1;
            }
            else
            {
                point.X = target.X;
            }

            if (target.Y + image.Height > dono.dimensao.Height || target.Y < 0)
            {
                soundBounce.Play();
                speedY *= -1;
            }
            else
            {
                point.Y = target.Y;
            }

            Rectangle from = new Rectangle(point, image.Size);
            Rectangle to = new Rectangle(mousePoint, image2.Size);

            if (from.IntersectsWith(to))
            {
                soundHit.Play();
            }
        
        }

        public override void start()
        {
            image = new Bitmap("images/personagem.png");
            image2 = new Bitmap("images/red.jpg");

            soundHit = new SoundPlayer("sounds/42899__freqman__canon-dos-d30-no-focus.wav");
            soundBounce = new SoundPlayer("sounds/13959__adcbicycle__7.wav");

            soundHit.Load();
            soundBounce.Load();

            point = new Point(0, 0);
            mousePoint = new Point(0, 0);
        }

        public override void draw(Graphics g)
        {
            g.DrawImage(image2, mousePoint);
            g.DrawImage(image, point);
        }

        public override void mouseMove(MouseEventArgs e)
        {
            mousePoint.X = e.X - (image2.Width / 2);
            mousePoint.Y = e.Y - (image2.Height / 2);
        }

    }
}
