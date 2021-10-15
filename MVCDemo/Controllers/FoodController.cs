using System.Collections.Generic;
using DataLibraryRepo.Data;
using DataLibraryRepo.Models;
using Microsoft.AspNetCore.Mvc;


namespace MVCDemo.Controllers
{
    public class FoodController : Controller
    {
        private readonly IFoodData _foodData;

        public FoodController(IFoodData foodData)
        {
            _foodData = foodData;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var food = _foodData.GetFood();
            return View(food);
        }
    }
}
