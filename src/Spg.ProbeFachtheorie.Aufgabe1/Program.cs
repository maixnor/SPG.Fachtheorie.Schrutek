using Microsoft.EntityFrameworkCore;
using Spg.ProbeFachtheorie.Aufgabe1.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.ProbeFachtheorie.Aufgabe1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=Store.db")
                .Options;

            using (var db = new LibraryContext(options))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }
    }
}
