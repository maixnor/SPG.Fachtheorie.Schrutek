using System.Threading.Tasks;
using Spg.ProbeFachtheorie.Aufgabe2.Domain.Interfaces;

namespace Spg.ProbeFachtheorie.Aufgabe2.Test;

public class MockDbAuthProvider : IAuthService
{

    public bool IsAuthenticated { get; set; }

    public string Username()
    {
        throw new System.NotImplementedException();
    }

    public int UserId()
    {
        throw new System.NotImplementedException();
    }

    public Task Login(string username, string password)
    {
        throw new System.NotImplementedException();
    }

    public Task Logout()
    {
        throw new System.NotImplementedException();
    }

    public bool HasRole(string role)
    {
        throw new System.NotImplementedException();
    }
}