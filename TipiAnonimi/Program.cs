namespace TipiAnonimi
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var tizio = new
			{
				Nome = "Mario",
				Cognome = "Rossi",
				Eta = new
				{
					Anni = 30,
					Mesi = 5
				}
			};
			Console.WriteLine(tizio.Nome);
		}
	}
}
