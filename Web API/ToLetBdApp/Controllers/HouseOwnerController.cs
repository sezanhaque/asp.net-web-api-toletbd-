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
    public class HouseOwnerController : Controller
    {
        //
        // GET: /HouseOwner/

        UserRepository userRepo = new UserRepository();
        UserTypeRepository userTypeRepo = new UserTypeRepository();
        PostRepository postRepo = new PostRepository();
        PostImageRepository imgRepo = new PostImageRepository();
        CommentRepository comRepo = new CommentRepository();

        public ActionResult Index()
        {
            var user = (User)Session["user"];
            ViewBag.totalPendingPost = postRepo.totalPendingPostByUserId(user.Id);
            ViewBag.totalPublishedPost = postRepo.totalPublishedPostByUserId(user.Id);
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

        [HttpGet]
        public ActionResult NewPost() {

            return View();
        }

        [HttpPost]
        public ActionResult NewPost(Post post, Picture picture)
        {
            var user = (User)Session["user"];
            PostRepository postRepo = new PostRepository();

            if (ModelState.IsValid) {

                if (picture.files.Count(file => file != null) > 0)
                {
                    post.PostDateTime = DateTime.Now;
                    post.UserId = user.Id;
                    post.Status = "Pending";
                    post.Views = 0;
                    postRepo.Insert(post);

                    foreach (var file in picture.files)
                    {
                        if (file.ContentLength > 0)
                        {

                            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                            string extension = Path.GetExtension(file.FileName);
                            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                            var path = Path.Combine(Server.MapPath("~/Content/PostImages"), fileName);
                            file.SaveAs(path);
                            PostImageRepository postImgRepo = new PostImageRepository();
                            PostImage pimg = new PostImage();
                            pimg.ImgPath = fileName;
                            pimg.PostId = post.Id;
                            postImgRepo.Insert(pimg);



                        }
                        else
                        {

                            TempData["msg"] = "Invalid Image!!";
                            return RedirectToAction("NewPost");

                        }
                    }
                    TempData["msg"] = "New Post Added!!";
                    return RedirectToAction("NewPost");
                }
                else {
                    TempData["msg"] = "Invalid Image!!";
                    return RedirectToAction("NewPost");
                }
                        
            }
            else
            {
                ModelState.Values.SelectMany(v => v.Errors).ElementAt(0);

                return View();
            }

           
          

           
        }

        public ActionResult CurrentPost()
        {
            User us = (User)Session["user"];

            List<Post> posts = postRepo.GetByUserId(us.Id);
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
            else {
                TempData["msg"] = "Post Already Desabled!!";
                return RedirectToAction("CurrentPost");
            }
            
        }

        [HttpGet]
        public ActionResult EditPost(int id)
        {
            Post p = postRepo.Get(id);

            ViewBag.post = p;
            return View();
        }


        [HttpPost]
        public ActionResult EditPost(Post post)
        {
            postRepo.EditPost(post);
            Post p = postRepo.Get(post.Id);
            ViewBag.post = p;

            TempData["msg"] = "Post Updated!!";
            return View();
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
