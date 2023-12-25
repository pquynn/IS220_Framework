using System;
using System.Collections.Generic;

namespace DoAnFramework.Models
{
    public partial class Book
    {
        public Book()
        {
            Comments = new HashSet<Comment>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int BookId { get; set; }
        public string? Name { get; set; }
        public int? BookCover { get; set; }
        public int? Pages { get; set; }
        public string? Author { get; set; }
        public DateTime? YearPublish { get; set; }
        public string? Publisher { get; set; }
        public int? Price { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual BookImage? BookImage { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
