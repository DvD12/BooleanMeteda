namespace Inheritance
{
	public class Contenuto
	{
		public virtual string Titolo { get; set; }

		public Contenuto(string t)
		{
			this.Titolo = t;
		}
		public virtual void Riproduci()
		{
			Console.WriteLine($"Stai guardando il contenuto chiamato {Titolo}");
		}

		public override string ToString()
		{
			return $"Contenuto {this.Titolo}";
		}
	}
	public class Serie : Contenuto
	{
		public override string Titolo
		{
			get
			{
				return base.Titolo;
			}
			set
			{
				if (value.Length < 50)
				{
					base.Titolo = value;
				}
			}
		}
		public string[] Cast { get; set; }

		public Serie(string titolo, string[] cast) : base(titolo)
		{
			this.Cast = cast;
		}

		public override void Riproduci()
		{
			Console.WriteLine($"Stai riproducendo la serie {Titolo} con un cast di {this.Cast.Length} attori");
		}
	}
	public class SerieYoutube : Serie
	{
		public string Canale { get; set; }
		public SerieYoutube(string titolo, string[] cast, string canale) : base(titolo, cast)
		{
			this.Canale = canale;
		}
		public override void Riproduci()
		{
			Console.WriteLine($"Stai guardando la serie {Titolo} su YouTube, con un cast di {this.Cast.Length} attori");
		}
	}
	public class Film : Contenuto
	{
		public int Durata { get; set; }
		public Film(string titolo, int durata) : base(titolo)
		{
			this.Durata = durata;
		}
		public override void Riproduci()
		{
			Console.WriteLine($"Stai guardando il film {Titolo} con una durata di {this.Durata} minuti");
		}
	}
	internal class Program
	{
		static void Main(string[] args)
		{
			Contenuto c = new Serie("Breaking Bad", new string[] { "Bryan Cranston", "Aaron Paul" });
			Contenuto y = new SerieYoutube("Breaking Bad", new string[] { "Bryan Cranston", "Aaron Paul" }, "Canale");
			Contenuto f = new Film("Via col vento", 8000);
			Film f1 = new Film("Via col vento", 8000);

			List<Contenuto> contenuti = new List<Contenuto>();
			contenuti.Add(c);
			contenuti.Add(y);
			contenuti.Add(f);
			foreach (var item in contenuti)
			{
				item.Riproduci();
			}

		}
	}
}
