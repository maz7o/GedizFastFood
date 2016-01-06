using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FastFood.Models;
using System.Data.Entity;
using FastFood.DataAccessLayer;
namespace FastFood.Controllers
{
    public class AdminController : Controller
    {
        FastFoodDAL db = new FastFoodDAL();
        // GET: Admin
        public ActionResult Index()
        {
            if (isLoggedIn())
            {
                Admin admin = (Admin)Session["admin"];
                Restaurant restaurant = db.Restaurants.ToList().First(rest => rest.restaurantId == admin.restaurant.restaurantId);
                ViewData["Restaurant"] = restaurant; 
                return View();
            }
            else
            {
                return View("Login");

            }

        }

        public ActionResult Account()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewData["restaurantId"] = getAdmin().restaurant.restaurantId;
            return View();
        }

        public ActionResult Orders()
        {
            List<ReservedFoods> foods = new List<ReservedFoods>();

            Admin admin = getAdmin();
            foreach (Order order in db.Orders.ToList())
            {
                if (order.restaurantId == admin.restaurant.restaurantId && order.status == false)
                {
                    ReservedFoods reserved = new ReservedFoods();
                    Food food = GetFood(order.foodId);
                    reserved.orderId = order.orderId;
                    reserved.studentId = order.userId;
                    reserved.food = food;
                    reserved.date = order.orderDate;
                    foods.Add(reserved);
                }
            }
            ViewData["Foods"] = foods;
            return View();

        }

        public ActionResult Order(int id)
        {
            Order order = db.Orders.ToList().First(ord => ord.orderId == id);
            order.status = true;
            db.Orders.Attach(order);
            var entry = db.Entry(order);
            entry.Property(e => e.status).IsModified = true;
            // other changed properties
            db.SaveChanges();
            return RedirectToAction("Orders");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "foodId,foodName,foodPrice,foodImage")] Food food, int restaurantId, HttpPostedFileBase file)
        {
            string pic = System.IO.Path.GetFileName(file.FileName);
            string path = System.IO.Path.Combine(
            Server.MapPath("~/Content/Images"), pic);
            string picPath = "/Content/Images/" + pic;
            food.foodImage = picPath;
            // file is uploaded
            file.SaveAs(path);
            if (ModelState.IsValid)
                {
                    food.restaurant = GetRestaurant(restaurantId);
                    db.Foods.Add(food);
                    db.SaveChanges();
                    return RedirectToAction("Foods");
                }

                return View(food);
         
        }

        public ActionResult Login()
        {
            if(isLoggedIn())
            {
                
                ViewData["Admin"] = (Admin)Session["admin"];
                return View("Account");
            }
            else
            {
                return View("Login");
            }
        }
        public ActionResult Foods()
        {
            if (isLoggedIn())
            {
                Admin admin = (Admin)Session["admin"];
                
                List<Food> foods = db.Foods.ToList();
                List<Food> myFoods = new List<Food>();
                foreach (Food food in foods)
                {
                    if(food.restaurant.restaurantId==admin.restaurant.restaurantId)
                    {
                        myFoods.Add(food);
                    }
                }

                ViewData["Foods"] = myFoods;
                return View();
            }
            else
            {
                return View("Login");

            }

        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return View("Login");
            
        }


        public String ValidateAdmin(String userId, String password)
        {
            List<Admin> admins = db.Admins.ToList();
            foreach (Admin admin in admins)
            {
                if (admin.userName == userId && admin.password == password)
                {
                    Session["admin"] = admin;
                    //Session["restaurantid"] = admin.restaurant.restaurantId;
                    return "1";
                }

            }
            return "0";
        }

        public Restaurant GetRestaurant(int id)
        {
            return db.Restaurants.Where(x => x.restaurantId == id).FirstOrDefault();
        }

        public ActionResult Delete(int? id)
        {
            if (isLoggedIn())
            {
                if (id == null)
                {
                    RedirectToAction("Foods");
                }
                Food food = db.Foods.Find(id);
                if (food == null)
                {
                    RedirectToAction("Foods");
                }
                else if (food.restaurant.restaurantId != getAdmin().restaurant.restaurantId)
                {
                    RedirectToAction("Foods");
                }
                else
                {
                    ViewData["Food"] = food;
                    return View();
                }

                
            }
            else
            {
                return View("Login");
            }
            return View("Foods");

           
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Food food = db.Foods.Find(id);
            db.Foods.Remove(food);
            db.SaveChanges();
            return RedirectToAction("Foods");
        }

        public ActionResult Edit(int? id)
        {
            if (isLoggedIn())
            {
                if (id == null)
                {
                    RedirectToAction("Foods");
                }
                Food food = db.Foods.Find(id);
                if (food == null)
                {
                    RedirectToAction("Foods");
                }
                else if (food.restaurant.restaurantId != getAdmin().restaurant.restaurantId)
                {
                    RedirectToAction("Foods");
                }
                else
                {
                    
                    return View(food);
                }


            }
            else
            {
                return View("Login");
            }
            return View("Foods");
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "foodId,foodName,foodPrice,foodImage")] Food food)
        {
            if (ModelState.IsValid)
            {
                db.Entry(food).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Foods");
            }
            return View(food);
        }

        private bool isLoggedIn()
        {
            if (Session["admin"] == null)
            {
                return false;
            }
            else
                return true;
        }
        private Admin getAdmin()
        {
            return (Admin)Session["admin"];
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