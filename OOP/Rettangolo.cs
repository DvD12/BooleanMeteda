using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
	public class Punto
	{
		public int X { get; set; }
		public int Y { get; set; }

		public void Trasla() { } // ...
	}
	public class Rettangolo
	{

		private int altezzaRettangolo;
		private int AltezzaRettangolo
		{
			get
			{
				return this.altezzaRettangolo;
			}
			set
			{
				if (altezzaRettangolo <= 0)
				{
					altezzaRettangolo = 1;
				}
				this.altezzaRettangolo = value;
			}
		}

		public int BaseRettangolo { get; set; }


		public int AreaRettangolo()
		{
			int area = this.BaseRettangolo * this.AltezzaRettangolo;
			return area;
		}

		public int PerimetroRettangolo()
		{
			int perimetro = (this.BaseRettangolo * 2) + (this.AltezzaRettangolo * 2);
			return perimetro;
		}

		public void DefinisciBase(int baseR)
		{
			this.BaseRettangolo = baseR;
		}

		public Rettangolo(int b, int a)
		{
			BaseRettangolo = b;
			AltezzaRettangolo = a;
		}
	}
}
