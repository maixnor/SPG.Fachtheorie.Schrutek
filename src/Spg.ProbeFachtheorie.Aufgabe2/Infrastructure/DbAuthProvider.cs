using Spg.ProbeFachtheorie.Aufgabe2.Domain.Interfaces;
using Spg.ProbeFachtheorie.Aufgabe2.Domain.Model;
using Spg.ProbeFachtheorie.Aufgabe2.Domain.Model.Custom;
using Spg.ProbeFachtheorie.Aufgabe2.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spg.ProbeFachtheorie.Aufgabe2.Infrastructure
{
    public class DbAuthProvider : IAuthProvider
    {
        private readonly LibraryContext _dbContext;

        public DbAuthProvider(LibraryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public (UserInfo userInfo, string errorMessage) CheckUser(string username, string password)
        {
            string message = string.Empty;

            User existingUser = _dbContext.Users.SingleOrDefault(u => u.UserName == username) ?? new User();
            if (existingUser.Guid != new Guid())
            {
                if (HashExtensions.GenerateHash(password, existingUser.Salt) != existingUser.PasswordHash)
                {
                    message = "Die Anmeldung ist fehlgeschlagen!";
                }
                else
                {
                    UserRoles userRole = UserRoles.Guest;
                    switch (existingUser.Role.ToString().ToUpper())
                    {
                        case "BACKOFFICEEMPLOYEE":
                            userRole = UserRoles.BackOfficeEmployee;
                            break;
                    }
                    return (new UserInfo(existingUser.UserName, existingUser.EMail, existingUser.Id, userRole), message);
                }
            }
            else
            {
                message = "Die Anmeldung ist fehlgeschlagen!";
            }
            return (null!, message);
        }
    }
}
