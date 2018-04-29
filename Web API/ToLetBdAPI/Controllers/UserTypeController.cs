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
    [RoutePrefix("api/userTypes")]
    public class UserTypeController : ApiController
    {
        UserTypeRepository userTypeRepo = new UserTypeRepository();
        UserRepository userRepo = new UserRepository();

        [Route("", Name = "GetAllUserTYpes")]
        public IHttpActionResult Get()
        {
            return Ok(userTypeRepo.GetAll());
        }



        [Route("{id}/users", Name = "GetAllUserByTypeId")]
        [BasicAdminAuthentication]
        public IHttpActionResult Get(int id)
        {
            List<User> userList = userRepo.GetByUserTypeId(id);
            if (userList != null)
            {
                return Ok(userList);
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
           
        }



    }
}
