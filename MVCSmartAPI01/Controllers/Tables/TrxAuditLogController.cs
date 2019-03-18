using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class TrxAuditLogController : ApiController
    {
        private IDataAccessRepository<trxAuditLog, int> _repository;
        //Inject the DataAccessRepository using Construction Injection 
        public TrxAuditLogController(IDataAccessRepository<trxAuditLog, int> r)
        {
            _repository = r;
        }
        public IEnumerable<trxAuditLog> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxAuditLog))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxAuditLog))]
        public IHttpActionResult Post(trxAuditLog myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxAuditLog myData)
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
