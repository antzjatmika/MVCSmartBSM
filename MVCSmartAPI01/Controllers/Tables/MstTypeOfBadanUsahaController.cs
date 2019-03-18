using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

using System.Security.Claims;
using System.Web;
using System.Linq;

namespace APIService.Controllers
{
    public class MstTypeOfBadanUsahaController : ApiController
    {

        private ClaimsPrincipal TryGetOwinUser()
        {
            if (HttpContext.Current == null)
                return null;

            var context = HttpContext.Current.GetOwinContext();
            if (context == null)
                return null;

            if (context.Authentication == null || context.Authentication.User == null)
                return null;

            return context.Authentication.User;
        }

        private Claim TryGetClaim(ClaimsPrincipal owinUser, string key)
        {
            if (owinUser == null)
                return null;

            if (owinUser.Claims == null)
                return null;

            return owinUser.Claims.FirstOrDefault(o => o.Type.Equals(key));
        }



        private IDataAccessRepository<mstTypeOfBadanUsaha, int> _repository;
        //Inject the DataAccessRepository using Construction Injection 
        public MstTypeOfBadanUsahaController(IDataAccessRepository<mstTypeOfBadanUsaha, int> r)
        {
            _repository = r;
        }
        public IEnumerable<mstTypeOfBadanUsaha> Get()
        {
            //var owinUser = TryGetOwinUser();
            //var claim = TryGetClaim(owinUser, "email");
            //string email = claim.Value;

            return _repository.Get();
        }

        [ResponseType(typeof(mstTypeOfBadanUsaha))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(mstTypeOfBadanUsaha))]
        public IHttpActionResult Post(mstTypeOfBadanUsaha myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, mstTypeOfBadanUsaha myData)
        {
            _repository.Put(id, myData);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(int id)
        {
            _repository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
