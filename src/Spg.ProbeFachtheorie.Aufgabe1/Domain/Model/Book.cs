using System;
using System.Collections.Generic;

namespace Spg.ProbeFachtheorie.Aufgabe1.Domain.Model
{
    public class Book : EntityBase
    {
        public Guid BookId { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty; 
        public string Abstract { get; set; } = string.Empty;
        public string Ean13 { get; set; } = string.Empty;

        public Category Category { get; set; } = default!;
        public List<BorrowCharge> BorrowCharges { get; set; } = new ();
    }
}