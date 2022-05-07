using Bogus;
using Spg.ProbeFachtheorie.Aufgabe2.Domain.Model;
using Spg.ProbeFachtheorie.Aufgabe2.Domain.Model.Custom;
using Spg.ProbeFachtheorie.Aufgabe2.Infrastructure;
using Spg.ProbeFachtheorie.Aufgabe2.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.ProbeFachtheorie.Aufgabe2.Services
{
    public class SeedService
    {
        private readonly LibraryContext _dbContext;

        public SeedService(LibraryContext dbContext)
            => _dbContext = dbContext;

        public void DeleteDatabase()
            => _dbContext.Database.EnsureDeleted();
        public void CreateDatabase()
            => _dbContext.Database.EnsureCreated();

        public void Seed()
        {
            Randomizer.Seed = new Random(091007);

            var borrowChargeTypes = new List<BorrowChargeType>()
            {
                new BorrowChargeType(){ Name="Normalpreis"},
                new BorrowChargeType(){ Name="Ermäßigt"},
            };
            _dbContext.BorrowChargeTypes.AddRange(borrowChargeTypes);
            _dbContext.SaveChanges();



            var books = new Faker<Book>("de").Rules((f, e) =>
            {
                e.Ean13 = f.Commerce.Ean13();
                e.Name = f.Commerce.ProductName();
                e.Abstract = f.Lorem.Paragraph();
            })
            .Generate(200)
            .ToList();

            _dbContext.Books.AddRange(books);
            _dbContext.SaveChanges();



            var borrowCharge = new Faker<BorrowCharge>("de").Rules((f, e) =>
            {
                e.Amount = f.Finance.Amount();
                e.Currency = "EUR";

                e.Book = f.Random.ListItem(books);
                e.BorrowChargeType = f.Random.ListItem(borrowChargeTypes);
            })
            .Generate(400)
            .ToList();

            _dbContext.BorrowCharges.AddRange(borrowCharge);
            _dbContext.SaveChanges();



            var users = new Faker<User>("de").Rules((f, e) =>
            {
                e.UserName = "";
                e.EMail = f.Internet.Email();
                e.FirstName = f.Name.FirstName();
                e.LastName = f.Name.LastName();
                e.Salt = HashExtensions.GenerateSecret(128);
                e.PasswordHash = HashExtensions.GenerateHash("geheim", e.Salt);
                e.Role = f.Random.Enum<UserRoles>();
                e.Guid = Guid.NewGuid();
            })
            .Generate(500)
            .ToList();

            _dbContext.Users.AddRange(users);
            _dbContext.SaveChanges();

            int userindex = 0;
            foreach (User user in _dbContext.Users)
            {
                user.UserName = $"user{userindex++}";
                _dbContext.Update(user);
            }
            _dbContext.SaveChanges();



            string[] cartNames = new string[] { "Vorlesebücher", "Nachlesebücher", "Andere Bücher", "Gute Nachtgeschichten", "Peppa Pig", "Märchenbücher", "Liederbücher", "Puzzlebücher", "Kinderreime", "Bilderbücher", "Kriminalromane", "Abenteuergeschichten", "Science Fiction Romane", "Gruselgeschichten", "Kochbücher", "Grillrezepte" };
            var shoppingCarts = new Faker<ShoppingCart>("de").Rules((f, e) =>
            {
                e.Name = f.Random.ArrayElement(cartNames);
                e.User = f.Random.ListItem(users);
                e.Guid = Guid.NewGuid();
            })
            .Generate(25)
            .ToList();

            _dbContext.ShoppingCarts.AddRange(shoppingCarts);
            _dbContext.SaveChanges();



            var shoppingCartitems = new Faker<ShoppingCartItem>("de").Rules((f, e) =>
            {
                e.Book = f.Random.ListItem(books);
                e.ShoppingCart = f.Random.ListItem(shoppingCarts);
                e.ShoppingCartItemState = ShoppingCartItemStates.Reserved;
            })
            .Generate(100)
            .ToList();

            _dbContext.ShoppingCartItems.AddRange(shoppingCartitems);
            _dbContext.SaveChanges();
        }
    }
}
