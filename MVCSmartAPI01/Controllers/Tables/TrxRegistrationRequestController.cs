using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class TrxRegistrationRequestController : ApiController
    {
        private IDataAccessRepository<trxRegistrationRequest, int> _repository;
        //Inject the DataAccessRepository using Construction Injection 
        public TrxRegistrationRequestController(IDataAccessRepository<trxRegistrationRequest, int> r)
        {
            _repository = r;
        }
        public IEnumerable<trxRegistrationRequest> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxRegistrationRequest))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxRegistrationRequest))]
        public IHttpActionResult Post(trxRegistrationRequest myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxRegistrationRequest myData)
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
