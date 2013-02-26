using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace GameClient
{
    public partial class Form1 : Form
    {

        TcpClient client;
        StreamReader reader = null;
        StreamWriter writer = null;
        Button[] board;

        public Form1()
        {
            InitializeComponent();

            board = new Button[9];
            board[0] = button0;
            board[1] = button1;
            board[2] = button2;
            board[3] = button3;
            board[4] = button4;
            board[5] = button5;
            board[6] = button6;
            board[7] = button7;
            board[8] = button8;

            foreach (Button button in board)
            {
                button.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int port = Int32.Parse(textBoxPort.Text);
                client = new TcpClient(textBoxServer.Text, port);

                NetworkStream stream = client.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);
                Thread thread = new Thread(checkMessage);
                thread.Start();


            }
            catch (Exception err)
            {
                MessageBox.Show("Erro : " + err.Message);
            }

        }

        /**
         * Verifica cada mensagem
         * 
         * @see Invoke : http://msdn.microsoft.com/en-us/library/zyzhdc6b.aspx 
         * @see MethodInvoker : http://msdn.microsoft.com/en-us/library/system.windows.forms.methodinvoker.aspx
         * @see delegate : http://msdn.microsoft.com/en-us/library/900fyy8e(v=VS.71).aspx
         */
        private void checkMessage()
        {
            try
            {
                // faz leitura de uma linha do cliente
                String data = reader.ReadLine();

                while (data != null)
                {

                    this.Invoke((MethodInvoker)delegate
                    {
                        reactToMessage(data);
                    });

                    // aguarda proximos dados
                    data = reader.ReadLine();
                }

                // Shutdown and end connection
                client.Close();

            }
            catch (Exception err)
            {
                MessageBox.Show("Erro lendo do servidor : " + err.Message);
            }


        }

        private void reactToMessage(string data)
        {

            if (data.Equals("play"))
            {
                panelGame.Enabled = true;
                labelMessage.Text = data;
            }

            if (data.Equals("wait"))
            {
                panelGame.Enabled = false;
                labelMessage.Text = data;
            }

            if (data.StartsWith("board:"))
            {
                String boardData = data.Replace("board:", "");
                for (int n = 0; n < boardData.Length; n++)
                {
                    if (boardData[n] != ' ')
                    {
                        board[n].Enabled = false;
                        board[n].Text = boardData[n].ToString();
                    }
                }
            }

            if (data.StartsWith("winner:"))
            {
                String boardData = data.Replace("winner:", "");
                labelMessage.Text = "Fim de jogo " + boardData + " ganhou";
                panelGame.Enabled = false;
            }


        }

        private void sendMovement_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.Enabled = false;

            writer.Write(button.Tag.ToString());
            writer.WriteLine();
            writer.Flush();
        }

    }
}
