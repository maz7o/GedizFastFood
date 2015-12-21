using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FastFood.Models;
namespace FastFood.DataAccessLayer
{
    public class FastFoodDAL : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Food> Foods { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().ToTable("UsersTbl");
            modelBuilder.Entity<Restaurant>().ToTable("RestaurantsTbl");
            modelBuilder.Entity<Food>().ToTable("FoodTbl");


            base.OnModelCreating(modelBuilder);
        }

    }
}