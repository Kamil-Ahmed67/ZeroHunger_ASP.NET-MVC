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
    public class EmployeeController : Controller
    {// GET: Employee
        [HttpGet]
        public ActionResult EmployeeIndex()
        {
            var db = new ZeroHunger1Entities1();
            var collectRequests = db.CollectRequests.ToList();
            return View(collectRequests);
        }
        [HttpGet]
        public ActionResult EmployeeLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EmployeeLogin(EmployeeLoginDTO LogIn)
        {
            if (ModelState.IsValid)
            {

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<EmployeeLoginDTO, Emplyee>();
                });
                var mapper = new Mapper(config);
                var data = mapper.Map<Emplyee>(LogIn);

                using (var db = new ZeroHunger1Entities1())
                {
                    var verify = db.Emplyees.SingleOrDefault(u => u.Email == LogIn.Email && u.Password == LogIn.Password);

                    if (verify != null)
                    {
                        // Map FoodDonorLoginDTO to FoodDonor
                        Session["id"] = verify.EmpId;  // Assigning the user's ID to the session
                        Session["user"] = verify.Email;
                        Session["password"] = verify.Password.Trim();
                        Session["type"] = "Employee";

                        return RedirectToAction("EmployeeIndex", "Employee");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid login credentials.");
                    }
                }
            }

            return View(LogIn);
        }
        [HttpGet]

         public ActionResult EmployeeCollected(int requestId)
        {
            var db= new ZeroHunger1Entities1();
            var c_request = db.CollectRequests.Find(requestId);

            if (c_request != null)
            {
                c_request.Status = "Collected";
                db.SaveChanges();
                return RedirectToAction("EmployeeIndex");
            }
            else
            {
                TempData["Msg"] = "Invalid Request";
                return RedirectToAction("EmployeeIndex");
            }
        }
        [HttpGet]
        public ActionResult EmployeeFoodDistribution()
        {

            var db = new ZeroHunger1Entities1();
            var foodDist = db.FoodDistributions.ToList();
            return View(foodDist);

        }
    }
}