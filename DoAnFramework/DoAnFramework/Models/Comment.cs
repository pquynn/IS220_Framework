using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnFramework.Models
{
    public partial class Comment
    {
        public int CmtId { get; set; }
        public int? BookId { get; set; }
        public string? UserId { get; set; }
        public string? BookName { get; set; }
        public string? UserName { get; set; }
        public string? Content { get; set; }
        public DateTime? CmtDay { get; set; }
        public int? Score { get; set; }

        public virtual Book? Book { get; set; }
        public virtual User? User { get; set; }
    }
}
