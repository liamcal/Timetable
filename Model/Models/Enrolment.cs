using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
	public class Enrolment
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		[Required]
		public Guid StudentId { get; set; }

		[ForeignKey(nameof(StudentId))]
		public virtual Student Student { get; set; }

		[Required]
		public Guid SubjectId { get; set; }

		[ForeignKey(nameof(SubjectId))]
		public virtual Subject Subject { get; set; }
	}
}
