using System.Collections.Generic;
using DataLibraryRepo.Data;
using DataLibraryRepo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class ListModel : PageModel
    {
        private readonly IFoodData _foodData;
        public List<FoodModel> Food { get; set; }

        public ListModel(IFoodData foodData)
        {
            _foodData = foodData;
        }
        public void OnGet()
        {
            Food = _foodData.GetFood();
        }
    }
}
