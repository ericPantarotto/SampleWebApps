using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibraryRepo.Data;
using DataLibraryRepo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace APIDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IFoodData _foodData;
        private readonly IOrderData _orderData;

        public OrderController(IFoodData foodData, IOrderData orderData)
        {
            _foodData = foodData;
            _orderData = orderData;
        }

        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(OrderModel order)
        {
              order.Total = order.Quantity * _foodData.GetFoodById(order.FoodId).Price;

            int id = _orderData.CreateOrder(order);

            return Ok(new {Id = id});
        }
    }
}
