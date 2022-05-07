using System;

namespace Spg.ProbeFachtheorie.Aufgabe2.Domain.Interfaces;

public interface IDateTimeProvider
{
    public DateTime Now();
}