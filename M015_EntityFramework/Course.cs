using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M015_EntityFramework
{
	public class Course
	{
		public int CourseId { get; set; }
		public string Name { get; set; }
		
		public CourseImage CourseImage { get; set; } // 1:1

		public List<Student> Students { get; set; } // N:N
	}

	public class CourseImage
	{
		public int CourseImageId { get; set; }
		public byte[] Image { get; set; }
		public string Caption { get; set; }

		public int CourseId { get; set; } // 1:1
		public Course Course { get; set; }
	}
}
