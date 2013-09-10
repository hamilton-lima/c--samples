using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Servidor2
{
    class Conexao
    {
        private System.Net.Sockets.TcpClient client;
        private StreamReader reader;
        private StreamWriter writer;
        private Server server;
        public bool feio = false;
        public int x;
        public int y;

        public int vx;
        public int vy;
        
        public Conexao(System.Net.Sockets.TcpClient client, 
            Server server)
        {
            this.client = client;
            this.server = server;
            this.x = -1;
            this.y = -1;
            this.vx = 0;
            this.vy = 0;

            Stream s = client.GetStream();
            writer = new StreamWriter(s);
            reader = new StreamReader(s);
        }

        char[] separador = { ',' }; 
        public void run()
        {
            try
            {
                String linha = reader.ReadLine();
                
                while (linha != null && !feio)
                {
                    Console.WriteLine(">>" + linha);
                    
                    if (linha.StartsWith("click="))
                    {
                        String coordenadas = linha.Replace("click=", "");
                        string[] coordenadasA = coordenadas.Split(separador);
                        int x1 = Convert.ToInt32(coordenadasA[0]);
                        int y1 = Convert.ToInt32(coordenadasA[1]);

                        if (this.x < 0)
                        {
                            this.x = x1;
                            this.y = y1;
                        }
                        else {
                            this.vx = x1 - x;
                            this.vy = y1 - y;
                        }
                    }
                    
                    linha = reader.ReadLine();
                }
            }
            catch (Exception e)
            {
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
