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
     [RoutePrefix("api/comments")]
    public class CommentController : ApiController
    {
         CommentRepository comRepo = new CommentRepository();

         [Route("", Name = "GetAllComments")]
         public IHttpActionResult Get()
         {
             return Ok(comRepo.GetAll());
         }

         [Route("", Name = "InsertComments")]
         [BasicAuthentication]
         public IHttpActionResult Post(Comment com)
         {
             comRepo.Insert(com);
             return Ok(com);
         }

    }
}
