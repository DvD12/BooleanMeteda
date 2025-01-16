namespace Applicazione1
{
    internal class Program
    {
		static void Main(string[] args)
		{
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
