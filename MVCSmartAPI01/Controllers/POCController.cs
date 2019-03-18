using System.Web.Http;

namespace MVCSmartAPI01.Controllers
{
    [Authorize]
    public class TestMethodController : ApiController
    {
        [HttpGet]
        [Route("api/TestMethod")]
        public string TestMethod()
        {
            return "Hello, C# Corner Member. ";
        }
        
    }
}
