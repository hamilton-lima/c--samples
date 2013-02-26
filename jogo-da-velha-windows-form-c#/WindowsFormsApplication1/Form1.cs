using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Button[,] buttons;
        String currentText = "X";

        public Form1()
        {
            buttons = new Button[3, 3];
            InitializeComponent();

            buttons[0, 0] = button1;
            buttons[0, 1] = button2;
            buttons[0, 2] = button3;

            buttons[1, 0] = button4;
            buttons[1, 1] = button5;
            buttons[1, 2] = button6;

            buttons[2, 0] = button7;
            buttons[2, 1] = button8;
            buttons[2, 2] = button9;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button localButton = (Button)sender;
            
            if (localButton.Text.Length == 0)
            {
                localButton.Text = currentText;
                if (currentText.Equals("X"))
                {
                    currentText = "O";
                }
                else
                {
                    currentText = "X";
                }
            }

            String winner = getWinner();
            if (winner.Length > 0)
            {
                MessageBox.Show("O vencedor eh " + winner);
                resetGame();
            }
            else
            {
                if ( ! hasEmpty())
                {
                    MessageBox.Show("Velha !!");
                    resetGame();
                }
            }

        }

        private void resetGame()
        {
            for (int n = 0; n < 3; n++)
            {
                for (int m = 0; m < 3; m++)
                {
                    buttons[n, m].Text = "";
                }
            }
        }

        private bool hasEmpty()
        {
            for (int n = 0; n < 3; n++)
            {
                for (int m = 0; m < 3; m++)
                {
                    if (buttons[n, m].Text.Length == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private String getWinner()
        {

            // teste horizontal
            for (int n = 0; n < 3; n++)
            {
                if (buttons[n, 0].Text.Length > 0 &&
                    buttons[n, 0].Text.Equals(buttons[n, 1].Text) &&
                    buttons[n, 1].Text.Equals(buttons[n, 2].Text))
                {
                    return buttons[n, 0].Text;
                }
            }

            // teste vertical
            for (int n = 0; n < 3; n++)
            {
                if (buttons[0,n].Text.Length > 0 &&
                    buttons[0,n].Text.Equals(buttons[1,n].Text) &&
                    buttons[1,n].Text.Equals(buttons[2,n].Text))
                {
                    return buttons[0,n].Text;
                }
            }            
    
            // diagonal esquerda
            if (buttons[0, 0].Text.Length > 0 &&
                    buttons[0, 0].Text.Equals(buttons[1, 1].Text) &&
                    buttons[1, 1].Text.Equals(buttons[2, 2].Text))
            {
                return buttons[0, 0].Text;
            }

            // diagonal direita
            if (buttons[0, 2].Text.Length > 0 &&
                    buttons[0, 2].Text.Equals(buttons[1, 1].Text) &&
                    buttons[1, 1].Text.Equals(buttons[2, 0].Text))
            {
                return buttons[0, 2].Text;
            }

            return "";
        }
    }
}
