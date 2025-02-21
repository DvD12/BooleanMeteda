namespace Eventi
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Counter c = new Counter(new Random().Next(10));

			// Qui dico COSA SUCCEDE quando viene sollevato un evento, ma non determino quando viene sollevato

			// ESECUZIONE: commentare l'opzione 1 o l'opzione 2

			// OPZIONE 1: mi iscrivo all'evento ThresholdReached con una funzione
			c.ThresholdReached += c_ThresholdReached; // Qui mi registro, mi iscrivo per ascoltare l'evento ThresholdReached con la funzione c_ThresholdReached
			// OPZIONE 2: associo al delegate Callback una funzione (ma solo una!)
			//c.Callback = () => Console.ForegroundColor = ConsoleColor.Red;

			Console.WriteLine("press 'a' key to increase total");
			while (Console.ReadKey(true).KeyChar == 'a')
			{
				Console.WriteLine($"adding one ({c.Total}/{c.Threshold})");
				c.Add(1);
			}
		}

		static void c_ThresholdReached(object sender, EventArgs e)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			//Console.WriteLine("The threshold was reached.");
			//Environment.Exit(0);
		}
	}

	class Counter
	{
		public int Threshold { get; private set; }
		public int Total { get; private set; }

		// OPZIONE 1: stabilisco l'esistenza di un evento
		public event EventHandler ThresholdReached; // Qui stabilisco l'esistenza di un evento
		
		// OPZIONE 2: stabilisco l'esistenza di una funzione che può essere associata dall'esterno
		public Action Callback { get; set; } // Alternativa all'evento, ma non è possibile avere più di un metodo che si registra per ascoltarlo

		public Counter(int passedThreshold)
		{
			Threshold = passedThreshold;
			Console.WriteLine($"Threshold set to {Threshold}");
		}

		// Qui dico QUANDO SOLLEVARE l'evento, ma non cosa succede una volta sollevato

		public void Add(int x)
		{
			Total += x;
			if (Total >= Threshold)
			{
				// OPZIONE 1: sollevo l'evento ThresholdReached
				// Qui dichiaro che in questo punto devo sollevare l'evento ThresholdReached, richiamando tutti i metodi che si sono registrati per ascoltarlo
				ThresholdReached?.Invoke(this, EventArgs.Empty);
				
				// OPZIONE 2: richiamo il Callback
				//Callback(); // oppure Callback?.Invoke();
			}
		}
	}
}
