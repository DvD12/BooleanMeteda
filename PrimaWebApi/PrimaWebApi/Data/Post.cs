using System.ComponentModel.DataAnnotations;

namespace PrimaWebApi.Data
{
	/// <summary>
	/// Classe che rappresenta il modello di un post di un blog.
	/// </summary>
	public class Post
	{
		public int Id { get; set; }
		[StringLength(25, ErrorMessage = "Il titolo non può superare i 25 caratteri")]
		public string Title { get; set; }
		public string Content { get; set; }
		/// <summary>
		/// ? -> La deserializzazione automatica che avviene quando un'API tenta di deserializzare la stringa JSON
		/// nel modello Post farà sì che non venga contato come errore l'eventuale mancanza della proprietà 'Author' nella stringa JSON
		/// </summary>
		public string? Author { get; set; } 

		// Chiave esterna (può essere NULL)
		public int? CategoryId { get; set; }
		public Category? Category { get; set; }
	}
	/*
	public class A
	{
		// A ha un attributo (campo) di tipo B
		private B b;
		// il costruttore di A istanzia B
		public A()
		{
			b = new B();
		}

		// A usa un metodo di B per eseguire un task
		public void Compito()
		{
			b.Method(); // serve b!
		}
	}
	
	*/
	public class B
	{
		public void Method()
		{
			// fa qualcosa
		}
	}
	public class Factory
	{
		// Crea un oggetto di tipo B
		public static B GetObjectOfB()
		{
			return new B();
		}
	}
	public class A
	{
		// A ha un attributo (campo) di tipo B
		private B b;

		public A()
		{
			//b = new B();
			b = Factory.GetObjectOfB();
		}

		public void Compito()
		{
			b.Method();
		}
	}
}
