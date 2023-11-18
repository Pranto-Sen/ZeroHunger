using Assignment1.DTOs;
using Assignment1.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;

namespace Assignment1.Controllers
{
    public class FoodController : Controller
    {
        // GET: Food
        public ActionResult Index()
        {
            var db = new Zero_HungerEntities3();
            var data = db.Foods.ToList();
            return View(data);
        }
      

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FoodDTO f)
        {
            if (ModelState.IsValid)
            {
                var db = new Zero_HungerEntities3();
                f.Status = "Available";
                f.ResId = (int)Session["ResId"];
                db.Foods.Add(Convert(f));
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(f);
        }

        public ActionResult Foodlist()
        {
            var db = new Zero_HungerEntities3();
            //var res = db.Foods.Find(Session["ResId"]);
            //var data = (from u in db.Foods where u.ResId == (int)Session["ResId"] select u).ToList();

            //int resId = Session["ResId"];

            //var data = db.Foods.Where(u => u.ResId == resId).ToList();

            var resId = Session["ResId"];

            var data = db.Foods.Where(u => u.ResId == (int)resId).ToList();

            return View(data);
        }

        public FoodDTO Convert(Food f)
        {
            var food = new FoodDTO()
            {
                FoodId = f.FoodId,
                FoodName = f.FoodName,
                PreserveTime = f.PreserveTime,
                FoodAmount = f.FoodAmount,
                Location = f.Location,
                Status = f.Status,
                ResId = f.ResId,
            };
            return food;
        }
        public Food Convert(FoodDTO f)
        {
            var food = new Food()
            {
                FoodId = f.FoodId,
                FoodName = f.FoodName,
                PreserveTime = f.PreserveTime,
                FoodAmount = f.FoodAmount,
                Location = f.Location,
                Status = f.Status,
                ResId = f.ResId,

            };
            return food;
        }
        public List<FoodDTO> Convert(List<Food> Foods)
        {
            var Fds = new List<FoodDTO>();
            foreach (var f in Foods)
            {
                Fds.Add(Convert(f));
            }
            return Fds;
        }
    }
}