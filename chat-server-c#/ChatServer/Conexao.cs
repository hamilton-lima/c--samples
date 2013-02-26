using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ChatServer
{
    class Conexao
    {
        private System.Net.Sockets.TcpClient client;
        private StreamReader reader;
        private StreamWriter writer;
        private Server server;
        public bool feio = false;
        public String name = "<anonimo>";

        public Conexao(System.Net.Sockets.TcpClient client, Server server)
        {
            this.client = client;
            this.server = server;
            Stream s = client.GetStream();
            writer = new StreamWriter(s);
            reader = new StreamReader(s);
        }

        public void run()
        {
            try
            {
                String linha = reader.ReadLine();
                while (linha != null && !feio)
                {
                    if (linha.StartsWith("nome="))
                    {
                        String novoNome = linha.Replace("nome=", "");
                        name = novoNome;
                    }
                    else
                    {
                        server.contarPraTodoMundo(name,linha);
                    }

                    linha = reader.ReadLine();
                }
            } catch(Exception e){
                feio = true;
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
