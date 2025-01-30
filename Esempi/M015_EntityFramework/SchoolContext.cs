using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M015_EntityFramework
{
	public class SchoolContext : DbContext
	{
		public DbSet<Student> Students { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<CourseImage> CourseImages { get; set; }
		public DbSet<Review> Reviews { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=EF_Studenti;Integrated Security=True;TrustServerCertificate=True");
		}
	}
}
