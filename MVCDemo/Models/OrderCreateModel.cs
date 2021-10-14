using System;
using System.Collections.Generic;
using DataLibraryRepo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCDemo.Models
{
    public class OrderCreateModel : IOrderCreateModel
    {
        public OrderModel Order { get; set; } = new OrderModel();
        public List<SelectListItem> FoodItems { get; set; } = new List<SelectListItem>();
    }
}