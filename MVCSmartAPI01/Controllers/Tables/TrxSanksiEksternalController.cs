using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class TrxSanksiEksternalController : ApiController
    {
        private IDataAccessRepository<trxSanksiEksternal, int> _repository;
        //Inject the DataAccessRepository using Construction Injection 
        public TrxSanksiEksternalController(IDataAccessRepository<trxSanksiEksternal, int> r)
        {
            _repository = r;
        }
        public IEnumerable<trxSanksiEksternal> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxSanksiEksternal))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxSanksiEksternal))]
        public IHttpActionResult Post(trxSanksiEksternal myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxSanksiEksternal myData)
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
