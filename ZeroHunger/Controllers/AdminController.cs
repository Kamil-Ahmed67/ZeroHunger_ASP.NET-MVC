using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.DTO;
using ZeroHunger.EF;

namespace ZeroHunger.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [HttpGet]
        public ActionResult AdminIndex()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(AdminLoginDTO LogIn)
        {
            if (ModelState.IsValid)
            {

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AdminLoginDTO, Admin>();
                });
                var mapper = new Mapper(config);
                var data = mapper.Map<Admin>(LogIn);

                using (var db = new ZeroHunger1Entities1())
                {
                    var verify = db.Admins.SingleOrDefault(u => u.Email == LogIn.Email && u.Password == LogIn.Password);

                    if (verify != null)
                    {
                        // Map FoodDonorLoginDTO to FoodDonor
                        Session["id"] = verify.Id;  // Assigning the user's ID to the session
                        Session["user"] = verify.Email;
                        Session["password"] = verify.Password.Trim();
                        Session["type"] = "Admin";

                        return RedirectToAction("AdminIndex", "Admin");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid login credentials.");
                    }
                }
            }

            return View(LogIn);
        }
        //ADMIN RECRUITING EMPLOYEE THROUGH EmployeeDTO
        [HttpGet]
        public ActionResult AdminAddedEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminAddedEmployee(EmployeeDTO obj)
        {
            if (ModelState.IsValid)
            {


                var db = new ZeroHunger1Entities1();
                var emp = new Emplyee();
                emp.Name = obj.Name;
                emp.Email = obj.Email;
                emp.Password = obj.Password;
                emp.Gender = obj.Gender;
                emp.Status = obj.Status;

                db.Emplyees.Add(emp);
                db.SaveChanges();

                TempData["Msg"] = " SUCCESSFULLY RECCRUTTED AN EMPLOYEE FOR COLLECTIONG ORDER!!";
                return RedirectToAction("AdminIndex");
            }
            return View(obj);
        }
        //ADMIN EDIT PROFILE
        [HttpGet]
        public ActionResult AdminEditProfile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminEditProfile(AdminDTO obj)
        {
            if (ModelState.IsValid)
            {


                var db = new ZeroHunger1Entities1();
                var foodDonor = new Admin();
                foodDonor.Username = obj.Username;
                foodDonor.Email = obj.Email;
                foodDonor.Password = obj.Password;

                db.Admins.Add(foodDonor);
                db.SaveChanges();

                TempData["Msg"] = "Food Donor registered successfully";
                return RedirectToAction("FoodDonorIndex");
            }
            return View(obj);
        }
    }
}