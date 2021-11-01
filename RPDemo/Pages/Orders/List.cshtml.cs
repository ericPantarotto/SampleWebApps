using DataLibraryRepo.Data;
using DataLibraryRepo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RPDemo.Models;
using System.Collections.Generic;
using System.Linq;

namespace RPDemo.Pages.Orders
{
    public class ListModel : PageModel
    {
        private readonly IFoodData _foodData;
        private readonly IOrderData _orderData;
        public List<OrderListModel> Order { get; set; }= new List<OrderListModel>();

        public ListModel(IFoodData foodData, IOrderData orderData)
        {
            _foodData = foodData;
            _orderData = orderData;
        }
        public void OnGet()
        {
            var food = _foodData.GetFood();
            
            var orders = _orderData.GetOrder();

            orders
            .Where(o => o.Total != 0).ToList()
            .ForEach(item =>
            {
                Order.Add(new OrderListModel
                {
                    Id = item.Id,
                    OrderDate = item.OrderDate,
                    OrderName = item.OrderName,
                    Quantity = item.Quantity,
                    Total = item.Total,
                    FoodTitle = food.Where(f => f.Id == item.FoodId).First().Title
                });
            });

        }
    }
}
