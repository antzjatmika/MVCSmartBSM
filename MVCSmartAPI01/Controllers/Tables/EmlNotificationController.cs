using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;
using System.Text.RegularExpressions;

namespace APIService.Controllers
{
    [Authorize]
    public class EmlNotificationController : ApiController
    {
        private IDataAccessRepository<emlNotification, int> _repository;
        //Inject the DataAccessRepository using Construction Injection 
        public EmlNotificationController(IDataAccessRepository<emlNotification, int> r)
        {
            _repository = r;
        }
        public IEnumerable<emlNotification> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(emlNotification))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(emlNotification))]
        public IHttpActionResult Post(emlNotification myData)
        {
            if (string.IsNullOrEmpty(myData.ParameterKeys))
            {
                ModelState.AddModelError("ParameterKeys", "ParameterKeys is required");
            }
            if (!string.IsNullOrEmpty(myData.AddressEmailIdTo))
            {
                string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                Regex re = new Regex(emailRegex);
                if (!re.IsMatch(myData.AddressEmailIdTo))
                {
                    ModelState.AddModelError("AddressEmailIdTo", "AddressEmailIdTo is not valid");
                }
            }
            else
            {
                ModelState.AddModelError("AddressEmailIdTo", "AddressEmailIdTo is required");
            }
            if (ModelState.IsValid)
            {
                _repository.Post(myData);
            }
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, emlNotification myData)
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
