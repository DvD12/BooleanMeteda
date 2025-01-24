namespace M009_GestioneFile
{
	internal class Program
	{
		static void Main(string[] args)
		{
			double aa = 1;
			double bb = 0;
			double cc = aa / bb;
			Console.WriteLine(cc);
			string path = @"C:\Users\DvD\FileDaLeggere.txt";

			// Scrittura su un file
			try
			{
				if (File.Exists(path) == false)
				{
					StreamWriter stream = File.CreateText(path);
					stream.WriteLine("Riga 1");
					stream.Write("Riga 2");
					stream.Write("\r\n");
					stream.Write("Riga 3");

					stream.Close();
				}
			}
			catch (Exception ex)
			{

			}

			// Sintassi alternativa per la chiusura automatica di uno stream
			try
			{
				if (File.Exists(path) == false)
				{
					using (StreamWriter stream = File.CreateText(path)) // Lo using rende superfluo chiamare .Close(): viene automaticamente eseguito al termine dello scope di using
					{
						stream.WriteLine("Riga 1");
						stream.Write("Riga 2");
						stream.Write("\r\n");
						stream.Write("Riga 3");
					}
				}
			}
			catch (Exception ex)
			{

			}
			// Sintassi alternativa per la chiusura automatica di uno stream
			try
			{
				if (File.Exists(path) == false)
				{
					using StreamWriter stream = File.CreateText(path); // Senza le parentesi rendo implicito lo scope dello using
					stream.WriteLine("Riga 1");
					stream.Write("Riga 2");
					stream.Write("\r\n");
					stream.Write("Riga 3");
				}
			}
			catch (Exception ex)
			{

			}

			// Lettura di un file
			try
			{
				StreamReader stream = File.OpenText(path);

				while (stream.EndOfStream == false)
				{
					Console.WriteLine($"Sono in posizione {stream.BaseStream.Position}/{stream.BaseStream.Length}");
					string riga = stream.ReadLine(); // Legge una riga dallo stream e "incrementa" la posizione di lettura, il cursore interno di N caratteri fino a raggiungere una nuova riga
					Console.WriteLine(riga);
				}

				stream.Close();

				// Errore gestito dal secondo blocco catch
				int a = 5;
				int b = 0;
				int c = a / b;
			}
			catch (FileNotFoundException ex)
			{
				Console.WriteLine($"Il file non esiste: {ex.Message}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Errore generico: {ex.Message}");
			}

			using (ClasseDisposable obj = new ClasseDisposable("MioOggetto")) // Chiamerà il metodo Dispose perché la ClasseDisposable implementa IDisposable, e quindi ha senso lo using
			{
				Console.WriteLine("Fai cose 1");
			}

			Console.WriteLine("Fai cose 2");
			Console.WriteLine("Fai cose 3");
		}
	}
}
