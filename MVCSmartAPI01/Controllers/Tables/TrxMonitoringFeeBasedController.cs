using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class TrxMonitoringFeeBasedController : ApiController
    {
        private IDataAccessRepository<trxMonitoringFeeBased, int> _repository;
        //Inject the DataAccessRepository using Construction Injection 
        public TrxMonitoringFeeBasedController(IDataAccessRepository<trxMonitoringFeeBased, int> r)
        {
            _repository = r;
        }
        public IEnumerable<trxMonitoringFeeBased> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxMonitoringFeeBased))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxMonitoringFeeBased))]
        public IHttpActionResult Post(trxMonitoringFeeBased myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxMonitoringFeeBased myData)
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
