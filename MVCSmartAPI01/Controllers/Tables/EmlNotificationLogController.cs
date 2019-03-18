using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class EmlNotificationLogController : ApiController
    {
        private IDataAccessRepository<emlNotificationLog, int> _repository;
        //Inject the DataAccessRepository using Construction Injection 
        public EmlNotificationLogController(IDataAccessRepository<emlNotificationLog, int> r)
        {
            _repository = r;
        }
        public IEnumerable<emlNotificationLog> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(emlNotificationLog))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(emlNotificationLog))]
        public IHttpActionResult Post(emlNotificationLog myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, emlNotificationLog myData)
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
