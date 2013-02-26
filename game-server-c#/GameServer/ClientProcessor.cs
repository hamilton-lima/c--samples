using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace GameServer
{
    class ClientProcessor
    {
        public int id{
            get { return _id; }
            set { }
        }

        GameState gameState;
        TcpClient client;
        int _id;
        StreamReader reader = null;
        StreamWriter writer = null;

        public ClientProcessor(GameState gameState, int id, TcpClient client) {
            this.gameState = gameState;
            this._id = id;
            this.client = client;

            NetworkStream stream = client.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
        }

        public void run() {

            String data = null;

            // envia id para cliente
            send("Jogando com " + gameState.playerChar[_id]);

            // faz leitura de uma linha do cliente
            data = reader.ReadLine();

            // Loop to receive all the data sent by the client. 
            while (data != null)
            {
                Console.WriteLine( _id + ": Recebido = " + data);
                gameState.addMoveFromPlayer(_id, data); 
                data = reader.ReadLine();
            }

            // Shutdown and end connection
            client.Close();

        }

        void send(String text)
        {
            Console.WriteLine(_id + " send: [" + text + "]");

            writer.Write(text);
            writer.WriteLine();
            writer.Flush();
        }

        public void sendReadyToPlay()
        {
            send("play");
        }

        public void sendWait()
        {
            send("wait");
        }

        public void sendBoard(string p)
        {
            send("board:" + p);
        }

        public void sendInvalidMove()
        {
            send("invalid");
        }

        public void sendWinner(string winner)
        {
            send("winner:" + winner);
        }
    }
}
