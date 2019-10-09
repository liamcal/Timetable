using System;
using Microsoft.EntityFrameworkCore;
using Model.Data;

namespace Tests.Data
{
	public static class InMemoryContextFactory
	{
		public static SchoolContext CreateSchoolContext() => new SchoolContext(InMemoryOptions);

		static DbContextOptions<SchoolContext> InMemoryOptions => new DbContextOptionsBuilder<SchoolContext>()
			.UseInMemoryDatabase(Guid.NewGuid().ToString())
			.Options;

	}
}
