using System.Collections.Generic;
using DataLibraryRepo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCDemo.Models
{
    public interface IOrderCreateModel
    {
        OrderModel Order { get; set; }
        List<SelectListItem> FoodItems { get; set; }
    }
}