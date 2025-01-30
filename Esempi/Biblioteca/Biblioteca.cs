using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
	public class Biblioteca
	{
		public List<Documento> Documenti { get; private set; } = new List<Documento>();
		public List<Utente> Utenti { get; private set; } = new List<Utente>();
		public List<Prestito> Prestiti { get; private set; } = new List<Prestito>();

		public Biblioteca(List<Documento> documenti = null, List<Utente> utenti = null, List<Prestito> prestiti = null)
		{
			if (documenti != null)
			{
				this.Documenti = documenti;
			}
			if (utenti != null)
			{
				this.Utenti = utenti;
			}
		}

		public void AggiungiDocumento(Documento documento)
		{
			Documenti.Add(documento);
		}

		public Documento Ricerca(Func<Documento, bool> criterio)
		{
			//return Documenti.FirstOrDefault(criterio);
			foreach (Documento doc in Documenti)
			{
				if (criterio(doc))
				{
					return doc; // Ho trovato il documento: esco dalla funzione
				}
			}
			return default; // Non ho trovato niente
		}

		public Documento RicercaDocumentoPerCodice(string codice)
		{
			foreach (Documento doc in Documenti)
			{
				if (doc.Codice == codice)
				{
					return doc; // Ho trovato il documento: esco dalla funzione
				}
			}

			return null; // Non ho trovato niente
		}

		public Documento RicercaDocumentoPerTitolo(string titolo)
		{
			foreach (Documento doc in Documenti)
			{
				if (doc.Titolo == titolo)
				{
					return doc; // Ho trovato il documento: esco dalla funzione
				}
			}

			return null;
		}

		public IEnumerable<Documento> Filtra(Func<Documento, bool> criterio)
		{
			return Documenti.Where(criterio);
		}
	}
}
