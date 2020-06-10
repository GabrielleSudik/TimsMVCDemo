using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.Data;
using Microsoft.AspNetCore.Mvc;

namespace MVCDemoApp.Controllers
{
    public class FoodController : Controller
    {
        private readonly IFoodData _foodData;

        //create the construtor, injecting IFoodData
        public FoodController(IFoodData foodData)
        {
            _foodData = foodData;
        }

        //revise the boilerplate Index method
        public async Task<IActionResult> Index()
        {
            var food = await _foodData.GetFood();
            return View(food);
        }
    }
}