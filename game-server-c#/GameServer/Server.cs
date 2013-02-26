using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace GameServer
{
    class Server
    {

        static void Main(string[] args)
        {
            TcpListener listener = null;
            int port = 50000;
            GameState gameState = new GameState();
            bool working = true;

            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                listener = new TcpListener(localAddr, port);
                listener.Start();

                while (working)
                {
                    if (!gameState.isReady())
                    {
                        Console.WriteLine("Aguardando conexoes");

                        // aguarda nova conexao
                        TcpClient client = listener.AcceptTcpClient();
                        gameState.addPlayer(client);
                    }
                    else
                    {
                        // o jogo comecou
                        gameState.play();
                    }
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("Erro de rede: {0}", e);
            }
            finally
            {
                listener.Stop();
            }

        }
    }
}
