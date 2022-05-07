using System;
using System.Collections.Generic;

namespace Spg.ProbeFachtheorie.Aufgabe1.Domain.Model
{
    public class ChargeType : EntityBase
    {
        public Guid ChargeTypeId { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;

        public List<BorrowCharge> BorrowCharges = new ();
    }
}