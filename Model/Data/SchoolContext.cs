using Microsoft.EntityFrameworkCore;
using Model.Models;

namespace Model.Data
{
	public class SchoolContext : DbContext
	{
		public SchoolContext()
			: base()
		{
		}

		public SchoolContext(DbContextOptions options) 
			: base(options)
		{
		}

		public DbSet<Student> Students { get; set; }
		public DbSet<Enrolment> Enrolments { get; set; }
		public DbSet<Staff> Staffs { get; set; }
		public DbSet<Lesson> Lessons { get; set; }
		public DbSet<Subject> Subjects { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=School;Persist Security Info=True;User Id=sa; Password=chooseabetterpassword123");
			}
			optionsBuilder.UseLazyLoadingProxies();
		}

	}
}
