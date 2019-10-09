﻿using System;
using System.Collections.Generic;
using System.Linq;
using Model.Data;
using Model.Models;

namespace Model
{
	public class TimetableHelper
	{
		public static List<Lesson> GetLessonsForStudent(SchoolContext context, Student student)
		{
			var subjects = context.Subjects.Where(subject => subject.Enrolments.Any(enrolment => enrolment.StudentId == student.Id));
			return subjects.SelectMany(subject => subject.Lessons).ToList();
		}

		public static Enrolment EnrolStudentInSubject(Student student, Subject subject)
		{
			if (subject == null)
			{
				throw new ArgumentNullException(nameof(subject));
			}

			if (subject == null)
			{
				throw new ArgumentNullException(nameof(subject));
			}

			return new Enrolment()
			{
				Student = student,
				Subject = subject,
			};
		}
	}
}