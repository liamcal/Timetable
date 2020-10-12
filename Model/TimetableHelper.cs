using System.Collections.Generic;
using System.Linq;
using Model.Models;

namespace Model
{
	public static class TimetableHelper
	{
		public static List<Lesson> GetLessonsForStudent(Student student)
		{
			var subjectsForStudent = student.Enrolments.Select(enrolment => enrolment.Subject).Distinct();
			return subjectsForStudent.SelectMany(subject => subject.Lessons).ToList();
		}
	}
}
