using System;
using System.Collections;
using System.Text;

namespace Prova2012_2
{
	class MainClass
	{
		
		const string QUESTOES = "/Users/hamiltonlima/Dropbox/oi nave/2013/avaliacoes/programacao_c#_questoes_turma_2003_ano_2012_e_2013.txt";
		const string TITULO = "Avaliacao de Tecnicas de programacao 2 - NAVE - turma 2003 - ano 2013 (2Bim)";
		const string PROFESSOR = "Professor Hamilton Lima";
		const string ALERTA = "Coloque seu nome em <b>TODAS</b> as folhas de resposta";
		const int quantidadeProvas = 50;
		const string arquivoSaida = "/Users/hamiltonlima/Dropbox/oi nave/2013/avaliacoes/programacao_c#_questoes_turma_2003_ano_2012_e_2013.html";
		static int[] QUESTOES_PONTOS = {3,3};
		
		public static void Main2 (string[] args)
		{
		
			// carrega questoes
			String[] questoes = carregaQuestoes ();
						
			// sorteia primeiras questoes
			Random random = new Random ();
			int[] ultimas_questoes = {random.Next (questoes.Length),0};
			ultimas_questoes[1] = random.Next (questoes.Length);
			
			int step = questoes.Length / 3;
			ultimas_questoes [1] = (ultimas_questoes [0] + step) % questoes.Length;

			// inicia HTML de saida
			StringBuilder output = new StringBuilder ();
			output.Append ("<html><body>");
			output.Append ("<H1>" + TITULO + "<br> Caderno de Provas</h1>");
				
			// cria uma questao
			for (int n=0; n < quantidadeProvas; n++) {
				criaUmaProva (questoes, output, ultimas_questoes);
			}
			
			// finaliza HTML de saida 
			output.Append ("</body></html>");
			
			// salva arquivo de saida		
			System.IO.File.WriteAllText (arquivoSaida, output.ToString ());
			
		}
		
		/**
		 * cria o HTML necessario para a montagem de uma questao
		 */ 
		public static void criaUmaProva (String[] questoes, StringBuilder output, int[] ultimas_questoes)
		{
		
			output.Append ("<div style=\"page-break-before: always\">");
			output.Append ("<h2>" + TITULO + "</h2>");		
			output.Append ("<b>Nome : _______________________________________________________</b><br>");
			output.Append (ALERTA + "<br>");
			
			int counter = 1;

			for (int n=0; n < ultimas_questoes.Length; n++) {

				output.Append ("<p><b>Questao " + counter + "</b> (peso " + QUESTOES_PONTOS [n] + ")");
				output.Append ("<pre>" + questoes [ultimas_questoes [n]] + "</pre>");					
				output.Append ("</p><br>\n");
					
				// avanca para a proxima questao
				ultimas_questoes [n] = (ultimas_questoes [n] + 1) % questoes.Length;
				counter ++;
									
			}

			output.Append ("</div>\n");			
			
		}
		
		/**
		 * Le questoes do arquivo texto e organiza em um arraylist de arraylists
		 */
		static String[] carregaQuestoes ()
		{
			// cria arraylists para armazenar as questoes separadas por topicos

			
			// le todo o texto
			String allText = System.IO.File.ReadAllText (QUESTOES);
			char[] quebrasParaRemover = { '\n','\r'};
			char[] sep = {'\\'};
				
			// separa cada questao e titulos no array questoes
			String[] questoes = allText.Split (sep);
			
			
			for (int n=0; n < questoes.Length; n++) {

				String linha = questoes [n];
				// identifica as quebras de linha
				questoes [n] = linha.TrimStart (quebrasParaRemover).TrimEnd (quebrasParaRemover);
			}
			
			return questoes;
		}	
		
	
	}
}
