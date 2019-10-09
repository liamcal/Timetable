using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
	public class Student
	{
		public Student()
		{
			Enrolments = new HashSet<Enrolment>();
		}

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Email { get; set; }

		public string Degree { get; set; }

		public virtual ICollection<Enrolment> Enrolments { get; set; }
	}
}
