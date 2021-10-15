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
        private readonly IOrderCreateModel _orderCreateModel;
        private readonly IOrderDisplayModel _orderDisplayModel;

        public OrdersController(IFoodData foodData,
                                IOrderData orderData,
                                IOrderCreateModel orderCreateModel,
                                IOrderDisplayModel orderDisplayModel)
        {
            _foodData = foodData;
            _orderData = orderData;
            _orderCreateModel = orderCreateModel;
            _orderDisplayModel = orderDisplayModel;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var food = _foodData.GetFood();

            food.ForEach(x =>
            {
                _orderCreateModel.FoodItems.Add(new SelectListItem { Value = x.Id.ToString(), Text = x.Title });
            });

            return View(_orderCreateModel);
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

            return RedirectToAction("Display", new {id});
        }

        public IActionResult Display(int id)
        {
            _orderDisplayModel.Order = _orderData.GetOrderById(id);

            if (_orderDisplayModel.Order is not null)
            {
                _orderDisplayModel.ItemPurchased = _foodData.GetFoodById(_orderDisplayModel.Order.FoodId)?.Title;
            }

            return View(_orderDisplayModel);
        }

        [HttpPost]
        public IActionResult Update(int id, string orderName )
        {
            var modelForUpdate = _orderData.GetOrderById(id);
            modelForUpdate.OrderName = orderName;

            _orderData.UpdateOrderName(modelForUpdate);

            return RedirectToAction("Display", new {id});
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var order = _orderData.GetOrderById(id);
            return View(order);
        }

        [HttpPost]
        public IActionResult Delete(OrderModel order)
        {
            _orderData.DeleteOrder(order.Id);
            return RedirectToAction("Create");
        }
    }
}
