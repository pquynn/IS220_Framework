namespace DoAnFramework.Models.ViewModel
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public IEnumerable<OrderDetailWithImage> OrderDetails { get; set; }
    }
}
