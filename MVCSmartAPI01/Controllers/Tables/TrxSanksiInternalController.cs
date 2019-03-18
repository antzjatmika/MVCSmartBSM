using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class TrxSanksiInternalController : ApiController
    {
        private IDataAccessRepository<trxSanksiInternal, int> _repository;
        //Inject the DataAccessRepository using Construction Injection 
        public TrxSanksiInternalController(IDataAccessRepository<trxSanksiInternal, int> r)
        {
            _repository = r;
        }
        public IEnumerable<trxSanksiInternal> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxSanksiInternal))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxSanksiInternal))]
        public IHttpActionResult Post(trxSanksiInternal myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxSanksiInternal myData)
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
