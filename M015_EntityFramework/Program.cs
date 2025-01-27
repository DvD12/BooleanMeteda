namespace M015_EntityFramework
{
	internal class Program
	{
		static void Main(string[] args)
		{
			using (SchoolContext db = new SchoolContext())
			{
				Student s = new Student(); // Modo "standard" di creare un oggetto: invocazione del costruttore come un metodo qualunque
				Student s2 = new(); // Modo "abbreviato" di creare un oggetto: invocazione del costruttore come un metodo qualunque, ma senza specificare il nome della classe
				Student s3 = new() // Prima chiamo il costruttore come al solito, POI specifico i valori delle proprietà come coppie nome = valore
				{
					Name = "Tizio",
					Surname = "Caio",
					Email = "blblblbl@blbl.com"
				};
				Student s4 = new Student // Come sopra, ma senza dover specificare le parentesi tonde (richiamo il costruttore vuoto)
				{
					Name = "Tizio",
					Surname = "Caio",
					Email = "blblblbl@blbl.com"
				};

				// Create
				Student nuovoStudente = new Student();
				nuovoStudente.Name = "Francesco";

				db.Add(nuovoStudente);
				db.SaveChanges();

				// Read
				Console.WriteLine("Recupero lista di Studenti");
				List<Student> students = db.Students.OrderBy(student => student.Name).ToList<Student>();
			}
		}
	}
}
