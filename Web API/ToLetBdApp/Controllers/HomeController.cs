using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToLetBdEntity;
using ToLetBdRepository;

namespace ToLetBdApp.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        UserRepository user = new UserRepository();
        PostRepository postRepo = new PostRepository();
        PostImageRepository imgRepo = new PostImageRepository();
        CommentRepository comRepo = new CommentRepository();

        [HttpGet]
        public ActionResult Index()
        {
            List<Post> posts = postRepo.GetPublishPost();
            ViewBag.allposts = posts;

            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            List<Post> posts = postRepo.GetPostByArea(form["Address"].ToString());
            ViewBag.allposts = posts;

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

                List<User> alluser = user.GetAll();
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
                TempData["msg"] = "Comment Added Successfully!!";
                return RedirectToAction("ViewPost");
            }
            else {
                return RedirectToAction("Index","Login");
            }

            

        }

    }
}
