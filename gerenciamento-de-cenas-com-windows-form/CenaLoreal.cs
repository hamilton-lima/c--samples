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
    class CenaLoreal : Cena
    {

        System.Windows.Forms.Timer timer;

        Image image;
        Point point;

        public CenaLoreal(GerenciadorCena d)
            : base(d)
        {
        
        
        }

        public override void start()
        {
            image = new Bitmap("images/LogoLoreal.jpg");
            point = new Point(0, 0);

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 3000;
            timer.Tick += new EventHandler(adeusAmigos);
            timer.Enabled = true;
        }

        private void adeusAmigos(object sender, EventArgs e)
        {
            timer.Enabled = false;
            dono.inicioJogo();
        }

        public override void draw(Graphics g)
        {
            g.DrawImage(image, point);
        }

    }
}
