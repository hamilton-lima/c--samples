using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Collections;

namespace ChatServer
{
    class Program
    {

        static void Main(string[] args)
        {
            Server server = new Server();
            server.run();
        }

    }
}
