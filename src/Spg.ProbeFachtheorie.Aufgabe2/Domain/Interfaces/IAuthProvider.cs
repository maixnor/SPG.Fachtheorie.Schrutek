using Spg.ProbeFachtheorie.Aufgabe2.Domain.Model.Custom;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.ProbeFachtheorie.Aufgabe2.Domain.Interfaces
{
    public interface IAuthProvider
    {
        (UserInfo userInfo, string errorMessage) CheckUser(string eMail, string password);
    }
}
