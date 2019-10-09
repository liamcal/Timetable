using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
	public class Subject
	{
		public Subject()
		{
			Lessons = new HashSet<Lesson>();
			Enrolments = new HashSet<Enrolment>();
		}

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		[Required]
		public string Code { get; set; }

		[Required]
		public string Name { get; set; }

		public string Description { get; set; }

		public virtual ICollection<Lesson> Lessons { get; set; }

		public virtual ICollection<Enrolment> Enrolments { get; set; }
	}
}
