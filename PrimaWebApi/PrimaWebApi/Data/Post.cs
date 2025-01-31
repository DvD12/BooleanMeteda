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
	}
}
