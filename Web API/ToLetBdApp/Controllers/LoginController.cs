using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToLetBdEntity;
using ToLetBdRepository;

namespace ToLetBdApp.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        UserRepository userRepo = new UserRepository();
        UserTypeRepository userTypeRepo = new UserTypeRepository();
        
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User u)
        {
            if (ModelState.IsValid)
            {
                User Validuser = userRepo.GetByEmailAndPass(u);

                if (Validuser != null && Validuser.UserTypeId==1)
                {
                    Session["user"] = Validuser;
                    return RedirectToAction("Index", "Admin");
                }
                else if (Validuser != null && Validuser.UserTypeId == 2)
                {
                    Session["user"] = Validuser;
                    return RedirectToAction("Index", "HouseOwner");
                }
                else if (Validuser != null && Validuser.UserTypeId == 3)
                {
                    Session["user"] = Validuser;
                    return RedirectToAction("Index", "Renter");
                }
                else
                {
                    @TempData["msg"] = "Login Invalid";
                    return View(u);
                }
            }
            else {
                ModelState.Values.SelectMany(v => v.Errors).ElementAt(0);
                @TempData["msg"] = "Login Invalid";
                return View();
            }

            
            
        }

        

        private bool IsvalidContentType(string contentType)
        {
            return contentType.Equals("image/png") || contentType.Equals("image/jpg") || contentType.Equals("image/jpeg") || contentType.Equals("image/gif");
        }

        

    }
}
