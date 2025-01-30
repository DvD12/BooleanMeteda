namespace Applicazione1
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
	}
}
