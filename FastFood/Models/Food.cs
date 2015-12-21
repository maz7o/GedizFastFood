using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastFood.Models
{
    public class Food
    {
        [Key]
        public int foodId { get; set; }
        public string foodName { get; set; }
        public float foodPrice { get; set; }
        public string foodImage { get; set; }
        public virtual Restaurant restaurant { get; set; }
    }
}