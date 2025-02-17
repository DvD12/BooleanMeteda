namespace Delegati
{
	internal class Program
	{
		static void Main(string[] args)
		{
			List<int> numeri = new List<int>();
			for (int i = 0; i < 100; i++)
			{
				numeri.Add(i);
			}
			IEnumerable<int> numeriPari2 = numeri.Where((numero) =>
			{
				return numero % 2 == 0;
			});

			numeri.Add(1000);

			foreach (var numero in numeriPari2)
			{
				Console.WriteLine(numero);
			}

			numeri.Add(1002);

			foreach (var numero in numeriPari2)
			{
				Console.WriteLine(numero);
			}

			IEnumerable<int> numeriIncrementati = numeri.Select((numero) => numero + 1);
			IEnumerable<string> numeriAStringa = numeri.Select((numero) =>
			{
				string risultato = $"Il mio numero è {numero}";
				return risultato;
			});

			List<int> numeriPari = numeri.Where((numero) => numero % 2 == 0).ToList();

			Azienda.Impiegati.Add(new Impiegato { Nome = "Mario", AnniLavoro = 5, Valutazione = 8, Livello = 1 });
			Azienda.Impiegati.Add(new Impiegato { Nome = "Luigi", AnniLavoro = 15, Valutazione = 6, Livello = 1 });
			Azienda.Impiegati.Add(new Impiegato { Nome = "Pippo", AnniLavoro = 10, Valutazione = 9, Livello = 1 });
			Azienda.Impiegati.Add(new Impiegato { Nome = "Pluto", AnniLavoro = 20, Valutazione = 5, Livello = 1 });
			Azienda.Impiegati.Add(new Impiegato { Nome = "Paperino", AnniLavoro = 1, Valutazione = 10, Livello = 1 });
			Azienda.Impiegati.Add(new Impiegato { Nome = "Topolino", AnniLavoro = 30, Valutazione = 4, Livello = 1 });
			Azienda.Impiegati.Add(new Impiegato { Nome = "Minnie", AnniLavoro = 25, Valutazione = 3, Livello = 1 });
			Azienda.Impiegati.Add(new Impiegato { Nome = "Clarabella", AnniLavoro = 2, Valutazione = 2, Livello = 1 });
			Console.WriteLine("Hello, World!");
			Azienda.Promuovi(Azienda.PromuoviPerValutazione);

			Func<int, int, int> Aggiungi = (int numero1, int numero2) =>
			{
				return numero1 + numero2;
			};

			Func<int, int, int> Moltiplica = (int numero1, int numero2) => numero2 * numero1;

			Func<int, string> ConvertiAStringa = (int numero) => numero.ToString();

			int c = Aggiungi(1, 2);
			int d = Moltiplica(2, 4);
			string e = ConvertiAStringa(3);

			Action stampaCiao = () =>
			{
				Console.WriteLine("Ciao");
			};
			Action<int> stampaNumero = (int numero) =>
			{
				Console.WriteLine(numero);
			};

			stampaCiao();
			stampaNumero(1234);



			List<string> list = new List<string> { "ciao", "come", "stai" };
			foreach (string item in list)
			{
				Console.WriteLine(item);
			}
			for (int i = 0; i < list.Count; i++)
			{
				Console.WriteLine(list[i]);
			}
			list.ForEach(x =>
			{
				Console.WriteLine(x);
			}
			);

			// Definisco una funzione anonima
			// Action rappresenta una funzione che non restituisce nulla (void)
			Action stampa = () =>
			{
				Console.WriteLine("Ciao");
			};
			Action<int, int> stampaNumeri = (n1, n2) =>
			{
				Console.WriteLine(n1);
				Console.WriteLine(n2);
			};

			// Func rappresenta una funzione che restituisce un valore
			// In questo caso, non prende niente in input e restituisce 42 in output
			Func<int> restituisci42 = () =>
			{
				return 42;
			};
			// Qui prende due int e restituisce una stringa
			Func<int, int, string> sommaInStringa = (n1, n2) =>
			{
				return (n1 + n2).ToString();
			};

			stampa();
			stampaNumero(2);
			stampaNumeri(2, 3);

			// Tra il chiamare il ForEach con una funzione anonima definita contestualmente come argomento
			// e il chiamarla con una Action<string> definita precedentemente, c'è la stessa differenza che
			// tra chiamare stampaNumero(2)
			// e definire int n = 2
			// e chiamare stampaNumero(n)
			list.ForEach(x =>
			{
				Console.WriteLine(x);
			}
			);

			Action<string> action = s =>
			{
				Console.WriteLine(s);
			};
			list.ForEach(action);
		}

		public static int Aggiungi(int n1, int n2) => n1 + n2;
	}
}
