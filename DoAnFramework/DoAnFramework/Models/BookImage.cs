using System;
using System.Collections.Generic;

namespace DoAnFramework.Models
{
    public partial class BookImage
    {
        public int BookId { get; set; }
        public byte[]? FrontCover { get; set; }
        public byte[]? BackCover { get; set; }
        public byte[]? FisrtImage { get; set; }
        public byte[]? SecondImage { get; set; }
        public byte[]? ThirdImage { get; set; }

        public virtual Book Book { get; set; } = null!;
    }
}
