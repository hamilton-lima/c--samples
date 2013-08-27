using System;
using System.Collections;
using System.Text;

namespace Prova2012_2
{
	public class MainCopia
	{
		
		const string QUESTOES = "/Users/hamiltonlima/Dropbox/oi nave/2013/avaliacoes/programacao_c#_questoes_turma_2003_ano_2012_e_2013.txt";
		const string TITULO = "Avaliacao de Programacao de Jogos 2 - NAVE - turma 2003 - ano 2013 (2Bim)";
		const string PROFESSOR = "Professor Hamilton Lima";
		const string ALERTA = "Coloque seu nome em <b>TODAS</b> as folhas de resposta";
		const int quantidadeProvas = 50;
		const string arquivoSaida = "/Users/hamiltonlima/Dropbox/oi nave/2013/avaliacoes/programacao_c#_questoes_turma_2003_ano_2012_e_2013.html";
		
		// indica para cada topico quantas questoes devem ser adicionadas
		// cada posicao do array representa um topico
		static int[] QUESTOES_QUANTIDADE = {2,2,1,1,1};
		
		// indica para cada topico o valor de cada questao
		// cada posicao do array representa um topico
		static int[] QUESTOES_PONTOS = {1,1,2,2,2};
		
		public static void Main (string[] args)
		{
		
			// guarda todas as questoes e nomes dos topicos
			ArrayList lista = new ArrayList();
			ArrayList topicos = new ArrayList();

			// carrega questoes
			carregaQuestoes (lista, topicos);
			Console.WriteLine("topicos : " + topicos.Count);
			
			
			int[] ultimas_questoes = new int[topicos.Count];
			for(int n=0; n < ultimas_questoes.Length; n++){
				ultimas_questoes[n] = 0;
			}			
			
			// sorteia primeiras questoes
			for(int n=0; n < topicos.Count; n++){
				ArrayList inner = (ArrayList)lista[n];
				int max = inner.Count;
				
				Random random = new Random();
				ultimas_questoes[n] = random.Next (max);	
			}
		
			// inicia HTML de saida
			StringBuilder output = new StringBuilder();
			output.Append("<html><body>");
			output.Append("<H1>" + TITULO + "<br> Caderno de Provas</h1>");
				
			// cria uma questao
			for(int n=0; n < quantidadeProvas; n++){
				criaUmaProva(lista,topicos,output, ultimas_questoes);
			}
			
			// finaliza HTML de saida 
			output.Append("</body></html>");
			
			// salva arquivo de saida		
			System.IO.File.WriteAllText(arquivoSaida,output.ToString());
			
		}
		
		/**
		 * cria o HTML necessario para a montagem de uma questao
		 */ 
		public static void criaUmaProva (ArrayList lista, ArrayList topicos, StringBuilder output, int[] ultimas_questoes)
		{
		
			output.Append("<div style=\"page-break-before: always\">");
			output.Append("<img src='cabecalho_prova.png'><br>" );
			//               + TITULO + "</h2>");		
			// output.Append("<b>Nome : _______________________________________________________</b><br>");
			output.Append(ALERTA + "<br>");
			
			int counter = 1;

			for(int n=0; n < topicos.Count; n++){

				ArrayList inner = (ArrayList)lista[n];
				int max = inner.Count;

				for(int i=0; i< QUESTOES_QUANTIDADE[n]; i++){
					
					output.Append( "<p><b>Questao " + counter + "</b> (peso " + QUESTOES_PONTOS[n] + ")");
					output.Append( "<pre>" + inner[ ultimas_questoes[n] ] + "</pre>");					
					output.Append( "</p><br>\n");
					
					// avanca para a proxima questao
					ultimas_questoes[n] = (ultimas_questoes[n]+1) % max;
					counter ++;
				}
					
			}

			output.Append("</div>\n");			
			
		}
		
		/**
		 * Le questoes do arquivo texto e organiza em um arraylist de arraylists
		 */
		static void carregaQuestoes (ArrayList lista, ArrayList topicos)
		{
			// cria arraylists para armazenar as questoes separadas por topicos
			for(int n=0; n < 5; n++){
				lista.Add( new ArrayList());
				topicos.Add ("");
			}
			
			// le todo o texto
			String allText = System.IO.File.ReadAllText(QUESTOES);
			char[] quebrasParaRemover = { '\n','\r'};
			char[] sep = {'\\'};
				
			// separa cada questao e titulos no array questoes
			String[] questoes = allText.Split(sep);
			
			int topico = 0;
			
			for(int n=0; n < questoes.Length; n++){
				String linha = questoes[n];
				Console.WriteLine( linha );
				
				// verifica se eh titulo de topico
				if( linha.Length > 2 && linha.Substring(0,2).Equals("--")){
					String topicoTexto = linha.Substring(2);
					char[] sepTitulo = {'.'};
					String [] corteTitulo = topicoTexto.Split(sepTitulo);
					topico = Convert.ToInt16(corteTitulo[0]);
					topicos[topico-1] = topicoTexto;
				} else {
					// identifica em que lista colocar a questao de acordo com o seu topico
					ArrayList inner = (ArrayList)lista[topico-1];
		
					// identifica as quebras de linha
					inner.Add( linha.TrimStart(quebrasParaRemover).TrimEnd(quebrasParaRemover) );
				}
			}
		}
	}
}

