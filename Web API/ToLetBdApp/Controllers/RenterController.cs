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
    public class RenterController : Controller
    {
        UserRepository userRepo = new UserRepository();
        UserTypeRepository userTypeRepo = new UserTypeRepository();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {

            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Profile()
        {

            User admin = (User)Session["user"];

            return View();
        }

        private bool IsvalidContentType(string contentType)
        {
            return contentType.Equals("image/png") || contentType.Equals("image/jpg") || contentType.Equals("image/jpeg") || contentType.Equals("image/gif");
        }

        [HttpPost]
        public ActionResult Profile(User u)
        {

            HttpPostedFileBase photo = Request.Files["photo"];

            if (photo.ContentLength > 0)
            {

                if (!IsvalidContentType(photo.ContentType))
                {

                    TempData["msg"] = "Image is invalid!!";
                    return View();
                }
                else
                {
                    if (photo.ContentLength > 0)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(photo.FileName);
                        string extension = Path.GetExtension(photo.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                        photo.SaveAs(path);

                        if (ModelState.IsValid)
                        {


                            u.ImgPath = fileName;

                            userRepo.UpdateWithImg(u);
                            User us = userRepo.Get(u.Id);
                            Session["user"] = us;
                            User admin = (User)Session["user"];

                            TempData["msg"] = "Profile updated successfully!!";
                            return RedirectToAction("Profile");

                        }
                        else
                        {
                            ModelState.Values.SelectMany(v => v.Errors).ElementAt(0);
                            return View();
                        }



                    }
                    return View();
                }

            }
            else
            {

                if (ModelState.IsValid)
                {



                    userRepo.Update(u);
                    User us = userRepo.Get(u.Id);
                    Session["user"] = us;
                    User admin = (User)Session["user"];

                    TempData["msg"] = "Profile updated successfully!!";
                    return RedirectToAction("Profile");

                }
                else
                {
                    ModelState.Values.SelectMany(v => v.Errors).ElementAt(0);
                    return View();
                }
            }



        }

        [HttpGet]
        public ActionResult Settings()
        {

            User admin = (User)Session["user"];
            ViewBag.admin = admin;
            return View();
        }

        [HttpPost]
        public ActionResult Settings(FormCollection form)
        {


            if (form["Newpassword"] == form["cpassword"])
            {
                if (String.IsNullOrEmpty(form["cpassword"]) || form["cpassword"].Length < 6)
                {
                    TempData["msg"] = "Password can't be empty or less than 6!!";
                    return RedirectToAction("Settings");
                }
                else
                {
                    userRepo.ChangePassById(Convert.ToInt32(form["Id"]), Convert.ToString(form["cpassword"]));
                    User u = userRepo.Get(Convert.ToInt32(form["Id"]));
                    Session["user"] = u;

                    TempData["msg"] = "Password changed successfully!!";
                    return RedirectToAction("Settings");
                }
            }
            else
            {
                TempData["msg"] = "Password doesn't matched!!";
                return RedirectToAction("Settings");
            }


        }

    }
}
