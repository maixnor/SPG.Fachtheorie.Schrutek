using Spg.ProbeFachtheorie.Aufgabe2.Domain.Model;
using Spg.ProbeFachtheorie.Aufgabe2.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Spg.ProbeFachtheorie.Aufgabe2.Domain.Exceptions;
using Spg.ProbeFachtheorie.Aufgabe2.Domain.Interfaces;
using Spg.ProbeFachtheorie.Aufgabe2.Services.Helpers;

namespace Spg.ProbeFachtheorie.Aufgabe2.Services
{
    public class PickupTimeService
    {
        private readonly LibraryContext _db;
        private readonly IAuthService _auth;
        private readonly IDateTimeProvider _date;

        public PickupTimeService(LibraryContext db, IAuthService auth, IDateTimeProvider date)
        {
            _db = db;
            _auth = auth;
            _date = date;
        }

        /// <summary>
        /// Mustermethode für einen Unittest.
        /// </summary>
        public IQueryable<Book> GetAllBooks()
        {
            return _db.Books;
        }

        public Guid AddAppointment(AppointmentDto dto)
        {
            if (!_auth.IsAuthenticated)
            {
                throw new AuthServiceException("Not Authenticated");
            }
            if (dto.Date < _date.Now().AddDays(1))
            {
                throw new ServiceException("Appointment Date must be at least 1 day in the Future to be added");
            }

            var singleOrDefault = _db.PickupTimes.SingleOrDefault(t => t.Date == dto.Date);
            var colliding = singleOrDefault is not null;
            if (colliding)
            {
                throw new ServiceException("Appointment Date is already taken");
            }

            var entity = new PickupTime()
            {
                Date = dto.Date,
                Guid = new Guid(),
                PicupUser = _db.Users.Find(dto.PicupUserId),
                ShoppingCart = _db.ShoppingCarts.Find(dto.ShoppingCartId)
            };
            
            _db.PickupTimes.Add(entity);
            _db.SaveChanges();
            return entity.Guid;
        }

        public void DeleteAppointment(AppointmentDto dto)
        {
            var entity = _db.PickupTimes.Single(t => t.Guid == dto.Id);
            if (entity.Date < _date.Now().AddDays(1))
            {
                throw new ServiceException("Appointment Date must be at least 1 day in the Future to be deleted");
            }
            _db.PickupTimes.Remove(entity);
            _db.SaveChanges();
        }
    }

    public record AppointmentDto()
    {
        public int PicupUserId { get; set; }
        public int ShoppingCartId { get; set; }
        public DateTime Date { get; set; }
        public Guid Id { get; set; }
    }
}
