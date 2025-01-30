using Microsoft.EntityFrameworkCore;

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

				// Read (senza riferimenti)
				Console.WriteLine("Recupero lista di Studenti");
				List<Student> students = db.Students.OrderBy(student => student.Name).ToList<Student>();
				Student primoStudente = students.First();

				// COME GESTISCO I NULL?

				// Caso 1: non lo gestisco!
				{
					//Review review = primoStudente.Reviews.FirstOrDefault(); // -> errore!
					// diverrebbe        <qualcuno>.null.FirstOrDefault() // Stiamo chiamando un metodo/proprietà su null -> ArgumentNullException
				}
				// Caso 2: "wrappo" le istruzioni che potrebbero dereferenziare un null in un if che mi garantisca l'esistenza dell'oggetto a cui tento di accedere
				if (primoStudente.Reviews != null)
				{
					Review review = primoStudente.Reviews.FirstOrDefault();
				}
				// Caso 3: uso l'operatore null-conditional
				{
					// ?. ==> Se l'oggetto alla sua sx, primoStudente.Reviews, è null, restituisce null; altrimenti, va a dx: restituisce il risultato di FirstOrDefault()
					Review review = primoStudente.Reviews?.FirstOrDefault();

					// equivarrebbe all'uso dell'operatore ternario: A ? B : C (è vero A? Allora prendi B. Altrimenti, prendi C)
					Review r = primoStudente.Reviews == null ? null : primoStudente.Reviews.FirstOrDefault();
				}

				// Caso 4: uso l'operatore null-coalescing
				{
					// ?? ==> Se l'oggetto alla sua sx, primoStudente.Reviews, è null, restituisce l'oggetto alla dx;
					// altrimenti, va a sx: restituisce il risultato di FirstOrDefault()
					Review review = primoStudente.Reviews?.FirstOrDefault() ?? new Review();
					review ??= new Review(); // Equivalente all'uso dell'operatore null-coalescing
				}

				// Read (con riferimenti)
				{
					List<Student> studentsConReviews = db.Students.Where(x => true)
																  .Include(s => s.Reviews)
																  .OrderBy(student => student.Name)
																  .ToList<Student>();
					Student primoStudenteConReview = studentsConReviews.First();
					Review review = primoStudenteConReview.Reviews.FirstOrDefault();
					//                  <qualcuno>.<lista vuota (qualcuno)>.FirstOrDefault() -> mi restituisce null, ma non dà errore, perché tutta questa "catena" di oggetti passa per oggetti reali, non-null
				}

				// Update
				nuovoStudente.Name = "Francesco II";
				db.SaveChanges();

				// Delete
				db.Remove(nuovoStudente);
				db.SaveChanges();

				List<Student> list = db.Students.Where(x => x.Name == "Francesco").ToList();
				Student studente = db.Students.Where(x => x.Name == "Francescosdfsdfsdf").FirstOrDefault();
				//Student studente = list.FirstOrDefault();
				list.Where(x => x.Name == "asd");
			}
		}
	}
}
