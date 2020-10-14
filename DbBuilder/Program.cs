using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model.Data;

namespace DbBuilder
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var schoolContext = new SchoolContext())
			{
				PromptToBuildDb(schoolContext);
				Console.WriteLine("Completed");
			}
		}

		static void PromptToBuildDb(SchoolContext schoolContext)
		{
			var anySubjects = false;
			try
			{
				anySubjects = schoolContext.Subjects.Any();
			}
			catch
			{
				Console.WriteLine("Something went wrong when connecting to db. Exiting...");
				return;
			}

			if (anySubjects)
			{
				Console.WriteLine("Database is not empty, do you want to DROP existing and RECREATE an empty db? (y/n)");
				Console.Write("> ");

				if (Console.ReadLine().ToUpperInvariant()[0] == 'Y')
				{
					Console.WriteLine("Deleting existing data...");
					schoolContext.Database.EnsureDeleted();
					Console.WriteLine("Creating db...");
					schoolContext.Database.Migrate();
				}
				else
				{
					return;
				}
			}

			Console.WriteLine("Add sample data? (y/n)");
			Console.Write("> ");
			if (Console.ReadLine().ToUpperInvariant()[0] == 'Y')
			{
				Console.WriteLine("Adding sample data...");
				DataGenerator.CreateSampleSchoolData(schoolContext);
			}
		}
	}
}
