using DataLibraryRepo.Data;
using DataLibraryRepo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPDemo.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly IFoodData _foodData;
        private readonly IOrderData _orderData;

        public List<SelectListItem> FoodItems { get; set; }

        [BindProperty]
        public OrderModel Order { get; set; }
        public CreateModel(IFoodData foodData,IOrderData orderData)
        {
            _foodData = foodData;
            _orderData = orderData;
        }
        public void OnGet()
        {
            var food =  _foodData.GetFood();

            FoodItems  = new List<SelectListItem>();

            food.ForEach(x =>
            {
                FoodItems.Add(new SelectListItem { Value = x.Id.ToString(), Text = x.Title });
            });
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Order.Total = Order.Quantity * _foodData.GetFoodById(Order.FoodId).Price;

            int id = _orderData.CreateOrder(Order);

            return RedirectToPage("./Display", new { Id = id});
        }
    }
}
