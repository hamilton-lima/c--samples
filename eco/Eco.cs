using System;
using System.Net.Sockets;
using System.IO;

namespace Echo
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			bool alive = true;
			
			Console.WriteLine ("Eco eco eco ...");
			TcpListener listener = new TcpListener(45000);
            listener.Start();

			while( alive ){
				TcpClient client = listener.AcceptTcpClient();
				
				Stream s = client.GetStream();
            	StreamWriter writer = new StreamWriter(s);
            	StreamReader reader = new StreamReader(s);
				
				String linha = reader.ReadLine();
				Console.WriteLine ("input : " + linha  );
				writer.Write("eco :" + linha );
	            writer.WriteLine();
    	        writer.Flush();
				
				client.Close();
			}
			
		}
	}
}
