using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToLetBdAPI.Attributes;
using ToLetBdEntity;
using ToLetBdRepository;

namespace ToLetBdAPI.Controllers
{
    [RoutePrefix("api/posts")]
    public class PostController : ApiController
    {
        PostRepository postRepo = new PostRepository();
        CommentRepository comRepo = new CommentRepository();
        PostImageRepository postImgRepo = new PostImageRepository();


        [Route("", Name = "GetAllPost")]
        public IHttpActionResult Get() {
            return Ok(postRepo.GetAll());
        }

        [Route("{id}", Name="GetPostById")]
        public IHttpActionResult Get(int id)
        {
            Post post = postRepo.Get(id);
            if (post != null)
            {
                return Ok(post);
            }
            else {
                return StatusCode(HttpStatusCode.NoContent);
            }
            
        }



        [Route("", Name = "InsertPost")]
        [BasicAuthentication]
        public IHttpActionResult Post(Post post)
        {
            postRepo.Insert(post);
            return Ok(post);

        }

        [Route("{id}", Name = "EditPostById")]
        [BasicAuthentication]
        public IHttpActionResult Put(int id, Post post)
        {
            post.Id = id;
            Post postToUpdate = postRepo.Get(id);
            if (postToUpdate != null)
            {
                postRepo.EditPost(post);
                return Ok(post);
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

        }

        [Route("{id}/disablePost", Name = "DisabledPost")][BasicAuthentication]
        public IHttpActionResult PutDisablePost(int id)
        {
            Post postToUpdate = postRepo.Get(id);
            if (postToUpdate != null)
            {
                postRepo.DisablePost(id);
                return StatusCode(HttpStatusCode.OK);
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

        }

        [Route("{id}/publishPost", Name = "PublishedPost")][BasicAuthentication]
        public IHttpActionResult Put(int id)
        {
            Post postToUpdate = postRepo.Get(id);
            if (postToUpdate != null)
            {
                postRepo.PublishPost(id);
                return StatusCode(HttpStatusCode.OK);
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

        }


        [Route("{id}", Name = "deletePostById")]
        [BasicAuthentication]
        public IHttpActionResult Delete(int id)
        {
            postRepo.Delete(id);

            return StatusCode(HttpStatusCode.NoContent);


        }

        [Route("byStatus/{status}", Name = "SearchPostByStatus")]
        public IHttpActionResult Post([FromUri]string status)
        {
            
            if (status == "Pending")
            {
                List<Post> postList = postRepo.GetPendingPost();
                return Ok(postList);
            }
            else if (status == "Active")
            {
                List<Post> postList = postRepo.GetPublishPost();
                return Ok(postList);
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

        }


        [Route("byArea/{area}", Name="SearchPostByArea")]
        public IHttpActionResult PostByArea([FromUri]string area)
        {
               List<Post> postList = postRepo.GetPostByArea(area);
               return Ok(postList);
        }

        [Route("{id}/comments", Name = "GetCommentsByPostId")]
        public IHttpActionResult GetCommentsByPostId([FromUri]int id)
        {
            List<Comment> comList = comRepo.GetByPostId(id);
            return Ok(comList);
        }

        [Route("{id}/postImages", Name = "GetPostImagesPostId")]
        public IHttpActionResult GetPostImagesPostId([FromUri]int id)
        {
            List<PostImage> postImgList = postImgRepo.GetAllImgByPostId(id);
            return Ok(postImgList);
        }

        [Route("totalPublishedPost", Name = "GetTotalPublishedPost")]
        [BasicAdminAuthentication]
        public IHttpActionResult GetTotalPublishedPost()
        {

            int c = postRepo.totalPublishedPost();
            return Ok(c);

        }


        [Route("totalPendingPost", Name = "GetTotalPendingPost")]
        [BasicAdminAuthentication]
        public IHttpActionResult GetTotalPendingPost()
        {

            int c = postRepo.totalPendingPost();
            return Ok(c);

        }
        
    }
}
