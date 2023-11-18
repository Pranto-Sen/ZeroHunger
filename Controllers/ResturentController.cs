using Assignment1.DTOs;
using Assignment1.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment1.Controllers
{
    public class ResturentController : Controller
    {
        // GET: Resturent
       
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string ResName, string Password)
        {
            var db = new Zero_HungerEntities3();
            //var name = db.Students.Where(u=>u.Username == Username && u.Password == Password);
            var name = (from u in db.Restaurants where u.ResName == ResName && u.Password == Password select u).SingleOrDefault();
            if (name != null)
            {
                Session["ResId"] = name.ResId;
                return RedirectToAction("Index", "Food");

            }
            TempData["Msg"] = "Username Password Invalid";
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(ResturentDTO r)
        {
            if (ModelState.IsValid)
            {
                var db = new Zero_HungerEntities3();
                db.Restaurants.Add(Convert(r));
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(r);
        }


        public ResturentDTO Convert(Restaurant r)
        {
            var Res = new ResturentDTO()
            {
                ResName = r.ResName,
                Password = r.Password
            };
            return Res;
        }
        public Restaurant Convert(ResturentDTO r)
        {
            var Res = new Restaurant()
            {
                ResName = r.ResName,
                Password = r.Password
            };
            return Res;
        }
    }
}