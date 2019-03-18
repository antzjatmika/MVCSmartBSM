using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(OwinAuthentication.Startup))]
namespace OwinAuthentication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureOAuth(app);
        }

        //private void ConfigureOAuth(IAppBuilder app)
        //{
        //}
    }
}