﻿namespace Applicazione1
{
    internal class Program
    {
		static void Main(string[] args)
		{
			//int numero = null; // int è tipo valore, non ammette null
			int? numero1 = null;

			string stringa = null;
			string? stringa1 = null;

			int volteStampate = 0;

			void StampaNumero(int numero)
			{
				volteStampate++;
				Console.WriteLine(numero);
			}

			StampaNumero(1);
			StampaNumero(100);
			StampaNumero(42);
		}

		public void StampaNumero(int num)
		{
			Console.WriteLine(num);
		}

		public int Restituisci42()
		{
			return 42;
		}

		public string SommaInStringa(int n1, int n2)
		{
			return (n1 + n2).ToString();
		}
	}
}
