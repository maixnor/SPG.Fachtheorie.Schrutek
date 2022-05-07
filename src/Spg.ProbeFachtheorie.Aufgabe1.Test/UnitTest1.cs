using Microsoft.EntityFrameworkCore;
using Spg.ProbeFachtheorie.Aufgabe1.Infrastructure;
using System;
using FluentAssertions;
using Xunit;

namespace Spg.ProbeFachtheorie.Aufgabe1.Test
{
    public class UnitTest1
    {
        [Fact]
        public void GenerateDbFromContextTest()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=Store.db")
                .Options;

            using (var db = new LibraryContext(options))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                Assert.True(true);
            }
        }

        [Fact]
        public void TrueIsTrue()
        {
            true.Should().BeTrue();
        }
    }
}
