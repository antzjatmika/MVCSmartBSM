using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class TrxDetailPekerjaan_ARCController : ApiController
    {
        private IDataAccessRepository<trxDetailPekerjaan_ARC, int> _repository;
        //Inject the DataAccessRepository using Construction Injection 
        public TrxDetailPekerjaan_ARCController(IDataAccessRepository<trxDetailPekerjaan_ARC, int> r)
        {
            _repository = r;
        }
        public IEnumerable<trxDetailPekerjaan_ARC> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxDetailPekerjaan_ARC))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxDetailPekerjaan_ARC))]
        public IHttpActionResult Post(trxDetailPekerjaan_ARC myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxDetailPekerjaan_ARC myData)
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
