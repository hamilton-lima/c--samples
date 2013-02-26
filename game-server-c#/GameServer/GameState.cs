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

    class GameState
    {
        const int MAX_PLAYERS = 2;
        const int EMPTY_CELL = 2;

        int lastId = 0;
        ClientProcessor[] players;
        int[] board;
        public String [] playerChar = { "X", "O", " " };

        bool ready = false;
        int turn = 0;

        public bool isReady(){
            return ready;
        }

        public GameState() {
            players = new ClientProcessor[MAX_PLAYERS];
            board = new int[9];
            for (int n = 0; n < board.Length; n++)
            {
                board[n] = EMPTY_CELL;
            }
        }

        public void addPlayer(TcpClient client)
        {
            if (ready)
            {
                disconectPlayer(client);
            }

            // gera identificador do cliente
            int id = nextId();
            Console.WriteLine("Novo cliente id = " + id);

            // cria novo thread para cuidar do novo cliente
            players[id] = new ClientProcessor(this, id, client);
            Thread thread = new Thread(players[id].run);
            thread.Start();

            if (id +1 >= MAX_PLAYERS)
            {
                ready = true;
            }

        }

        private void disconectPlayer(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write("server cheio");
            writer.WriteLine();
            writer.Flush();

            client.Close();
        }

        public int nextId() {
            return lastId++;
        }

        bool waiting = false;

        public void play()
        {
            if (!waiting) {
                readyToPlay();
                waiting = true;
            }

        }

        private void readyToPlay()
        {
            String winner = checkVictory();

            if ( winner == null)
            {

                Console.WriteLine("Notify players");
                players[turn].sendReadyToPlay();
                foreach (ClientProcessor processor in players)
                {
                    if (processor.id != turn)
                    {
                        processor.sendWait();
                    }
                }
            }
            else {
                foreach (ClientProcessor processor in players)
                {
                    processor.sendWinner(winner);
                }
            }

        }

        int[,] victoryPosition = {
                                 {0,1,2},
                                 {3,4,5},
                                 {6,7,8},
                                 {0,3,6},
                                 {1,4,7},
                                 {2,5,8},
                                 {0,4,8},
                                 {2,4,6}
                                 };


        private string checkVictory()
        {
            String result = null;

            for (int n = 0; n < victoryPosition.GetLength(0); n++)
            {
                result = checkBoard(victoryPosition[n,0], victoryPosition[n,1], victoryPosition[n,2]);
                if (result != null)
                {
                    return result;
                }
            }
            return result;
        }

        private string checkBoard(int p, int p_2, int p_3)
        {
            if (board[p] != EMPTY_CELL && board[p] == board[p_2] && board[p] == board[p_3])
            {
                return playerChar[board[p]];
            }
            return null;
        }

        internal void addMoveFromPlayer(int _id, string data)
        {
            Console.WriteLine("addmove from " + _id + " data=" + data );

            if ( _id == turn && waiting)
            {
                // converte jogada para posicao no tabuleiro de 0 a 9
                int pos = Int16.Parse(data);
                if (board[pos] == EMPTY_CELL)
                {
                    board[pos] = turn;

                    StringBuilder boardState = new StringBuilder();
                    for (int n = 0; n < board.Length;n++ )
                    {
                        boardState.Append(playerChar[board[n]]);
                    }

                    // notifica todos do tabuleiro atual
                    foreach (ClientProcessor processor in players)
                    {
                        processor.sendBoard(boardState.ToString());
                    }

                    changeTurn();
                    waiting = false;
                }
                else
                {
                    Console.WriteLine("jogada invalida");
                    players[_id].sendInvalidMove();
                }
             }
        }

        private void changeTurn()
        {
            if (turn + 1 >= MAX_PLAYERS)
            {
                turn = 0;
            }
            else
            {
                turn++;
            }
        }
    }
}
