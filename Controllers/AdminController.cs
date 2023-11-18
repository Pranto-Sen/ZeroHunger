using Assignment1.DTOs;
using Assignment1.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment1.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            var db = new Zero_HungerEntities3();
            var data = db.Foods.ToList();
            return View(data);
        }
        public ActionResult Availablefood()
        {
            var db = new Zero_HungerEntities3();
            //var emp = db.Employees.Find(Session["EmpId"]);
            var data = (from u in db.Foods where u.Status == "Available" select u);
            return View(data);

        } 
        public ActionResult Assignedfood()
        {
            var db = new Zero_HungerEntities3();
            //var emp = db.Employees.Find(Session["EmpId"]);
            var data = (from u in db.Foods where u.Status == "Assigned" select u);
            return View(data);

        }
        public ActionResult Deleverdfood()
        {
            var db = new Zero_HungerEntities3();
            //var emp = db.Employees.Find(Session["EmpId"]);
            var data = (from u in db.Foods where u.Status == "Deleverd" select u);
            return View(data);

        }
        [HttpGet]
        public ActionResult Details(int id)
        {

            var db = new Zero_HungerEntities3();
            ViewBag.Employees = db.Employees.ToList();
            ViewBag.Foods = db.Foods.ToList();
            var st = (from s in db.Foods
                      where s.FoodId == id
                      select s).SingleOrDefault();
            return View(st);
        }

        //Assign to employee to pickup the food , foods table Assign and Status is changed
        [HttpPost]
        public ActionResult Details(FoodDTO f)
        {
            if (ModelState.IsValid)
            {
                var db = new Zero_HungerEntities3();
                {
                    // Find the food record
                    var food = db.Foods.Find(f.FoodId);

                    if (food != null)
                    {
                        // Update fields in the Food table
                        food.FoodName = f.FoodName;
                        food.FoodAmount = f.FoodAmount;
                        food.PreserveTime = f.PreserveTime;
                        food.Location = f.Location;
                        food.Status = f.Status;
                        food.Assign = f.Assign;
                        food.ResId = f.ResId;

                        // Save changes to the Food table
                        db.SaveChanges();

                        // Find the corresponding employee record
                        var employee = db.Employees.SingleOrDefault(e => e.EmpName == f.Assign);

                        if (employee != null)
                        {
                            // Update the Status field in the Employee table
                            employee.Status = "Assigned";

                            // Save changes to the Employee table
                            db.SaveChanges();
                        }
                        else
                        {
                            // Handle the case where the employee record is not found
                            ModelState.AddModelError(string.Empty, "Employee not found");
                        }

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Handle the case where the food record is not found
                        ModelState.AddModelError(string.Empty, "Food not found");
                    }
                }
            }

            return View(f);
        }

        //public ActionResult Details(FoodDTO f)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var db = new Zero_HungerEntities3();
        //       var food = db.Foods.Find(f.FoodId);
        //        //var emp = (from s in db.Employees where s.EmpName == f.Assign select s).SingleOrDefault();

        //        //food.FoodId = f.FoodId;
        //        food.FoodName = f.FoodName;
        //        food.FoodAmount = f.FoodAmount;
        //        food.PreserveTime = f.PreserveTime;
        //        food.Location   = f.Location;
        //        food.Location  =f.Location;
        //        food.Status = f.Status;
        //        food.Assign = f.Assign;
        //        food.ResId = f.ResId;
        //        //f.ResId = (int)Session["ResId"];
        //        //db.Foods.(Convert(f));
        //        db.SaveChanges();
        //        return RedirectToAction("Index");

        //    }
        //    return View(f);
        //}
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
                Assign = f.Assign,
                ResId = f.ResId,

            };
            return food;
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {

            var db = new Zero_HungerEntities3();
            var name = (from u in db.Admins where u.Username == Username && u.Password == Password select u).SingleOrDefault();
            if(name != null) {
                Session["admin"] = name;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Msg"] = "Username Password Invalid";
            }
            return View();
        }

    }
}