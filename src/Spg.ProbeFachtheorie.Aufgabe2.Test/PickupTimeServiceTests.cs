using System;
using Microsoft.EntityFrameworkCore;
using Spg.ProbeFachtheorie.Aufgabe2.Infrastructure;
using Spg.ProbeFachtheorie.Aufgabe2.Services;
using System.Linq;
using Spg.ProbeFachtheorie.Aufgabe2.Domain.Exceptions;
using Spg.ProbeFachtheorie.Aufgabe2.Domain.Model;
using Xunit;

namespace Spg.ProbeFachtheorie.Aufgabe2.Test
{
    public class PickupTimeServiceTests
    {
        private LibraryContext GetContext()
        {
            var opt = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=Library.db")
                .Options;
            var db = new LibraryContext(opt);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.Seed();
            return db;
        }

        private readonly PickupTimeService _sut;
        private readonly LibraryContext _context;
        private readonly MockDbAuthProvider _mockDbAuthProvider;

        private readonly AppointmentDto dto = new ()
        {
            Date = new DateTime(2022, 2, 2),
            Id = Guid.Empty,
            PicupUserId = 4,
            ShoppingCartId = 3
        };
        
        public PickupTimeServiceTests()
        {
            _context = GetContext();
            _mockDbAuthProvider = new MockDbAuthProvider
            {
                IsAuthenticated = true
            };
            _sut = new PickupTimeService(
                _context, 
                _mockDbAuthProvider, 
                new MockDateTimeProvider
                {
                    ToReturn = new DateTime(2022, 1, 1)
                }
            );
        }

        /// <summary>
        /// Demotest zum Testen einer Servicemethode.
        /// </summary>
        [Fact]
        public void GetBooksSuccessTest()
        {
            var count = _sut.GetAllBooks().Count();
            Assert.True(count == 200);
        }

        [Fact]
        public void AddAppointmentSuccessTest()
        {
            _sut.AddAppointment(dto);
            Assert.True(true);
        }

        [Fact]
        public void AddAppointmentNotInFutureValidationErrorTest()
        {
            dto.Date = new DateTime(2022, 1, 1);
            Assert.Throws<ServiceException>(() => _sut.AddAppointment(dto));
        }

        [Fact]
        public void AddAppointmentNotUniqueValidationErrorTest()
        {
            _sut.AddAppointment(dto);
            Assert.Throws<ServiceException>(() => _sut.AddAppointment(dto));
        }

        [Fact]
        public void AddAppointmentNoUserValidationErrorTest()
        {
            _mockDbAuthProvider.IsAuthenticated = false;
            Assert.Throws<AuthServiceException>(() => _sut.AddAppointment(dto));
            _mockDbAuthProvider.IsAuthenticated = true;
        }

        [Fact]
        public void DeleteAppointmentSuccessTest()
        {
            var guid = _sut.AddAppointment(dto);
            var deleteDto = new AppointmentDto
            {
                Id = guid,
            };
            _sut.DeleteAppointment(deleteDto);
            Assert.True(true);
        }

        [Fact]
        public void DeleteAppointmentNotInFutureValidationErrorTest()
        {
            var guid = Guid.NewGuid();
            _context.PickupTimes.Add(new PickupTime()
            {
                Date = new DateTime(2022, 1, 1),
                Guid = guid,
                ShoppingCart = _context.ShoppingCarts.Find(3),
                PicupUser = _context.Users.Find(3)
            });
            _context.SaveChanges();
            var deleteDto = new AppointmentDto
            {
                Id = guid,
            };
            Assert.Throws<ServiceException>(() => _sut.DeleteAppointment(deleteDto));
        }
        
    }
}
