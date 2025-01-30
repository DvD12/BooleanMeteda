using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eccezioni
{
	public class ArgomentoNonValidoException : Exception
	{
		public string Messaggio2 { get; set; }
		public ArgomentoNonValidoException() : base()
		{

		}

		public ArgomentoNonValidoException(string message, string message2) : base(message)
		{
			Messaggio2 = message2;
		}
	}
}
