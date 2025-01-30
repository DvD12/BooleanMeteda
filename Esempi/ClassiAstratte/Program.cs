namespace ClassiAstratte
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");
			Animale a = new Mucca();
			a.FaiVerso();
			Animale p = new Pesce();
			p.FaiVerso();
		}
	}
}
