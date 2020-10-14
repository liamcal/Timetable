using System.Collections.Generic;
using System.Linq;
using Model.Data;
using Model.Models;

namespace DbBuilder
{
	public static class DataGenerator
	{
		public static void CreateSampleSchoolData(SchoolContext context)
		{
			CreateStudents(context);
			CreateStaff(context);
			CreateSubjects(context);
			context.SaveChanges();

			CreateClasses(context);
			context.SaveChanges();
		}

		static void CreateClasses(SchoolContext context)
		{
			var prg101 = context.Subjects.Single(s => s.Code == "prg101");
			var prg102 = context.Subjects.Single(s => s.Code == "prg102");
			var prg201 = context.Subjects.Single(s => s.Code == "prg201");

			var lecturer = context.Staffs.First(s => s.JobTitle == "Lecturer");
			var tutor = context.Staffs.First(s => s.JobTitle == "Tutor");

			context.AddRange(new List<Lesson>()
			{
				new Lesson()
				{
					Subject = prg101,
					Teacher = tutor,
					StartTime = "TUE 09:00",
					EndTime = "TUE 10:00",
					RoomNumber = "K30",
					LessonType = LessonType.Tutorial
				},
				new Lesson()
				{
					Subject = prg101,
					Teacher = lecturer,
					StartTime = "THU 10:00",
					EndTime = "THU 11:00",
					RoomNumber = "L35",
					LessonType = LessonType.Lecture
				},
				new Lesson()
				{
					Subject = prg101,
					Teacher = lecturer,
					StartTime = "THU 12:00",
					EndTime = "THU 13:00",
					RoomNumber = "L35",
					LessonType = LessonType.Lecture
				},
				new Lesson()
				{
					Subject = prg101,
					Teacher = lecturer,
					StartTime = "FRI 11:00",
					EndTime = "FRI 12:00",
					RoomNumber = "L18",
					LessonType = LessonType.Lecture
				},
				new Lesson()
				{
					Subject = prg102,
					Teacher = lecturer,
					StartTime = "MON 10:00",
					EndTime = "MON 12:00",
					RoomNumber = "L33",
					LessonType = LessonType.Lecture
				},
				new Lesson()
				{
					Subject = prg102,
					Teacher = lecturer,
					StartTime = "WED 15:00",
					EndTime = "WED 16:00",
					RoomNumber = "L35",
					LessonType = LessonType.Lecture
				},
				new Lesson()
				{
					Subject = prg102,
					Teacher = tutor,
					StartTime = "THU 12:00",
					EndTime = "THU 13:00",
					RoomNumber = "M20",
					LessonType = LessonType.Tutorial
				},

				new Lesson()
				{
					Subject = prg201,
					Teacher = lecturer,
					StartTime = "TUE 11:00",
					EndTime = "TUE 12:00",
					RoomNumber = "L33",
					LessonType = LessonType.Lecture
				},
				new Lesson()
				{
					Subject = prg201,
					Teacher = lecturer,
					StartTime = "WED 15:00",
					EndTime = "WED 16:00",
					RoomNumber = "L33",
					LessonType = LessonType.Lecture
				},
				new Lesson()
				{
					Subject = prg201,
					Teacher = tutor,
					StartTime = "THU 12:00",
					EndTime = "THU 13:00",
					RoomNumber = "M22",
					LessonType = LessonType.Tutorial
				},
				new Lesson()
				{
					Subject = prg201,
					Teacher = tutor,
					StartTime = "FRI 14:00",
					EndTime = "FRI 17:00",
					RoomNumber = "M16",
					LessonType = LessonType.Lab
				},
			});
		}

		static void CreateSubjects(SchoolContext context)
		{
			context.AddRange(new List<Subject>()
			{
				new Subject()
				{
					Code = "PRG101",
					Name = "Introduction to Programming",
					Description = "A first look at basic programming techniques.",
				},
				new Subject()
				{
					Code = "PRG102",
					Name = "Data Structures and Algorithms",
					Description = "Covers core concepts related to algorithmic problem solving and common data structures.",
				},
				new Subject()
				{
					Code = "PRG201",
					Name = ".NET Application Development",
					Description = "Desktop application development using Microsoft's .NET platform.",
				},
				new Subject()
				{
					Code = "PRG301",
					Name = "Advanced Algorithms",
					Description = "A more in-dept focus on complex algoriths and related techniques.",
				},
			});
		}

		static void CreateStaff(SchoolContext context)
		{
			context.AddRange(new List<Staff>()
			{
				new Staff()
				{
					Name = "Nicola",
					Email = "nicola@uts.com",
					JobTitle = "Lecturer"
				},
				new Staff()
				{
					Name = "Saurabh",
					Email = "saurabh@uts.com",
					JobTitle = "Associate Professor"
				},
				new Staff()
				{
					Name = "Lindsay",
					Email = "lindsay@uts.com",
					JobTitle = "Tutor"
				},
				new Staff()
				{
					Name = "Michael",
					Email = "michael@uts.com",
					JobTitle = "Tutor"
				}
			});
		}

		static void CreateStudents(SchoolContext context)
		{
			context.AddRange(new List<Student>()
			{
				new Student()
				{
					Name = "Liam",
					Email = "liam@wisetech.com",
					Degree = "Bachelor of Science"
				},
				new Student()
				{
					Name = "Sam",
					Email = "sam@uts.com",
					Degree = "Bachelor of Arts"
				},
				new Student()
				{
					Name = "Jess",
					Email = "jess@uts.com",
					Degree = "Bachelor of Computer Science"
				},
				new Student()
				{
					Name = "Sylvia",
					Email = "sylvia@uts.com",
					Degree = "Bachelor of Engineering"
				},
				new Student()
				{
					Name = "Darell",
					Email = "darell@uts.com",
					Degree = "Bachelor of Computer Science"
				},
				new Student()
				{
					Name = "Lucia",
					Email = "lucia@uts.com",
					Degree = "Bachelor of IT"
				},
				new Student()
				{
					Name = "Han",
					Email = "han@uts.com",
					Degree = "Bachelor of Computer Science"
				},
				new Student()
				{
					Name = "Alex",
					Email = "alex@uts.com",
					Degree = "Bachelor of Science"
				},
			});
		}
	}
}
