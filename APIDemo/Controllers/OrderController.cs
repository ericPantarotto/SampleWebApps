using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibraryRepo.Data;
using DataLibraryRepo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using APIDemo.Models;

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

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var order = _orderData.GetOrderById(id);
            if (order is not null)
            {
                var food = _foodData.GetFood();
                var output = new
                {
                    Order = order,
                    ItemPurchased = _foodData.GetFoodById(order.FoodId)?.Title
                };
                return Ok(output);
            }else
            {
                return NotFound(new {Id = id});
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            List<OrderListModel> orderList = new List<OrderListModel>();
            var food = _foodData.GetFood();
            
            var orders = _orderData.GetOrder();

            orders
            .Where(o => o.Total != 0).ToList()
            .ForEach(item =>
            {
                orderList.Add(new OrderListModel
                {
                    Id = item.Id,
                    OrderDate = item.OrderDate,
                    OrderName = item.OrderName,
                    Quantity = item.Quantity,
                    Total = item.Total,
                    FoodTitle = food.Where(f => f.Id == item.FoodId).First().Title
                });
            });

            if (orderList.Count != 0)
            {
                return Ok(orderList);
            }else
            {
                return NotFound();
            }
        }


        [HttpPut]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Put([FromBody] OrderUpdateModel data)
        {
            
            var modelForUpdate = _orderData.GetOrderById(data.Id);
            modelForUpdate.OrderName = data.OrderName;
            _orderData.UpdateOrderName(modelForUpdate);

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
         public IActionResult Delete(int id )
         {
             _orderData.DeleteOrder(id);
             return Ok();
         }
    }
}
