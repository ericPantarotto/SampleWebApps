using System.Threading.Tasks;
using DataLibraryRepo.Data;
using DataLibraryRepo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class DisplayModel : PageModel
    {
        private readonly IOrderData _orderData;
        private readonly IFoodData _foodData;

        [BindProperty(SupportsGet =true)]
        public int Id { get; set; }
        public OrderModel Order { get; set; }
        public string ItemPurchased { get; set; }

        public DisplayModel(IOrderData orderData, IFoodData foodData)
        {
            _orderData = orderData;
            _foodData = foodData;
        }
        public IActionResult OnGet()
        {
            Order = _orderData.GetOrderById(Id);

            if (Order is not null)
            {
                ItemPurchased = _foodData.GetFoodById(Order.FoodId)?.Title;
            }

            return Page();
        }
    }
}
