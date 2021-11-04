using System;

namespace APIDemo.Models
{
    public class OrderListModel
    {
        public int Id { get; set; }
        public string OrderName { get; set; }
        public DateTime OrderDate { get; set; } 
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public string FoodTitle { get; set; }
    }
}
