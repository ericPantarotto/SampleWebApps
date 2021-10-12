using System.Threading.Tasks;
using DataLibraryRepo.Data;
using DataLibraryRepo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RPDemo.Models;

namespace MyApp.Namespace
{
    public class DisplayModel : PageModel
    {
        private readonly IOrderData _orderData;
        private readonly IFoodData _foodData;

        [BindProperty(SupportsGet =true)]
        public int Id { get; set; }
        
        [BindProperty]
        public OrderUpdateModel UpdateModel { get; set; }

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

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var modelForUpdate = _orderData.GetOrderById(UpdateModel.Id);
            modelForUpdate.OrderName = UpdateModel.OrderName;

            _orderData.UpdateOrderName(modelForUpdate);

            return RedirectToPage("./Display", new { UpdateModel.Id});
        }
    }
}
