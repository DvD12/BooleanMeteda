using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegati
{
	public static class Azienda
	{
		public static string Nome { get; set; }
		public static List<Impiegato> Impiegati { get; set; } = new List<Impiegato>();

		public delegate bool CriterioPromozione(Impiegato i);

		public static bool PromuoviPerValutazione(Impiegato imp)
		{
			return imp.Valutazione > 7;
			/*
			foreach (Impiegato i in Impiegati)
			{
				if (i.Valutazione > 7) // Quest'if è una sorta di funzione che ha bisogno di conoscere un Impiegato come input e restituisce un boolean in output
				{
					i.Livello++;
				}
			}
			*/
		}
		public static void PromuoviPerAnzianita()
		{
			foreach (Impiegato i in Impiegati)
			{
				// if (GenericoCriterio(i)) // Questa riga è un esempio di come potremmo usare un delegato per passare una funzione come parametro
				if (i.AnniLavoro > 10)
				{
					i.Livello++;
				}
			}
		}

		public static void Promuovi(CriterioPromozione GenericoCriterio)
		{
			foreach (Impiegato i in Impiegati)
			{
				if (GenericoCriterio(i))
				{
					i.Livello++;
				}
			}
		}
	}
}
