using System;
using System.Linq;
using Model;
using Model.Data;
using Model.Models;

namespace ConsoleRunner
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var schoolContext = new SchoolContext())
			{
				DataGenerator.CreateSampleSchoolData(schoolContext);

				var students = schoolContext.Students.Where(s => s.Degree == "Bachelor of Computer Science").Take(3).ToList();
				var subjects = schoolContext.Subjects.Take(3).ToList();

				schoolContext.Add(TimetableHelper.EnrolStudentInSubject(students[0], subjects[0]));
				schoolContext.Add(TimetableHelper.EnrolStudentInSubject(students[0], subjects[1]));
				schoolContext.Add(TimetableHelper.EnrolStudentInSubject(students[1], subjects[1]));
				schoolContext.Add(TimetableHelper.EnrolStudentInSubject(students[1], subjects[2]));
				schoolContext.Add(TimetableHelper.EnrolStudentInSubject(students[2], subjects[0]));

				schoolContext.SaveChanges();

				PrintTimeTableForStudent(schoolContext, students[0]);
				PrintTimeTableForStudent(schoolContext, students[1]);
				PrintTimeTableForStudent(schoolContext, students[2]);

				//RunInteractive(schoolContext);
			}
		}

		static void RunInteractive(SchoolContext schoolContext)
		{
			Console.WriteLine("--------Interactive Timetable--------");
			Console.WriteLine("Options:");
			Console.WriteLine("\tVIEW <studentName>");
			Console.WriteLine("\tENROL <studentName> <subjectCode>");
			Console.WriteLine("\tUNENROL <studentName> <subjectCode>");
			Console.WriteLine("\tQUIT");
			Console.WriteLine("> ");

			var input = Console.ReadLine().Trim();
			var components = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			var command = components.First().ToUpperInvariant();

			while (command != "QUIT")
			{
				if (command == "VIEW")
				{

					var student = schoolContext.Students.Single(s => s.Name == components[1]);
					var lessons = TimetableHelper.GetLessonsForStudent(schoolContext, student);
				}
			}

		}

		static void PrintTimeTableForStudent(SchoolContext schoolContext, Student student)
		{
			Console.WriteLine($"{"Name:",-10}{student.Name}");
			Console.WriteLine($"{"Email:",-10}{student.Email}");
			Console.WriteLine($"{"Degree:",-10}{student.Degree}");
			Console.WriteLine("Timetable:");
			var lessons = TimetableHelper.GetLessonsForStudent(schoolContext, student);

			foreach (var lesson in lessons)
			{
				Console.WriteLine($"{lesson.Subject.Code} {lesson.LessonType}: {lesson.StartTime} - {lesson.EndTime}, Room: {lesson.RoomNumber}, Teacher: {lesson.Teacher.Name}");
			}
			Console.WriteLine(new string('-', 60));

			//foreach (var student in students)
			//{
			//	Console.WriteLine($"{"Id:",-10}{student.Id}");
			//	Console.WriteLine($"{"Name:",-10}{student.Name}");
			//	Console.WriteLine($"{"Email:",-10}{student.Email}");
			//	Console.WriteLine($"{"Degree:",-10}{student.Degree}");
			//	Console.WriteLine(new string('-', 60));
			//}
		}
	}
}
