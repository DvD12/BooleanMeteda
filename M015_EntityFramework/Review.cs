using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M015_EntityFramework
{
	public class Review
	{
		public int ReviewId { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }

		public int StudentId { get; set; } // 1:N -> riferimento allo studente
		public Student Student { get; set; }
	}
}
