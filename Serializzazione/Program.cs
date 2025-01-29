using Newtonsoft.Json;

namespace Serializzazione
{
	internal static class Program
	{
		static void Main(string[] args)
		{
			Classe2 c2 = new Classe2();
			c2.Numero2 = 5;
			c2.Stringhe = new[] { "Ciao", "Buongiorno" };

			Classe1 c1 = new Classe1();
			c1.Numero1 = 10;
			c1.Stringa1 = "Ciao";
			c1.MiaClasse = c2;

			// Serializzo l'oggetto c2: serializzare significa trasformare un oggetto in memoria in una sua rappresentazione (testuale) riconvertibile in un oggetto
			// SerializeObject: oggetto --> stringa
			string jsonC2 = JsonConvert.SerializeObject(c2, Formatting.Indented); // Restituisce una stringa che rappresenta l'oggetto c2

			// Deserializzo la stringa JSON per creare un oggetto in memoria a partire dalla sua rappresentazione testuale
			// DeserializeObject: stringa --> oggetto
			Classe2 c2Deserializzato = JsonConvert.DeserializeObject<Classe2>(jsonC2);

			string jsonC1 = JsonConvert.SerializeObject(c1, Formatting.Indented);
			Classe1 c1Deserializzato = JsonConvert.DeserializeObject<Classe1>(jsonC1);

			Classe2 c3 = Clona(c2);
			Classe2 c4 = c2.Clona();

			int x = 5.Clona();
		}

		public static T Clona<T>(this T obj)
		{
			string json = JsonConvert.SerializeObject(obj);
			return JsonConvert.DeserializeObject<T>(json);
		}
	}

	public class Classe1
	{
		public int Numero1 { get; set; }
		public string Stringa1 { get; set; }
		public Classe2 MiaClasse { get; set; }
	}

	public class Classe2
	{
		public int Numero2 { get; set; }
		public string[] Stringhe { get; set; }
	}
}
