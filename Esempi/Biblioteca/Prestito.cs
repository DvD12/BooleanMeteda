using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
	public class Prestito
	{
		public DateTime InizioPrestito { get; private set; }
		public DateTime FinePrestito { get; private set; }
		//public Utente Utente { get; private set; }
		//public Documento Documento { get; private set; }
		public string DocumentoCodice { get; private set; }
		public string NomeUtente { get; private set; }
		public string CognomeUtente { get; private set; }
	}
}
