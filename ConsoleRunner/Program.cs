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
				RunInteractive(schoolContext);
			}
		}

		static void RunInteractive(SchoolContext schoolContext)
		{
			Console.WriteLine("--------Interactive Timetable--------");
			Console.WriteLine("Options:");
			Console.WriteLine("\tLIST [STUDENTS|SUBJECTS]");
			Console.WriteLine("\tVIEW <studentName>");
			Console.WriteLine("\tENROL <studentName> <subjectCode>");
			Console.WriteLine("\tDROP <studentName> <subjectCode>");
			Console.WriteLine("\tQUIT");
			Console.Write("> ");

			(var command, var args) = ParseInput();

			while (command != "QUIT")
			{
				if (command == "LIST")
				{
					List(schoolContext, args[0].ToUpperInvariant());
				}
				else if (command == "VIEW")
				{
					View(schoolContext, args[0]);
				}
				else if (command == "ENROL")
				{
					Enrol(schoolContext, args[0], args[1]);
				}
				else if (command == "DROP")
				{
					Drop(schoolContext, args[0], args[1]);
				}
				else
				{
					Console.WriteLine("Unrecognized command");
				}

				Console.Write("> ");
				(command, args) = ParseInput();
			}
		}

		static void List(SchoolContext context, string table)
		{
			if (table == "STUDENTS")
			{
				foreach (var name in context.Students
					.OrderBy(s => s.Name)
					.Select(s => s.Name))
				{
					Console.WriteLine(name);
				}
			}
			else if (table == "SUBJECTS")
			{
				foreach (var code in context.Subjects
					.OrderBy(s => s.Code)
					.Select(s => s.Code))
				{
					Console.WriteLine(code);
				}
			}
		}

		static void View(SchoolContext schoolContext, string studentName)
		{
			var student = schoolContext.Students.SingleOrDefault(s => s.Name == studentName);
			if (student == null)
			{
				Console.WriteLine($"Student {studentName} not found");
			}
			else
			{
				PrintTimeTableForStudent(schoolContext, student);
			}
		}

		static void Enrol(SchoolContext schoolContext, string studentName, string subjectCode)
		{
			var student = schoolContext.Students.SingleOrDefault(s => s.Name == studentName);
			var subject = schoolContext.Subjects.SingleOrDefault(s => s.Code == subjectCode);

			if (student == null)
			{
				Console.WriteLine($"Student {studentName} not found");
			}
			else if (subject == null)
			{
				Console.WriteLine($"Subject {subjectCode} not found");
			}
			else
			{
				var enrolment = schoolContext.Enrolments.SingleOrDefault(e => e.StudentId == student.Id && e.SubjectId == subject.Id);

				if (enrolment != null)
				{
					Console.WriteLine($"{studentName} is already enroleld in {subjectCode}");
				}
				else
				{
					schoolContext.Add(new Enrolment()
					{
						Student = student,
						Subject = subject,
					});
					schoolContext.SaveChanges();
					Console.WriteLine($"{studentName} now enrolled in {subjectCode}");
				}
			}
		}

		static void Drop(SchoolContext schoolContext, string studentName, string subjectCode)
		{
			var student = schoolContext.Students.SingleOrDefault(s => s.Name == studentName);
			var subject = schoolContext.Subjects.SingleOrDefault(s => s.Code == subjectCode);

			if (student == null)
			{
				Console.WriteLine($"Student {studentName} not found");
			}
			else if (subject == null)
			{
				Console.WriteLine($"Subject {subjectCode} not found");
			}
			else
			{
				var enrolment = schoolContext.Enrolments.SingleOrDefault(e => e.StudentId == student.Id && e.SubjectId == subject.Id);

				if (enrolment == null)
				{
					Console.WriteLine($"{studentName} is not yet enrolled in {subjectCode}");
				}
				else
				{
					schoolContext.Remove(enrolment);
					schoolContext.SaveChanges();
					Console.WriteLine($"{studentName} dropped enrolment in {subjectCode}");
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
		}


		static (string command, string[] args) ParseInput()
		{
			var input = Console.ReadLine().Trim();
			var components = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			var command = components.First().ToUpperInvariant();
			var args = components.Skip(1).ToArray();
			return (command, args);
		}
	}
}
