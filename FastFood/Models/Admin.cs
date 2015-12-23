using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FastFood.Models;
namespace FastFood.Models
{
    public class Admin
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public virtual Restaurant restaurant { get; set; }
    }
}