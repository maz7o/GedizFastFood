﻿using System;
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

    }
}