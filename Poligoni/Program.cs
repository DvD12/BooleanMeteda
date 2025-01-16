namespace Poligoni
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");

			IPoligono t = GeneraPoligono("rettangolo");
		}

		public static IPoligono GeneraPoligono(string richiesta)
		{
			if (richiesta == "rettangolo")
				return new Rettangolo();
			else
				return new Triangolo();
		}
	}
}
