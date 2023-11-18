using Assignment1.DTOs;
using Assignment1.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace Assignment1.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            var db = new Zero_HungerEntities3();
            var emp = db.Employees.Find(Session["EmpId"]);
            var data = (from u in db.Foods where u.Assign == emp.EmpName && u.Status == "Deleverd" select u);
            return View(data);
            
        }
        public ActionResult Assignfood()
        {
            var db = new Zero_HungerEntities3();
            //var emp = db.Employees.Find(Session["EmpId"]);
            var data = (from u in db.Foods where u.Status == "Assigned" select u);
            return View(data);

        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string EmpName, string Password)
        {
            var db = new Zero_HungerEntities3();
            
            var name = (from u in db.Employees where u.EmpName == EmpName && u.Password == Password select u).SingleOrDefault();
            if (name != null)
            {
                Session["EmpId"] = name.EmpId;
                Session["Name"] = name.EmpName;
                return RedirectToAction("Index");

            }
            TempData["Msg"] = "Username Password Invalid";
            return View();
           
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(EmployeeDTO e)
        {
            if(ModelState.IsValid)
            {
                var db = new Zero_HungerEntities3();
                db.Employees.Add(Convert(e));
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(e);
        }

         [HttpGet]
         public ActionResult Assign()
         {
             var db = new Zero_HungerEntities3();
             var emp = db.Employees.Find(Session["EmpId"]);
             var data = (from u in db.Foods where u.Assign == emp.EmpName && u.Status=="Assigned" select u).SingleOrDefault();

             return View(data);
         }
        /*
         [HttpPost]
         public ActionResult Assign(FoodDTO f)
         {
             if (ModelState.IsValid)
             {
                 var db = new Zero_HungerEntities3();
                 var food = db.Foods.Find(f.FoodId);
                 if (food != null)
                 {
                     food.FoodName = f.FoodName;
                     food.FoodAmount = f.FoodAmount;
                     food.PreserveTime = f.PreserveTime;
                     food.Location = f.Location;
                     food.Status = f.Status;
                     food.Assign = f.Assign;
                     food.ResId = f.ResId;

                     db.SaveChanges();
                 }
                 var emp = db.Employees.Find(Session["EmpId"]);
                 if (emp != null)
                 {
                     emp.Status = "NULL";
                     db.SaveChanges();
                 }
             }

             return RedirectToAction("Index");
         }*/
        [HttpPost]
        public ActionResult Assign(int id)
        {
            var db = new Zero_HungerEntities3();
            var food = db.Foods.Find(id);
            if (food != null)
            {
                food.Status = "Deleverd";
                
                db.SaveChanges();
            }
            var emp = db.Employees.Find(Session["EmpId"]);
            if (emp != null)
            {
                emp.Status = "NULL";
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
      
        public ActionResult Cancel(int id)
        {
            var db = new Zero_HungerEntities3 ();
            var food = db.Foods.Find(id);
            var employee = db.Employees.SingleOrDefault(e => e.EmpName == food.Assign);

            if (employee != null)
            {
                employee.Status = "NULL";
                db.SaveChanges();
            }

            if (food != null)
            {
                food.Status = "Available";
                food.Assign = "NULL";
                db.SaveChanges();
            }
           
            return RedirectToAction("Index");
        }

        public EmployeeDTO Convert(Employee e)
        {
            var Emp = new EmployeeDTO()
            {
                EmpName = e.EmpName,
                Password = e.Password
            };
            return Emp;
        }
        public Employee Convert (EmployeeDTO e)
        {
            var Emp = new Employee()
            {
                EmpName = e.EmpName,
                Password = e.Password
            };
            return Emp;
        }
    }
}