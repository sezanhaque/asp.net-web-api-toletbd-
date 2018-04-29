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
    [RoutePrefix("api/postImages")]
    public class PostImageController : ApiController
    {
        PostImageRepository postImgRepo = new PostImageRepository();

        [Route("", Name = "GetAllImages")]
        public IHttpActionResult Get()
        {
            return Ok(postImgRepo.GetAll());
        }

        [Route("", Name = "InsertImages")]
        [BasicAuthentication]
        public IHttpActionResult Post(PostImage postImg)
        {
            postImgRepo.Insert(postImg);
            return Ok(postImg);
        }
       
    }
}
