namespace M010_Collezioni
{
	internal class Program
	{
		static void Main(string[] args)
		{
			HashSet<string> strings = new HashSet<string>();
			strings.Add("1"); // Internamente, una funzione di hash traduce il valore "1" in un numero che rappresenta internamente la sua posizione (quindi la ricerca è efficiente)
			
			Dictionary<string, int> stringheANumeri = new Dictionary<string, int>();
			stringheANumeri.Add("UNO!!!1!11", 1);
			stringheANumeri.Add("Uno, di nuovo", 1); // è come una funzione: posso associare più chiavi allo stesso valore
			stringheANumeri.Add("DUE", 2);
			//stringheANumeri.Add("Uno, di nuovo", 5); // ERRORE (di runtime)! Non posso aggiungere di nuovo la stessa chiave

			stringheANumeri["Uno, di nuovo"] = 5; // Posso aggiungere/sovrascrivere il valore associato a una chiave
			stringheANumeri["Numero totalmente nuovo"] = 666; // Posso aggiungere nuove chiavi così
			
			foreach (var coppia in stringheANumeri) // "coppia" è KeyValuePair<K,V>: un tipo di dato che rappresenta una coppia di una chiave e il suo corrispondente valore
			{
				Console.WriteLine(coppia.Key + " -> " + coppia.Value);
			}

			foreach (string chiave in stringheANumeri.Keys)
			{
				Console.WriteLine($"Chiave {chiave}");
			}
			foreach (int valore in stringheANumeri.Values)
			{
				Console.WriteLine($"Valore {valore}");
			}

			if (stringheANumeri.ContainsKey("UNO!!!1!11") == true) // Operazione praticamente istantanea nel caso dei dizionari: ricercare le chiave implica solo applicare una funzione di hash, proprio come nel caso degli HashSet
			{
				Console.WriteLine("C'è UNO!!!1!11");
			}

			int numeroTrovato = default;
			if (stringheANumeri.ContainsKey("DUE") == true)
			{
				numeroTrovato = stringheANumeri["DUE"];
			}

			int numeroNonTrovato = default;
			if (stringheANumeri.ContainsKey("TRE") == true)
			{
				numeroNonTrovato = stringheANumeri["TRE"];
			}

			int numeroTrovato2 = default;
			if (stringheANumeri.TryGetValue("DUE", out numeroTrovato2) == true) // Funzione che, tramite "out", associa il valore trovato (oppure default, se non trovato) al parametro in out
			{
				Console.WriteLine("Ho trovato il numero " + numeroTrovato2);
			}

			if (stringheANumeri.TryGetValue("DUE", out int numeroTrovato3) == true) // Posso dichiarare l'esistenza di una variabile (out) direttamente, contestualmente all'interno della chiamata a funzione
			{
				Console.WriteLine("Ho trovato il numero" + numeroTrovato3);
			}
			numeroTrovato3 = 5; // Posso usarla anche dopo

			if (stringheANumeri.ContainsKey("QUATTRO") == false)
			{
				stringheANumeri.Add("QUATTRO", 4);
			}
		}
	}
}
