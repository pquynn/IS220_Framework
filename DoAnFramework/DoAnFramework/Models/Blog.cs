using System;
using System.Collections.Generic;

namespace DoAnFramework.Models
{
    public partial class Blog
    {
        public int BlogId { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public DateTime? BlogDay { get; set; }
        public string? Content { get; set; }
        public byte[]? BlogImage { get; set; }
        public string? BlogTitle { get; set; }

        public virtual User? User { get; set; }
    }
}
