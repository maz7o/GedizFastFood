using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace FastFood.Models
{
    public class Restaurant
    {
        public int restaurantId { get; set; }
        public string restaurantImage { get; set; }
        public string restaurantName { get; set; }
        public string phoneNumber { get; set; }
        public virtual ICollection<Food> Foods { get; set; }
    }
}