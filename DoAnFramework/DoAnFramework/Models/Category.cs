using System;
using System.Collections.Generic;

namespace DoAnFramework.Models
{
    public partial class Category
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }

        public int CategoryId { get; set; }
        public string? NameCategory { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
