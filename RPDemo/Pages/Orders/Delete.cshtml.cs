using DataLibraryRepo.Data;
using DataLibraryRepo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class DeleteModel : PageModel
    {
        private readonly IOrderData _orderData;

        [BindProperty(SupportsGet =true)]
        public int Id { get; set; }
        public OrderModel Order { get; set; }

        public DeleteModel(IOrderData orderData)
        {
            _orderData = orderData;
        }

        public void OnGet()
        {
            Order = _orderData.GetOrderById(Id);
        }

        public IActionResult OnPost()
        {
            _orderData.DeleteOrder(Id);
            return RedirectToPage("./Create");
        }
    }
}
