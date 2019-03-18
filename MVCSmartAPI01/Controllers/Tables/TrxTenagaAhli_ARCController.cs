using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class TrxTenagaAhli_ARCController : ApiController
    {
        private IDataAccessRepository<trxTenagaAhli_ARC, int> _repository;
        //Inject the DataAccessRepository using Construction Injection 
        public TrxTenagaAhli_ARCController(IDataAccessRepository<trxTenagaAhli_ARC, int> r)
        {
            _repository = r;
        }
        public IEnumerable<trxTenagaAhli_ARC> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxTenagaAhli_ARC))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxTenagaAhli_ARC))]
        public IHttpActionResult Post(trxTenagaAhli_ARC myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxTenagaAhli_ARC myData)
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
