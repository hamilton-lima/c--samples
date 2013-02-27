using System;

namespace ConsoleExemplos
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Informe os dados de 3 depesas");
			
			Despesa a = lerDespesa ();
			Despesa b = lerDespesa ();
			Despesa c = lerDespesa ();
			
			Console.WriteLine ("");
			listarDespesas (a, b, c);
			Console.ReadKey();
		}
		
		public static Despesa lerDespesa ()
		{
			Despesa x = new Despesa ();
			Console.Write ("data : ");
			x.data = Console.ReadLine ();
			Console.Write ("valor : ");
			x.valor = Console.ReadLine ();
		
			return x;
		}
		
		public static void listarDespesas(Despesa a, Despesa b, Despesa c){
			Console.WriteLine("data\tvalor");
			Console.WriteLine( a.data + "\t" + a.valor );
			Console.WriteLine( b.data + "\t" + b.valor );
			Console.WriteLine( c.data + "\t" + c.valor );
		}
	
	}

	class Despesa
	{
		public string data;
		public string valor;
	}
}
