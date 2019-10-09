using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
	public enum LessonType { Lecture, Tutorial, Lab }

	public class Lesson
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		[Required]
		public Guid SubjectId { get; set; }

		[ForeignKey(nameof(SubjectId))]
		public virtual Subject Subject { get; set; }

		[Required]
		public Guid TeacherId { get; set; }

		[ForeignKey(nameof(TeacherId))]
		public virtual Staff Teacher { get; set; }

		[Required]
		public string StartTime { get; set; }

		[Required]
		public string EndTime { get; set; }

		[Required]
		public string RoomNumber { get; set; }

		[Required]
		[Column(TypeName = "nvarchar(MAX)")]
		public LessonType LessonType { get; set; }
	}
}
