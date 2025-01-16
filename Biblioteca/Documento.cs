using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
	public enum TipoSettore
	{
		Fantascienza,
		Thriller,
		Giallo,
	}

	public class Documento
	{
		public string Codice { get; private set; }
		public string Titolo { get; private set; }
		public int Anno { get; private set; }
		public TipoSettore Settore { get; private set; }
		public string Scaffale { get; private set; }
		public string NomeAutore { get; private set; }
		public string CognomeAutore { get; private set; }

		public Documento(string codice, string titolo, int anno, TipoSettore settore, string scaffale, string nomeAutore, string cognomeAutore)
		{
			this.Codice = codice;
			this.Titolo = titolo;
			this.Anno = anno;
			this.Settore = settore;
			this.Scaffale = scaffale;
			this.NomeAutore = nomeAutore;
			this.CognomeAutore = cognomeAutore;
		}

		public override string ToString()
		{
			return $"[{Codice}] {Titolo} ({Anno}) - {NomeAutore} {CognomeAutore}";
		}
	}

	public class Libro : Documento
	{
		public int NumeroPagine { get; private set; }

		public Libro(string codice, string titolo, int anno, TipoSettore settore, string scaffale, string nomeAutore, string cognomeAutore, int numeroPagine)
					: base(codice, titolo, anno, settore, scaffale, nomeAutore, cognomeAutore)
		{
			this.NumeroPagine = numeroPagine;
		}
	}

	public class Dvd : Documento
	{
		public int Durata { get; private set; }

		public Dvd(string codice, string titolo, int anno, TipoSettore settore, string scaffale, string nomeAutore, string cognomeAutore, int durata)
					: base(codice, titolo, anno, settore, scaffale, nomeAutore, cognomeAutore)
		{
			this.Durata = durata;
		}
	}
}
