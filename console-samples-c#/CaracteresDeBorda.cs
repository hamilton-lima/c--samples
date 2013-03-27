using System;
using System.Text;

namespace Ascii
{
	class CaracteresDeBorda
	{
		static void catalogo ()
		{
			int itensPorLinha = 15;
			char n = '\u2500';
			int contador = 1;
	
			while( n < '\u2600' ){
				
				// veja mais sobre formatacao em : 
				// http://msdn.microsoft.com/en-us/library/dwhawy9k.aspx
				// neste caso o :X formata a saida em hexadecimal
				//
				Console.Write( "{0:X}={1} ", (int)n, n );
				
				// controla quantos caracteres por linha
				if( contador >= itensPorLinha){
					contador = 1;
					Console.WriteLine("");
				} else {
					contador ++;
				}

				n++;
			}

			
				
		}
		
		// veja mais em : http://en.wikipedia.org/wiki/Box-drawing_character
		//
		static void caixa(int largura, int altura){
			
			int linhasDoMeio = altura - 2;
			
			// linha superior
			Console.Write( "\u256D"  );
			for(int i=0;i< largura; i++){
				Console.Write( "\u2500"  );
			}
			Console.Write( "\u256E"  );
			Console.Write( "\n"  );	
			
			// meio
			for(int n=0;n< linhasDoMeio; n++){
				Console.Write( "\u2502"  );
				for(int i=0;i< largura; i++){
					Console.Write( " "  );
				}
				Console.Write( "\u2502"  );			
				Console.Write( "\n"  );	
			}
			
			// linha inferior
			Console.Write( "\u2570"  );
			for(int i=0;i< largura; i++){
				Console.Write( "\u2500"  );
			}
			Console.Write( "\u256F"  );
			Console.Write( "\n"  );	
			
		}
		
		public static void Main (string[] args)
		{
			Console.OutputEncoding = System.Text.Encoding.Unicode;
			
			catalogo();
			Console.WriteLine();
			caixa(40,5);
		}
	}
	
	
}
