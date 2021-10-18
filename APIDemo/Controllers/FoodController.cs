using System.Collections.Generic;
using DataLibraryRepo.Data;
using DataLibraryRepo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace APIDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodController : ControllerBase
    {
        private readonly IFoodData _foodData;

        public FoodController(IFoodData foodData)
        {
            _foodData = foodData;
        }

        // without decoration
        // [HttpGet]
        // public List<FoodModel> Get()
        // {
        //     return _foodData.GetFood();
        // }

        //with decoration : better for swagger!
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult Get()
        {
            var output =  _foodData.GetFood();
            
            if (output is not null)
            {
               return Ok(output);
            }
            else
            {
                return NotFound() ;
            }
        }
    }
}