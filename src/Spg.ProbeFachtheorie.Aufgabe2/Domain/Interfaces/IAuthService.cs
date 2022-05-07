using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.ProbeFachtheorie.Aufgabe2.Domain.Interfaces
{
    public interface IAuthService
    {
        string Username();

        int UserId();

        bool IsAuthenticated { get; }

        Task Login(string username, string password);

        Task Logout();

        bool HasRole(string role);
    }
}
