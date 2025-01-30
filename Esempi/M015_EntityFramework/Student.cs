using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace M015_EntityFramework
{
	[Table("student")]
	[Index(nameof(Email), IsUnique = true)]
	public class Student
	{
		[Key]
		public int StudentId { get; set; }
		public string Name { get; set; }
		public string? Surname { get; set; }
		public string? Email { get; set; }
		//public DateTime? DataUltimaInterrogazione { get; set; }

		public List<Review> Reviews { get; set; } // 1:N

		public List<Course> Courses { get; set; } // N:N

		// EF ha bisogno di un costruttore vuoto (posso non specificarlo fintantoché non ne creo uno con parametri,
		// dal momento che, in assenza di costruttori, il compilatore ne crea uno vuoto implicitamente)
		public Student()
		{

		}
	}
}
