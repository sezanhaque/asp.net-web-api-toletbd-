using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToLetBdEntity;
using ToLetBdInterface;
using ToLetBdRepository;

namespace ToLetBdApp.Controllers
{
   
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
       // IUserRepository userRepo = new UserRepository();

        UserRepository userRepo = new UserRepository();
        UserTypeRepository userTypeRepo = new UserTypeRepository();
        PostRepository postRepo = new PostRepository();
        PostImageRepository imgRepo = new PostImageRepository();
        CommentRepository comRepo = new CommentRepository();

        public ActionResult Index()
        {

            User admin = (User)Session["user"];
            ViewBag.admin = admin;
            ViewBag.TotalHouseOwner = userRepo.totalHouseOwner();
            ViewBag.totalRenter = userRepo.totalRenter();
            ViewBag.totalPendingPost = postRepo.totalPendingPost();
            ViewBag.totalPublishedPost = postRepo.totalPublishedPost();
            return View();

            
        }

        public ActionResult Logout()
        {

            Session.Clear();
            return RedirectToAction("Index","Home");
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
            else {

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
                if (String.IsNullOrEmpty(form["cpassword"]) || form["cpassword"].Length<6)
                {
                    TempData["msg"] = "Password can't be empty or less than 6!!";
                    return RedirectToAction("Settings");
                }
                else {
                    userRepo.ChangePassById(Convert.ToInt32(form["Id"]), Convert.ToString(form["cpassword"]));
                    User u = userRepo.Get(Convert.ToInt32(form["Id"]));
                    Session["user"] = u;

                    TempData["msg"] = "Password changed successfully!!";
                    return RedirectToAction("Settings");
                }
            }
            else {
                TempData["msg"] = "Password doesn't matched!!";
                return RedirectToAction("Settings");
            }
           
            
        }

        public ActionResult Users()
        {

            List<User> users = userRepo.GetAll();
            List<UserType> userTypes = userTypeRepo.GetAll();
            ViewBag.allusers = users;
            ViewBag.alluserTypes = userTypes;
            return View();
        }

        public ActionResult DeleteUsers(int id)
        {

            userRepo.Delete(id);
            TempData["msg"] = "User deleted Successfully!!";
            return RedirectToAction("Users");
        }

        [HttpPost]
        public ActionResult SearchUsers(FormCollection form)
        {

            if (Convert.ToInt32(form["userTypeId"]) != 0)
            {
                List<User> users = userRepo.GetByUserTypeId(Convert.ToInt32(form["userTypeId"]));
                List<UserType> userTypes = userTypeRepo.GetAll();
                ViewBag.allusers = users;
                ViewBag.alluserTypes = userTypes;
                return View("Users");
            }
            else {
                return RedirectToAction("Users");
            }
           
        }

        public ActionResult CurrentPost() {
            List<Post> posts = postRepo.GetPublishPost();
            ViewBag.allposts = posts;

            return View();
        }

        public ActionResult DeletePost(int id)
        {
            postRepo.Delete(id);

            TempData["msg"] = "Post Deleted!!";
            return RedirectToAction("CurrentPost");
        }

        public ActionResult DesablePost(int id)
        {
            Post p = postRepo.Get(id);
            if (p.Status != "Pending")
            {

                postRepo.DesablePost(id);
                TempData["msg"] = "Post Desabled!!";
                return RedirectToAction("CurrentPost");
            }
            else
            {
                TempData["msg"] = "Post Already Desabled!!";
                return RedirectToAction("CurrentPost");
            }

        }

        public ActionResult NewPost()
        {
            List<Post> posts = postRepo.GetPendingPost();
            ViewBag.allposts = posts;

            return View();
        }

        public ActionResult PublishPost(int id)
        {
            Post p = postRepo.Get(id);
            if (p.Status == "Pending")
            {

                postRepo.PublishPost(id);
                TempData["msg"] = "Post Published!!";
                return RedirectToAction("NewPost");
            }
            else
            {
                TempData["msg"] = "Post Already Published!!";
                return RedirectToAction("NewPost");
            }

        }

        [HttpGet]
        public ActionResult ViewPost(int id)
        {

            Post Post = postRepo.Get(id);
            ViewBag.post = Post;

            List<PostImage> postimg = imgRepo.GetAllImgByPostId(id);
            ViewBag.postImages = postimg;

            List<Comment> comments = comRepo.GetByPostId(id);
            ViewBag.com = comments;
            Session["comments"] = comments;

            List<User> alluser = userRepo.GetAll();
            ViewBag.alluser = alluser;

            return View();

        }

        [HttpPost]
        public ActionResult ViewPost(Comment comment)
        {

            if (Session["user"] != null)
            {

                var us = (User)Session["user"];
                comment.UserId = us.Id;
                comment.PostId = comment.PostId;
                comment.CommentDateTime = DateTime.Now;

                comRepo.Insert(comment);
                TempData["msg"] = "Comment added Successfully!!";
                return RedirectToAction("ViewPost");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }



        }

    }
}
