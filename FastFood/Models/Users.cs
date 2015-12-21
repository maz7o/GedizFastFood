using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace FastFood.Models
{
    public class Users
    {
        [Key]
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string studentId { get; set; }
        public float balance { get; set; }
        public string mail { get; set; }
        public string password { get; set; }

    }
}