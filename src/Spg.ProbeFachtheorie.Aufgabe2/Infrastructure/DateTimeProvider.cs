using System;
using Spg.ProbeFachtheorie.Aufgabe2.Domain.Interfaces;

namespace Spg.ProbeFachtheorie.Aufgabe2.Infrastructure;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now()
    {
        return DateTime.Now;
    }
}