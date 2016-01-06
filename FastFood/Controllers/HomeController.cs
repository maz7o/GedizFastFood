using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FastFood.DataAccessLayer;
using FastFood.Models;
namespace FastFood.Controllers
{
    public class HomeController : Controller
    {
        private FastFoodDAL db = new FastFoodDAL();
        public ActionResult Index()
        {
            List<Restaurant> restaurants = db.Restaurants.ToList();
            ViewData["Restaurants"] = restaurants;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }



        public ActionResult Reservation(int? id)
        {
            if (Session["user"] != null)
            {
                if (id != null)
                {
                    var food = db.Foods.ToList().First(f => f.foodId == id);
                    
                    ViewData["Food"] = food;
                    return View();

                }
                else
                {
                    return View("Foods");
                }
            }
            else
            {
                return View("Login");
            }   
        }

        public ActionResult Order(int id)
        {
            Food food = GetFood(id);
            Users user = GetUser();
            if (user.balance<food.foodPrice)
            {
                return View("Failed");
            }
            else
            {
                Order order = new Order();
                order.foodId = food.foodId;
                order.restaurantId = food.restaurant.restaurantId;
                order.orderDate = DateTime.Now;
                order.status = false;
                order.userId = Convert.ToInt32(user.studentId);
                db.Orders.Add(order);
                user.balance = user.balance - food.foodPrice;
                db.Users.Attach(user);
                var entry = db.Entry(user);
                entry.Property(e => e.balance).IsModified = true;
                // other changed properties
                db.SaveChanges();
                return View("Index");
            }

        }


        public ActionResult Foods(int? id)
        {
            if (id == null)
            {
                ViewBag.Message = "Foods";
                List<Food> foods = db.Foods.ToList();
                ViewData["Foods"] = foods;

                return View();
            }
            else
            {
                List<Food> foods = db.Foods.ToList();
                List<Food> restaurantFoods = new List<Food>();
                foreach (Food food in foods)
                {
                    if(food.restaurant.restaurantId == id)
                    {
                        restaurantFoods.Add(food);
                    }
                }
                ViewData["Foods"] = restaurantFoods;
                return View();

            } 
        }

        public ActionResult Contact()
        {
            return View();

        }

        public ActionResult Login()
        {
            if(Session["user"]!=null)
            {
                Users user = GetUser();
                List<ReservedFoods> foods = new List<ReservedFoods>();
                //Users user = db.Users.ToList().First(i => i.studentId == Session["userid"].ToString());
                ViewData["User"] = user;
                foreach (Order order in db.Orders.ToList())
                {
                    if (order.userId == Convert.ToInt32(user.studentId) && order.status == false)
                    {
                        ReservedFoods reserved = new ReservedFoods();
                        Food food = GetFood(order.foodId);
                        reserved.food = food;
                        reserved.date = order.orderDate;
                        foods.Add(reserved);
                    }
                }
                ViewData["Foods"] = foods;
                return View("Account");
            }
            else
            {
                return View();
            }
            
        }

        public String ValidateUser(String userId, String password)
        {
            List<Users> users = db.Users.ToList();
            foreach (Users user in users)
            {
                if (user.studentId == userId && user.password==password)
                {
                    Session["user"] = user;
                    return "1";
                }



            }
            return "0";

        }

        public Boolean userExist(String userid)
        {
            List<Users> users = db.Users.ToList();
            foreach (Users user in users)
            {
                if (user.studentId == userid)
                {
                    //Session["userid"] = user.studentId;
                    return true;
                }
            }
            return false;

        }


        public ActionResult Logout()
        {
            Session.RemoveAll();
            return View("Login");
        }

        private Users GetUser()
        {
            return (Users)Session["user"];
        }

        private Food GetFood(int id)
        {

            foreach (Food food in db.Foods.ToList())
            {
                if (food.foodId == id)
                    return food;
            }
            return null;
        }



    }
}