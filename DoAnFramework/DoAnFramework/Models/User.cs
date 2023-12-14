using System;
using System.Collections.Generic;

namespace DoAnFramework.Models
{
    public partial class User
    {
        public User()
        {
            Blogs = new HashSet<Blog>();
            Comments = new HashSet<Comment>();
            Orders = new HashSet<Order>();
        }

        public string UserId { get; set; } = null!;
        public string? UserLogin { get; set; }
        public string? UserName { get; set; }
        public string? UserTelephone { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public DateTime? DayAdd { get; set; }

        public virtual Login? UserLoginNavigation { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
