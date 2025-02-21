using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWPF.Models
{
	public class Post
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }


		public static List<Post> Posts; // lista di dati che supponiamo verrà "da qualche parte" (API? Database?)
	}
}
