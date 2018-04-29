using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ToLetBdAPI.Attributes;
using ToLetBdEntity;
using ToLetBdRepository;

namespace ToLetBdAPI.Controllers
{
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        UserType userTypeRepo = new UserType();
        UserRepository userRepo = new UserRepository();
        PostRepository postRepo = new PostRepository();
        CommentRepository comRepo = new CommentRepository();

        [Route("", Name = "GetAllUsers")]
        [BasicAdminAuthentication]
        public IHttpActionResult Get()
        {
            return Ok(userRepo.GetAll());
        }


        [Route("", Name = "InsertUser")]
        public IHttpActionResult Post(User user)
        {
            userRepo.Insert(user);
            return Ok(user);
        }

        [Route("{id}", Name = "UpdateUser")]
        [BasicAuthentication]
        public IHttpActionResult Put([FromUri]int id,[FromBody]User user)
        {
          
            user.Id = id;
            User userToUpdate = userRepo.Get(id);
            if (userToUpdate != null)
            {
                userRepo.UpdateWithImg(user);
                return Ok(user);
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            
        }


        [Route("{id}", Name = "GetUserById")][BasicAdminAuthentication]
        public IHttpActionResult Get(int id)
        {
            User user = userRepo.Get(id);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

        }


        [Route("{id}", Name = "DeleteUserById")]
        [BasicAdminAuthentication]
        public IHttpActionResult Delete(int id)
        {
            userRepo.Delete(id);
            
           return StatusCode(HttpStatusCode.NoContent);
          

        }



        [Route("{id}/posts", Name = "GetPostUserById")]
        [BasicAuthentication]
        public IHttpActionResult GetPostByUserId(int id)
        {
            List<Post> postList = postRepo.GetByUserId(id);
          
            return Ok(postList);
          
        }

        [Route("{id}/posts/totalPendingPost", Name = "GetTotalPendingPostPostUserById")]
        [BasicAuthentication]
        public IHttpActionResult GetTotalPendingPostPostUserById(int id)
        {
           
            int c = postRepo.totalPendingPostByUserId(id);
            return Ok(c);

        }


        [Route("{id}/posts/totalPublishedPost", Name = "GetTotalPublishedPostUserById")]
        [BasicAuthentication]
        public IHttpActionResult GetTotalPublishedPostUserById(int id)
        {

            int c = postRepo.totalPublishedPostByUserId(id);
            return Ok(c);

        }

        [Route("totalHouseOwner", Name = "GetTotalHouseOwner")][BasicAdminAuthentication]
        public IHttpActionResult GetTotalHouseOwner()
        {

            int c = userRepo.totalHouseOwner();
            return Ok(c);

        }


        [Route("login", Name = "userLogin")]
        public IHttpActionResult PutUserLogin(User user)
        {

            User validUser = userRepo.GetByEmailAndPass(user);
            if (validUser != null)
            {
                return Ok(validUser);
            }
            else
            {

                return StatusCode(HttpStatusCode.NotAcceptable);
            }

        }

        [Route("logout", Name = "userLogout")][BasicAuthentication]
        public IHttpActionResult GetUserLogOut(User user)
        {

            return Ok();

        }

    }
}
