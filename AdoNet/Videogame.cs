using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet
{
	public class Videogame
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Overview { get; set; }
		public DateTime ReleaseDate { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public long SoftwareHouseId { get; set; }
	}
}
