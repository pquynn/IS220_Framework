using System;
using System.Collections.Generic;

namespace DoAnFramework.Models
{
    public partial class Login
    {
        public Login()
        {
            Users = new HashSet<User>();
        }

        public string UserLogin { get; set; } = null!;
        public string? UserPassword { get; set; }
        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
