using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Collections;

namespace Servidor2
{
    class Server
    {
        ArrayList conexoes;

        public void comunicaJogadores() {

            while (true)
            {
                StringBuilder builder = new StringBuilder();
                int i = 0;
                while (i < conexoes.Count)
                {
                    Conexao cnx = (Conexao)conexoes[i++];
                    builder.Append(cnx.x);
                    builder.Append(',');
                    builder.Append(cnx.y);
                    builder.Append(';');
                }

                Console.WriteLine("<<" + builder.ToString());

                i = 0;
                while (i < conexoes.Count)
                {
                    Conexao cnx = (Conexao)conexoes[i++];
                    cnx.send(builder.ToString());
                }

                Thread.Sleep(20);
            }
        }

        public void atualizaJogadores() {
            while (true)
            {
                Thread.Sleep(20);


                int i = 0;
                while (i < conexoes.Count)
                {
                    Conexao cnx = (Conexao)conexoes[i++];

                    if (cnx.vx > 0)
                    {
                        cnx.x += 1;
                        cnx.vx -= 1;
                    }

                    if (cnx.vx < 0)
                    {
                        cnx.x -= 1;
                        cnx.vx += 1;
                    }

                    if (cnx.vy > 0)
                    {
                        cnx.y += 1;
                        cnx.vy -= 1;
                    }

                    if (cnx.vy < 0)
                    {
                        cnx.y -= 1;
                        cnx.vy += 1;
                    }

                }
            }
        
        }

        public void executar() {

            TcpListener listener = new TcpListener(1337);
            listener.Start();
            conexoes = new ArrayList();

            Thread atualiza = new Thread(atualizaJogadores);
            atualiza.Start();

            Thread comunica = new Thread(comunicaJogadores);
            comunica.Start();

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                
                Conexao c = new Conexao(client, this);
                conexoes.Add(c);

                Thread laranja = new Thread(c.run);
                laranja.Start();
            }
        
        
        }

    }
}
