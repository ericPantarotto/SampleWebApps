using System;
using System.Collections.Generic;
using DataLibraryRepo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCDemo.Models
{

    public class OrderDisplayModel : IOrderDisplayModel
    {
        public OrderModel Order { get; set; }
        public string ItemPurchased { get; set; }
    }
}