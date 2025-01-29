namespace M017_AsyncAwait
{
	internal class Program
	{
		// ciclo for
		// in mezzo a questo ciclo for ho un await a una chiamata API
		/*
		 * Task t = ChiamataAdApiEsterna();
		 * long j = 0;
		 * for (long i = 0; i < dieci miliardi; i++)
		 * {
		 *     j += i;
		 *     Console.WriteLine(i);
		 *
		 * }
		 * await t;
		 * 
		*/
		static async Task Main(string[] args)
		{
			string choice = "";
			while (choice != "q")
			{
				// Facciamo notare che il momento della stampa di queste cose
				// (quindi l'esecuzione di questo blocco di codice)
				// cambia a seconda di cosa selezioniamo
				// Se facciamo await aspettiamo, altrimenti andremo avanti
				// se il task chiamato deve ritornare al chiamante perché in attesa
				Extensions.WriteDebugLine("");
				Extensions.WriteDebugLine("===================================");
				Extensions.WriteDebugLine("0) Prepara colazione SINCRONA");
				Extensions.WriteDebugLine("1) Prepara colazione (CON AWAIT)");
				Extensions.WriteDebugLine("2) Prepara colazione (SENZA AWAIT)");
				Extensions.WriteDebugLine("===================================");
				Extensions.WriteDebugLine("");
				choice = Console.ReadLine();
				switch (choice)
				{
					case "0":
						BreakfastSlowExample.PreparaColazione();
						break;
					case "1":
						await BreakfastExample.PreparaColazione();
						break;
					case "2":
						BreakfastExample.PreparaColazione();
						break;
					case "3":
						Task t = BreakfastExample.PreparaColazioneStretta();
						Console.WriteLine("Stampa cose");
						for (int i = 0; i < 20; i++)
						{
							Console.WriteLine(t.IsCompleted);
							await Task.Delay(1000);
						}
						BreakfastSlowExample.PreparaColazione();
						break;
				}
			}
		}
	}
}
