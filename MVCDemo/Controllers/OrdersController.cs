using System.Diagnostics;
using DataLibraryRepo.Data;
using DataLibraryRepo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCDemo.Models;


namespace MVCDemo.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IFoodData _foodData;
        private readonly IOrderData _orderData;

        public OrdersController(IFoodData foodData, IOrderData orderData)
        {
            _foodData = foodData;
            _orderData = orderData;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var food = _foodData.GetFood();

            OrderCreateModel model = new OrderCreateModel();

            food.ForEach(x =>
            {
                model.FoodItems.Add(new SelectListItem { Value = x.Id.ToString(), Text = x.Title });
            });

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(OrderModel order)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            order.Total = order.Quantity * _foodData.GetFoodById(order.FoodId).Price;

            int id = _orderData.CreateOrder(order);

            return RedirectToAction("Create");
            // return RedirectToPage("./Display", new { Id = id});
        }
      
    }
}
