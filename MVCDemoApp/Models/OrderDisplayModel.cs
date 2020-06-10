using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemoApp.Models
{
    //created to work with OrderControllers.Display() method
    public class OrderDisplayModel
    {
        public OrderModel Order { get; set; }
        public string ItemPurchased { get; set; }
    }
}
