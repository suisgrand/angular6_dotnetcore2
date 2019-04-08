using System;
using proj.Persistence;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace test
{
    public class UnitTest1
    {

        public UnitTest1()
        {
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .EnableSensitiveDataLogging()
                // .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;
            var context = new TaskDbContext(options);
        }

        private void populateDatabase(TaskDbContext context)
        {

        }

        [Fact]
        public void Test1()
        {

        }
    }
}
