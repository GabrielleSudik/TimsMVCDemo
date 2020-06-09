using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCDemoApp.Models;

namespace MVCDemoApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IFoodData _foodData;
        private readonly IOrderData _orderData;

        public OrdersController(IFoodData foodData, IOrderData orderData)
        {
            _foodData = foodData;
            _orderData = orderData;
        }

        public IActionResult Index() //this was boilerplate
        {
            return View();
        }

        //sending data to and from a page in MVC is harder than in
        //Razor Page or MVVM projects because Views can show info sent by a controller
        //but Views can't ask for info from the controller.
        //ie, no two-way binding.
        //But obv there are ways to make it happen.
        //Let's start with creating a model to send around.

        //ours:
        //OrderCreateModel was created just to use here.
        //This method only deals with part of that model: FoodItems.
        //but Order will play a role in the view for Create.
        public async Task<IActionResult> Create() //by default, this is GET
        {
            var food = await _foodData.GetFood();

            OrderCreateModel model = new OrderCreateModel(); //that's the model we created just for this.

            food.ForEach(x =>
            {
                model.FoodItems.Add(new SelectListItem { Value = x.Id.ToString(), Text = x.Title });
            });
            //this takes each item in food, and adds it to the list of FoodItems
            //defined in OrderCreateModel. For the dropdown.

            return View(model); //note we send the whole model, not just FoodItems.

        }

        //overloaded method:
        [HttpPost] //needed to make it a POST
        public async Task<IActionResult> Create(OrderModel order)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }

            var food = await _foodData.GetFood();

            order.Total = order.Quantity * food.Where(x => x.Id == order.FoodId).First().Price;

            int id = await _orderData.CreateOrder(order);

            return RedirectToAction("Create"); //sends user back to unfilled Create page.
        }

        //the sum of what happens with those two methods:
        //Create() makes the template on the Create.cshtml page,
        //including populating the dropdown with the food items.
        //Like the "outbound" flow of some data (food list).
        //Create(OrderModel) is what "reads" the user's input on .cshtml page,
        //and handles the "inbound" flow of the data.
        //IE, we fake two-way binding.
    }
}