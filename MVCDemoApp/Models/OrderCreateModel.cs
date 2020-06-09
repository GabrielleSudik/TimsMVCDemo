using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemoApp.Models
{
    //We made this model to make our OrdersController.Create() work.
    //We didn't need it in the Razor Pages app. Why?
    //Because the "views" in that app had two-way binding with
    //both OrderModel and List of FoodItems.
    //Here, we need to wrap those up into a new model that itself
    //can be sent to the view. Tab back to OrdersController.Create()

    public class OrderCreateModel
    {
        public OrderModel Order { get; set; } = new OrderModel();
        public List<SelectListItem> FoodItems { get; set; } = new List<SelectListItem>();
        //We use FoodItems in OrdersController.Create() to make the drop-down list.
        //We use Model in OrdersController.Create(OrderModel) (overloaded method).

    }
}
