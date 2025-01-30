using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statici
{
	public class Auto
	{
		public string Marca { get; private set; } = "";
		public string Modello { get; private set; }
		public static int Contatore { get; private set; }
		public static List<Auto> AutoInMemoria { get; private set; }
		public Auto(string marca, string modello)
		{
			Auto.Contatore++;
			this.Marca = marca;
			this.Modello = modello;
			AutoInMemoria.Add(this);
		}

		static Auto()
		{
			AutoInMemoria = new List<Auto>();
		}

		public static void StampaTutteLeAuto()
		{
			foreach (Auto a in AutoInMemoria)
			{
				Console.WriteLine(a.Marca + " " + a.Modello);
			}
		}
	}
}
