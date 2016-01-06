using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FastFood.Models
{
    public class ReservedFoods
    {
        public Food food { get; set; }
        public Boolean status { get; set; }
        public DateTime date { get; set; }
        public int studentId { get; set; }
        public int orderId { get; set; }
    }
}