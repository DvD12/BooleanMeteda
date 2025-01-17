using System.Data.SqlTypes;

namespace Eccezioni
{
	internal class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Console.WriteLine("Sto facendo. 1..");
				Console.WriteLine("Sto facendo 2...");
				Console.WriteLine("Sto facendo 3...");
				Console.WriteLine("Sto facendo. 4..");
				try
				{
					Oggetto o = new Oggetto();
					Console.WriteLine(o.Nome);
					Console.WriteLine("Oggetto trovato");

					// Perché usare più catch? Per gestire diversamente diversi tipi di errore. Per esempimo:

					// Caso 1: tento di mandare in esecuzione un comando SQL errato: "SELECT * FROM miaTabellaCheNonEsiste"
					// Possibile gestione del caso sintassi non corretta: loggare l'errore e mandare una mail all'amministratore

					// Caso 2: mando in esecuzione un comando SQL ma genererà un time-out
					// Possibile gestione del caso timeout: ritenta fino a un massimo di volte

					try
					{
						// Lancio il comando SQL
					}
					catch (TimeoutException ex)
					{
						// Ritenta...
					}
					catch (SqlTypeException ex)
					{
						// Fai log e manda mail etc.
					}
					catch (Exception ex)
					{
						// Fai log e manda mail etc.
					}

				}
				catch (ArgumentNullException ex)
				{
					Console.WriteLine(ex);
				}
				finally
				{
					// Anche se questo try-catch non riesce a gestire la mia eccezione, prima di saltare al try/catch superiore, eseguo il codice qui dentro
					Console.WriteLine("Vengo eseguito sempre e comunque");
				}
				Console.WriteLine("Sto facendo 5...");
				Console.WriteLine("Sto facendo 6...");
				Console.WriteLine("Sto facendo 7...");
				Console.WriteLine("Sto facendo 8...");
				Console.WriteLine("Sto facendo. 9..");
			}
			catch (Exception ex)
			{

			}
			finally
			{

			}

			Console.WriteLine("Sto facendo. 10..");

			try
			{
				Oggetto o = new Oggetto();
				o.Nome = "MioOggetto";
				if (o.Nome == "MioOggetto")
				{
					// Anziché aspettare che il nostro programma generi un'eccezione, lo possiamo fare noi tramite il throw
					throw new ArgomentoNonValidoException($"Non puoi chiamare questo oggetto {o.Nome}", "Ciao, sono un'eccezione speciale");
				}
			}
			catch (ArgomentoNonValidoException e)
			{
				Console.WriteLine(e.Messaggio2);
			}
			catch (Exception e)
			{
				Console.WriteLine(e); // Non posso accedere alle proprietà particolari della mia classe eccezione personalizzata
			}
		}
	}

	public class Oggetto
	{
		public string Nome { get; set; } // default = null
	}
}
