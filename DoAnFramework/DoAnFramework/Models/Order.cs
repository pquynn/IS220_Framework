using System;
using System.Collections.Generic;

namespace DoAnFramework.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserTelephone { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? Address { get; set; }
        public string? Status { get; set; }
        public int? TotalBooks { get; set; }
        public int? TotalPrice { get; set; }
        public string? Pay { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
