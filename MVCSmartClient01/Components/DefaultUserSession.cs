using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSmartClient01.Components
{
    public class DefaultUserSession : IUserSession
    {
        public string Username
        {
            get { 

                return ((System.Security.Claims.ClaimsPrincipal)HttpContext.Current.User).FindFirst(System.Security.Claims.ClaimTypes.Name).Value; 
            }
        }   

        public string BearerToken
        {
            get { return ((System.Security.Claims.ClaimsPrincipal)HttpContext.Current.User).FindFirst("AcessToken").Value; }
        }
    }
}