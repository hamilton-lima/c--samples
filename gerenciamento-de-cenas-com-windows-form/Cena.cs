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
    public class Cena
    {
        public GerenciadorCena dono;

        public Cena(GerenciadorCena d) {
            dono = d;
        }

        public virtual void start() { }

        public virtual void update(double deltaTime) {
        }

        public virtual void draw(Graphics g) { }

        public virtual void mouseMove(MouseEventArgs e) { }

    }
}
