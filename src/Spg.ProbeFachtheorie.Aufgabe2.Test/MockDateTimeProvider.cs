using System;
using Spg.ProbeFachtheorie.Aufgabe2.Domain.Interfaces;

namespace Spg.ProbeFachtheorie.Aufgabe2.Test;

public class MockDateTimeProvider : IDateTimeProvider
{
    public DateTime ToReturn { get; set; }

    public DateTime Now()
    {
        return ToReturn;
    }
}