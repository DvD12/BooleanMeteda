namespace M010_Generics
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Node<int> node3 = new Node<int>(5);

			Node<string> node4 = new Node<string>("Hello");

			List<int> list = new List<int>();

			object obj = new object();

			Stampa<string>("Ciao");
			Stampa<int>(2);

			NodeDiNumeri<int> nodo5 = new NodeDiNumeri<int>(5);
			NodeDiNumeri<double> nodo6 = new NodeDiNumeri<double>(5.0);
			//NodeDiNumeri<string> nodo7 = new NodeDiNumeri<string>("Ciao"); // Non lo posso fare!
			nodo6.Aggiungi(4);
		}

		public static bool Empty<T>(IEnumerable<T> collection)
		{
			return collection == null || !collection.Any();
		}

		public static T Stampa<T>(T valore)
		{
			Console.WriteLine(valore.ToString());
			return valore;
		}
	}
}
