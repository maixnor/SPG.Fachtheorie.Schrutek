using Microsoft.EntityFrameworkCore;
using Spg.ProbeFachtheorie.Aufgabe1.Domain;
using Spg.ProbeFachtheorie.Aufgabe1.Domain.Model;

namespace Spg.ProbeFachtheorie.Aufgabe1.Infrastructure
{
    public class LibraryContext : DbContext
    {
        public DbSet<Library> Libraries => Set<Library>();
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<ChargeType> ChargeTypes => Set<ChargeType>();
        public DbSet<BorrowCharge> BorrowCharges => Set<BorrowCharge>();

        public LibraryContext(DbContextOptions options) 
            : base(options) 
        { }
    }
}
