namespace OOP
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");
			Rettangolo rettangolo1 = new Rettangolo(10, 40);
			int area = rettangolo1.AreaRettangolo();

			Rettangolo rettangolo2 = new Rettangolo(100, 3);
			int area2 = rettangolo2.AreaRettangolo();

			List<Rettangolo> rettangoli = new List<Rettangolo>();
			rettangoli.Add(rettangolo1); // [0] 10
			rettangoli.Add(rettangolo2); // [1] 1000
			rettangoli.Add(rettangolo2); // [2] 1000
			rettangoli[1].BaseRettangolo = 1000;
			rettangolo2.BaseRettangolo = 1000;

			foreach (Rettangolo r in rettangoli)
			{
				Console.WriteLine(r.BaseRettangolo);
			}

			Dictionary<string, Rettangolo> rettangoliDizionario = new Dictionary<string, Rettangolo>();
			rettangoliDizionario.Add("RettangoloPippo", rettangolo1);
			rettangoliDizionario["RettangoloPluto"] = rettangolo2;

			foreach (var item in rettangoliDizionario.Values)
			{

			}

			rettangoliDizionario["RettangoloPluto"].BaseRettangolo = 200;

			Dictionary<string, List<Rettangolo>> tantiRettangoli = new Dictionary<string, List<Rettangolo>>();

			rettangolo2.DefinisciBase(200);
		}
	}
}
