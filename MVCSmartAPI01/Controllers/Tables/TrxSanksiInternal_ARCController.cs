using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class TrxSanksiInternal_ARCController : ApiController
    {
        private IDataAccessRepository<trxSanksiInternal_ARC, int> _repository;
        //Inject the DataAccessRepository using Construction Injection 
        public TrxSanksiInternal_ARCController(IDataAccessRepository<trxSanksiInternal_ARC, int> r)
        {
            _repository = r;
        }
        public IEnumerable<trxSanksiInternal_ARC> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxSanksiInternal_ARC))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxSanksiInternal_ARC))]
        public IHttpActionResult Post(trxSanksiInternal_ARC myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxSanksiInternal_ARC myData)
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
