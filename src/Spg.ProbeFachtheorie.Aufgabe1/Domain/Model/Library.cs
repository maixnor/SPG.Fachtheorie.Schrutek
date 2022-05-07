using System.Collections.Generic;

namespace Spg.ProbeFachtheorie.Aufgabe1.Domain.Model
{
    public class Library : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string Suffix { get; set; } = string.Empty;
        public string Phrase { get; set; } = string.Empty;
        public string Bs { get; set; } = string.Empty;
        public int Rating { get; set; } = 0;
        public List<Category> Categories = new ();
    }
}