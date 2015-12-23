using System.Data.Entity;
using FastFood.Models;
namespace FastFood.DataAccessLayer
{
    public class FastFoodDAL : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Admin> Admins  { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().ToTable("UsersTbl");
            modelBuilder.Entity<Restaurant>().ToTable("RestaurantsTbl");
            modelBuilder.Entity<Food>().ToTable("FoodTbl").HasRequired(s=>s.restaurant);
            modelBuilder.Entity<Admin>().ToTable("AdminTbl").HasRequired(s=>s.restaurant);

            base.OnModelCreating(modelBuilder);
        }

    }
}