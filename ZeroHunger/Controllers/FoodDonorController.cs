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
    public class FoodDonorController : Controller
    {
        // GET: FoodDonor
        [HttpGet]
        public ActionResult FoodDonorIndex()
        {
            return View();
        }
        [HttpGet]
        public ActionResult FoodDonorLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FoodDonorLogin(FoodDonorLoginDTO LogIn)
        {
            if (ModelState.IsValid)
            {
                
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<FoodDonorLoginDTO, FoodDonor>();
                });
                var mapper = new Mapper(config);
                var data = mapper.Map<FoodDonor>(LogIn);

                using (var db = new ZeroHunger1Entities1())
                {
                    var verify = db.FoodDonors.SingleOrDefault(u => u.Email == LogIn.Email && u.Password == LogIn.Password);

                    if (verify != null)
                    {
                        // Map FoodDonorLoginDTO to FoodDonor
                        Session["id"] = verify.DonorId;  // Assigning the user's ID to the session
                        Session["user"] = verify.Email;
                        Session["password"] = verify.Password.Trim();
                        Session["type"] = "FoodDonor";

                        return RedirectToAction("FoodDonorIndex", "FoodDonor");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid login credentials.");
                    }
                }
            }

            return View(LogIn);
        }
        //Food Donor Creating Collect Request
        [HttpGet]
        public ActionResult FoodDonorCollectRequest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FoodDonorCollectRequest(FoodDonorCollectRequestDTO obj)
        {
            if (ModelState.IsValid)
            {
                var db = new ZeroHunger1Entities1();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<FoodDonorCollectRequestDTO, CollectRequest>();
                });
                var mapper = new Mapper(config);
                var data = mapper.Map<CollectRequest>(obj);

                data.DonorId = (int)Session["id"];

                db.CollectRequests.Add(Convert(obj));
                db.SaveChanges();

                TempData["Msg"] = $"Collect Request Placed Successfully with request id {data.DonorId}";
                return RedirectToAction("FoodDonorIndex");
            }
            return View(obj);

        }
        public FoodDonorCollectRequestDTO Convert(CollectRequest s)
        {
            var st = new FoodDonorCollectRequestDTO()
            {
                CollectRequest_Id = s.CollectRequest_Id,
                Food_Dscription = s.Food_Dscription,
                Quantity = s.Quantity,
                Expired_Time = s.Expired_Time,
                Address = s.Address,
                Preffered_Time = s.Preffered_Time,
                DonorId = s.DonorId,
                Status=s.Status
            };
            return st;

        }
        public CollectRequest Convert(FoodDonorCollectRequestDTO p)
        {
            var st = new CollectRequest()
            {
                CollectRequest_Id = p.CollectRequest_Id,
                Food_Dscription = p.Food_Dscription,
                Quantity = p.Quantity,
                Expired_Time = p.Expired_Time,
                Address = p.Address,
                Preffered_Time = p.Preffered_Time,
                DonorId = p.DonorId,
                Status=p.Status
            };
            return st;
        }
        public List<FoodDonorCollectRequestDTO> Convert(List<CollectRequest> obj)
        {
            var sts = new List<FoodDonorCollectRequestDTO>();
            foreach (var s in obj)
            {
                sts.Add(Convert(s));
            }
            return sts;
        }

        //********Food Donor Managing Collect Requesr**********
        public ActionResult FoodDonorRequests()
        {
            var db = new ZeroHunger1Entities1();
            int foodDonorId = (int)Session["id"]; // Retrieve food donor ID from session
            var foodDonations = db.CollectRequests.Where(c => c.DonorId == foodDonorId).ToList();

            return View(foodDonations);
        }
        [HttpGet]
        public ActionResult FoodDonorRegistration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FoodDonorRegistration(FoodDonorDTO obj)
        {
            if (ModelState.IsValid)
            {


                var db = new ZeroHunger1Entities1();
                var foodDonor = new FoodDonor();
                foodDonor.Name = obj.Name;
                foodDonor.Address = obj.Address;
                foodDonor.Phone = obj.Phone;
                foodDonor.Email = obj.Email;
                foodDonor.Password = obj.Password;

                db.FoodDonors.Add(foodDonor);
                db.SaveChanges();

                TempData["Msg"] = "Food Donor registered successfully";
                return RedirectToAction("FoodDonorIndex");
            }
            return View(obj);
        }
        //creating edit ,details and delete
        [HttpGet]
        public ActionResult CollectReqEdit(int id)
        {
            var db = new ZeroHunger1Entities1();
            var data = db.CollectRequests.Find(id);
            return View(data);
        }
        public ActionResult CollectReqDetails(int id)
        {
            var db = new ZeroHunger1Entities1();
            var dept = db.CollectRequests.Find(id);

            return View(dept);

        }
        [HttpPost]
        public ActionResult CollectReqEdit(CollectRequest d)
        {
            var db = new ZeroHunger1Entities1();
            var ex = db.CollectRequests.Find(d.CollectRequest_Id);
            ex.Food_Dscription = d.Food_Dscription;
            ex.Quantity = d.Quantity;
            ex.Expired_Time = d.Expired_Time;
            ex.Address = d.Address;
            ex.Preffered_Time = d.Preffered_Time;
            db.SaveChanges();
            return RedirectToAction("FoodDonorRequests");
        }
        public ActionResult CollectReqDelete(int id)
        {
            var db = new ZeroHunger1Entities1();
            var ex = db.CollectRequests.Find(id);
            db.CollectRequests.Remove(ex);
            db.SaveChanges();
            return RedirectToAction("FoodDonorRequests");
        }

    }
}