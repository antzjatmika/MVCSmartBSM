namespace MVCSmartClient01.Attributes
{
    using System.Web.Mvc;
    using ApiHelper;
    using ApiInfrastructure;

    public class AuthenticationAttribute : ActionFilterAttribute
    {
        private readonly ITokenContainer tokenContainer;

        public AuthenticationAttribute()
        {
            tokenContainer = new TokenContainer();
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (tokenContainer.ApiToken == null)
            {
                bool bolTest = filterContext.HttpContext.User.Identity.IsAuthenticated;
                filterContext.HttpContext.Response.RedirectToRoute(RouteConfig.LoginRouteName);
            }
        }
    }
}