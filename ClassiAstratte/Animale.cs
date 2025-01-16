using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassiAstratte
{
	public abstract class Animale
	{
		public static int Contatore { get; private set; }

		public Animale()
		{
			Contatore++;
		}

		public abstract void FaiVerso();

		public void Dormi()
		{
			Console.WriteLine("zzz");
		}
	}

	public class Mucca : Animale
	{
		public override void FaiVerso()
		{
			Console.WriteLine("Mmmumumu");
		}
	}
	public class Pesce : Animale
	{
		public override void FaiVerso()
		{
			Console.WriteLine("Blublulbubb");
		}
	}
}
