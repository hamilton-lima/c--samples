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
    public class GerenciadorCena
    {
        public Size dimensao;

        public GerenciadorCena(Size dim)
        {
            dimensao = dim;
            cenaAtual = new CenaLoreal(this);
        }

        public void inicioJogo() {
            cenaAtual = new CenaDeJogo(this);
            cenaAtual.start();
        }

        public Cena cenaAtual;
    }
}
