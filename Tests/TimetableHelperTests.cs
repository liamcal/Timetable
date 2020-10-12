using NUnit.Framework;
using Model;
using Model.Models;
using Tests.Data;

namespace Tests
{
	public class TimetableHelperTests
	{
		[Test]
		public void GetLessonsForStudent()
		{
			var context = InMemoryContextFactory.CreateSchoolContext();

			var testStudent = new Student() { Name = "Testee", Email = "testee@testing.com", Degree = "Bachelor of Testing" };
			var testStaff = new Staff() { Name = "Tester", Email = "tester@testtest.com", JobTitle = "Lead tester" };
			var testSubject1 = new Subject() { Code = "TES101", Name = "Intro to testing", Description = "The basics of good unit testing" };
			var testSubject2 = new Subject() { Code = "TES102", Name = "Testing fundamentals", Description = "The next steps for good unit testing" };
			var testSubject3 = new Subject() { Code = "TES103", Name = "Advanced Testing", Description = "Advanced theory unit testing" };
			var testClass1 = new Lesson() { Subject = testSubject1, Teacher = testStaff, StartTime = "MON 10:00", EndTime = "MON 11:00", LessonType = LessonType.Lecture, RoomNumber = "123" };
			var testClass2 = new Lesson() { Subject = testSubject1, Teacher = testStaff, StartTime = "TUE 10:00", EndTime = "TUE 11:00", LessonType = LessonType.Lecture, RoomNumber = "123" };
			var testClass3 = new Lesson() { Subject = testSubject1, Teacher = testStaff, StartTime = "WED 10:00", EndTime = "WED 11:00", LessonType = LessonType.Lecture, RoomNumber = "123" };
			var testClass4 = new Lesson() { Subject = testSubject2, Teacher = testStaff, StartTime = "MON 11:00", EndTime = "MON 12:00", LessonType = LessonType.Lecture, RoomNumber = "123" };
			var testClass5 = new Lesson() { Subject = testSubject2, Teacher = testStaff, StartTime = "TUE 11:00", EndTime = "TUE 12:00", LessonType = LessonType.Lecture, RoomNumber = "123" };
			var testClass6 = new Lesson() { Subject = testSubject3, Teacher = testStaff, StartTime = "MON 13:00", EndTime = "TUE 14:00", LessonType = LessonType.Lecture, RoomNumber = "123" };

			var enrolment1 = new Enrolment() { Student = testStudent, Subject = testSubject1 };
			var enrolment2 = new Enrolment() { Student = testStudent, Subject = testSubject3 };

			context.Add(testStudent);
			context.Add(testStaff);
			context.AddRange(testSubject1, testSubject2, testSubject3);
			context.AddRange(testClass1, testClass2, testClass3, testClass4, testClass5, testClass6);
			context.AddRange(enrolment1, enrolment2);
			context.SaveChanges();

			Assert.That(TimetableHelper.GetLessonsForStudent(testStudent), Is.EquivalentTo(new[] { testClass1, testClass2, testClass3, testClass6 }), "Student timetable should contain 4 lessons");
		}

		[Test]
		public void GetLessonsForStudent_StudentHasNoEnrolments()
		{
			var context = InMemoryContextFactory.CreateSchoolContext();

			var testStudent = new Student() { Name = "Testee", Email = "testee@testing.com", Degree = "Bachelor of Testing" };
			var testStaff = new Staff() { Name = "Tester", Email = "tester@testtest.com", JobTitle = "Lead tester" };
			var testSubject1 = new Subject() { Code = "TES101", Name = "Intro to testing", Description = "The basics of good unit testing" };
			var testSubject2 = new Subject() { Code = "TES102", Name = "Testing fundamentals", Description = "The next steps for good unit testing" };

			context.Add(testStudent);
			context.Add(testStaff);
			context.AddRange(testSubject1, testSubject2);
			context.SaveChanges();

			Assert.That(TimetableHelper.GetLessonsForStudent(testStudent), Is.Empty, "Student timetable should be empty");
		}

		[Test]
		public void GetLessonsForStudent_SubjectHasNoClasses()
		{
			var context = InMemoryContextFactory.CreateSchoolContext();

			var testStudent = new Student() { Name = "Testee", Email = "testee@testing.com", Degree = "Bachelor of Testing" };
			var testStaff = new Staff() { Name = "Tester", Email = "tester@testtest.com", JobTitle = "Lead tester" };
			var testSubject1 = new Subject() { Code = "TES101", Name = "Intro to testing", Description = "The basics of good unit testing" };
			var testSubject2 = new Subject() { Code = "TES102", Name = "Testing fundamentals", Description = "The next steps for good unit testing" };
	
			var enrolment = new Enrolment() { Student = testStudent, Subject = testSubject1 };

			context.Add(testStudent);
			context.Add(testStaff);
			context.AddRange(testSubject1, testSubject2);
			context.Add(enrolment);
			context.SaveChanges();

			Assert.That(TimetableHelper.GetLessonsForStudent(testStudent), Is.Empty, "Student timetable should be empty");
		}
	}
}
