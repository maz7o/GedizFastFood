﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using FastFood.DataAccessLayer;
using FastFood.Models;
namespace FastFood.Controllers
{
    public class HomeController : Controller
    {
        FastFoodDAL db = new FastFoodDAL();
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
            if (Session["userid"] != null)
            {
                if (id != null)
                {
                    FastFood.Models.Food food = db.Foods.ToList().First(f => f.foodId == id);
                    
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
            if(Session["userid"]!=null)
            {
                
                Users user = db.Users.ToList().First(i => i.studentId == Session["userid"].ToString());
                ViewData["User"] = user;
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
                    Session["userid"] = user.studentId;
                    return "1";
                }



            }
            return "0";

        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return View("Login");
        }




    }
}