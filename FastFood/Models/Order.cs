using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FastFood.Models
{
    public class Order
    {
        [Key]
        public int orderId { get; set; }
        public int userId { get; set; }
        public int foodId { get; set; }
        public DateTime orderDate { get; set; }
        public int restaurantId { get; set; }
        public Boolean status { get; set; }

    }
}