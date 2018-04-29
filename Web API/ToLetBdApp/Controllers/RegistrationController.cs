using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToLetBdEntity;
using ToLetBdRepository;

namespace ToLetBdApp.Controllers
{
    public class RegistrationController : Controller
    {
        UserRepository userRepo = new UserRepository();
        UserTypeRepository userTypeRepo = new UserTypeRepository();

        [HttpGet]
        public ActionResult Index()
        {
            List<UserType> usertypes = userTypeRepo.GetAll();
            ViewBag.userTypes = usertypes;

            return View();
        }

        [HttpPost]
        public ActionResult Index(User u)
        {
            if (ModelState.IsValid)
            {
                userRepo.Insert(u);
                TempData["msg"] = "Registration Success!!";
                return RedirectToAction("Index","Login");
            }
            else
            {
                ModelState.Values.SelectMany(v => v.Errors).ElementAt(0);
                List<UserType> usertypes = userTypeRepo.GetAll();
                ViewBag.userTypes = usertypes;
                return View();
            }

        }

    }
}
