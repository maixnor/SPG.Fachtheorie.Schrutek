using System;
using System.Collections.Generic;

namespace Spg.ProbeFachtheorie.Aufgabe1.Domain.Model
{
    public class Category : EntityBase
    {
        public Guid CategoryId { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;

        public Library Library { get; set; } = default!;
        public List<Book> Books = new ();
    }
}