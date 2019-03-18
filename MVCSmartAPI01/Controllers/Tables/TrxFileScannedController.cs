using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class TrxFileScannedController : ApiController
    {
        private IDataAccessRepository<trxFileScanned, int> _repository;
        private TrxFileScannedRep _repAsisten = new TrxFileScannedRep();
        //Inject the DataAccessRepository using Construction Injection 
        public TrxFileScannedController(IDataAccessRepository<trxFileScanned, int> r, TrxFileScannedRep repAsisten)
        {
            _repository = r;
            _repAsisten = repAsisten;
        }
        public IEnumerable<trxFileScanned> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxFileScanned))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxAsisten))]
        public IHttpActionResult Post(trxFileScanned myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxFileScanned myData)
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
