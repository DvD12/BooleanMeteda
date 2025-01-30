using System.Data.SqlTypes;

namespace Biblioteca
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Biblioteca biblio = new Biblioteca(utenti: new List<Utente>());
			Documento d = new Libro("L001", "Il signore degli anelli", 1954, TipoSettore.Fantascienza, "A", "J.R.R.", "Tolkien", 1000);
			Documento d1 = new Libro(titolo: "Il signore degli anelli",
				codice: "L001",
				anno: 1954,
				settore: TipoSettore.Fantascienza,
				scaffale: "A",
				nomeAutore: "J.R.R.",
				cognomeAutore: "Tolkien",
				numeroPagine: 1000);

			biblio.AggiungiDocumento(d);

			Func<Documento, bool> criterioPerCodiceL001 = (doc) =>
			{
				return doc.Codice == "L00112341";
			};
			bool risposta = criterioPerCodiceL001(d);
			bool risposta2 = criterioPerCodiceL001(d1);

			Documento d1234 = biblio.Ricerca((d) =>
			{
				return d.Codice == "L001";
			});

			Documento d12345 = biblio.Ricerca((d) => d.Codice == "L001");

			biblio.Documenti.FirstOrDefault(doc => doc.Codice == "L001");
			biblio.Documenti.FirstOrDefault(doc => doc.Titolo == "L002" && doc.Codice == "asd");

			IEnumerable<Documento> mieiDocumenti = biblio.Filtra((doc) => doc.Titolo == "Il signore degli anelli").ToList();
			IEnumerable<Documento> mieiDocumenti2 = new HashSet<Documento>(mieiDocumenti);
			List<IEnumerable<Documento>> gruppiDiDocumenti = new List<IEnumerable<Documento>>();
			gruppiDiDocumenti.Add(mieiDocumenti);
			gruppiDiDocumenti.Add(mieiDocumenti2);
			foreach (IEnumerable<Documento> gruppo in gruppiDiDocumenti)
			{
				foreach (Documento doc in gruppo)
				{
					Console.WriteLine(doc.ToString());
				}
			}

			var list = biblio.Documenti.Where(doc => doc.CognomeAutore == "Tolkien");

			Documento documentoTrovato1 = biblio.RicercaDocumentoPerCodice("L001");
			if (documentoTrovato1 != null)
			{
				Console.WriteLine($"Documento trovato: {documentoTrovato1.ToString()}");
			}
			else
			{
				Console.WriteLine($"Documento non trovato");
			}
			
			Documento documentoTrovato2 = biblio.RicercaDocumentoPerCodice("L002");
			Console.WriteLine($"Documento trovato: {documentoTrovato2.ToString()}");
		}
	}
}
