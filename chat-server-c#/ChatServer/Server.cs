using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Collections;

namespace ChatServer
{
    class Server
    {

        private bool estouAfimdeTrabalhar = true;
        private ArrayList conexoes;

        public void run()
        {

            TcpListener listener = new TcpListener(45000);
            listener.Start();
            conexoes = new ArrayList();

            while (estouAfimdeTrabalhar)
            {
                TcpClient client = listener.AcceptTcpClient();
                Conexao c = new Conexao(client, this);
                conexoes.Add(c);

                Thread laranja = new Thread(c.run);
                laranja.Start();
            }

        }

        public void contarPraTodoMundo(string name, string linha)
        {
            foreach (Conexao c in conexoes)
            {
                if (!c.feio)
                {
                    c.send(name + " diz : " + linha );
                }
            }

            int i = 0;
            while (i < conexoes.Count)
            {
                Conexao cnx = (Conexao)conexoes[i];
                if (cnx.feio)
                {
                    conexoes.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }
    }
}
