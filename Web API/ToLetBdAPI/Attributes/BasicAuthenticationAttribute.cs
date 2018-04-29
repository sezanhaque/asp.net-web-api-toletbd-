using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading;
using System.Security.Principal;
using ToLetBdEntity;
using ToLetBdRepository;

namespace ToLetBdAPI.Attributes
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        UserRepository userRepo = new UserRepository();
        
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                string encodedString = actionContext.Request.Headers.Authorization.Parameter;
                string decodedString = Encoding.UTF8.GetString(Convert.FromBase64String(encodedString));
                string[] arr = decodedString.Split(new char[] { ':' });
                string email = arr[0];
                string password = arr[1];
                User us = new User();
                us.Email = email;
                us.Password = password;
                User u = userRepo.GetByEmailAndPass(us);

                if (u!=null)
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(u.Email), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }

        }
    }
}