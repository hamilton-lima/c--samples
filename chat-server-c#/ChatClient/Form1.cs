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

namespace ChatClient
{
    public partial class Form1 : Form
    {
        private StreamReader reader;
        private StreamWriter writer;

        public Form1()
        {
            InitializeComponent();

            TcpClient client = new TcpClient("127.0.0.1", 45000);
            Stream s = client.GetStream();
            reader = new StreamReader(s);
            writer = new StreamWriter(s);

            Thread thread = new Thread(this.run);
            thread.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            send(textBoxMensagem.Text);
        }

        public void run()
        {
            String linha = reader.ReadLine();
            while (linha != null)
            {
                Invoke((MethodInvoker)delegate
                {
                    textBoxDialogo.AppendText(linha + "\r\n");
                });
 
                linha = reader.ReadLine();
            }
        }

        public void send(string s)
        {
            writer.Write(s);
            writer.WriteLine();
            writer.Flush();
        }

    
    
    }
}
