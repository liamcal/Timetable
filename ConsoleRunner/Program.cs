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
				RunInteractive(schoolContext);
			}
		}

		static void RunInteractive(SchoolContext schoolContext)
		{
			Console.WriteLine("--------Interactive Timetable--------");
			Console.WriteLine("Options:");
			Console.WriteLine("\tVIEW <studentName>");
			Console.WriteLine("\tENROL <studentName> <subjectCode>");
			Console.WriteLine("\tDROP <studentName> <subjectCode>");
			Console.WriteLine("\tQUIT");
			Console.Write("> ");

			var input = Console.ReadLine().Trim();
			var components = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			var command = components.First().ToUpperInvariant();

			while (command != "QUIT")
			{
				if (command == "VIEW")
				{
					var student = schoolContext.Students.SingleOrDefault(s => s.Name == components[1]);
					if (student == null)
					{
						Console.WriteLine($"Student {components[1]} not found");
					}
					else
					{
						PrintTimeTableForStudent(schoolContext, student);
					}
				}

				else if (command == "ENROL")
				{
					var student = schoolContext.Students.SingleOrDefault(s => s.Name == components[1]);
					var subject = schoolContext.Subjects.SingleOrDefault(s => s.Code == components[2]);

					if (student == null)
					{
						Console.WriteLine($"Student {components[1]} not found");
					}
					else if (subject == null)
					{
						Console.WriteLine($"Subject {components[2]} not found");
					}
					else
					{
						var enrolment = schoolContext.Enrolments.SingleOrDefault(e => e.StudentId == student.Id && e.SubjectId == subject.Id);

						if (enrolment != null)
						{
							Console.WriteLine($"{components[1]} is already enroleld in {components[2]}");
						}
						else
						{
							schoolContext.Add(new Enrolment()
							{
								Student = student,
								Subject = subject,
							});
							schoolContext.SaveChanges();
							Console.WriteLine($"{components[1]} now enrolled in {components[2]}");
						}
					}
				}

				else if (command == "DROP")
				{
					var student = schoolContext.Students.SingleOrDefault(s => s.Name == components[1]);
					var subject = schoolContext.Subjects.SingleOrDefault(s => s.Code == components[2]);

					if (student == null)
					{
						Console.WriteLine($"Student {components[1]} not found");
					}
					else if (subject == null)
					{
						Console.WriteLine($"Subject {components[2]} not found");
					}
					else
					{
						var enrolment = schoolContext.Enrolments.SingleOrDefault(e => e.StudentId == student.Id && e.SubjectId == subject.Id);

						if (enrolment == null)
						{
							Console.WriteLine($"{components[1]} is not yet enrolled in {components[2]}");
						}
						else
						{
							schoolContext.Remove(enrolment);
							schoolContext.SaveChanges();
							Console.WriteLine($"{components[1]} dropped enrolment in {components[2]}");
						}
					}
				}

				else
				{
					Console.WriteLine("Unrecognized command");
				}

				Console.Write("> ");

				input = Console.ReadLine().Trim();
				components = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
				command = components.First().ToUpperInvariant();
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
