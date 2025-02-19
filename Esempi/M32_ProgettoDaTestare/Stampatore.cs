using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M32_ProgettoDaTestare
{
    public class Stampatore
    {
		public int MessaggiStampati { get; private set; }

        public string Stampa(string messaggio)
		{
			Console.WriteLine(messaggio);
			MessaggiStampati++;
			return messaggio;
		}

		public string StampaCiao()
		{
			return Stampa("Ciao!");
		}

		public string StampaDueParole(string parola1, string parola2)
		{
			if (string.IsNullOrWhiteSpace(parola1))
				return Stampa(parola2);
			if (string.IsNullOrWhiteSpace(parola2))
				return Stampa(parola1);
			return Stampa(parola1 + " " + parola2);
		}

		public string StampaAlContrario(string messaggio)
		{
			return Stampa(new string(messaggio.Reverse().ToArray()));
		}
	}
}
