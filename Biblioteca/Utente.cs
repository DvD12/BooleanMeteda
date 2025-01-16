using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
	public class Utente
	{
		public string Nome { get; private set; }
		public string Cognome { get; private set; }
		public string Email { get; private set; }
		public string Password { get; private set; }
		public string Telefono { get; private set; }
	}
}
