using System;
using System.Collections.Generic;

namespace Spg.ProbeFachtheorie.Aufgabe1.Domain.Model
{
    public class BorrowCharge : EntityBase
    {
        public Guid BorrowChargeId { get; set; } = Guid.NewGuid();
        public decimal Amount { get; set; } = 0m;
        public string Currency { get; set; } = string.Empty;

        public Book Book { get; set; } = default!;
        public List<ChargeType> ChargeType = new ();
    }
}