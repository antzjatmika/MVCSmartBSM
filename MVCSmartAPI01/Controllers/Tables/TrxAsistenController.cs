using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class TrxAsistenController : ApiController
    {
        private IDataAccessRepository<trxAsisten, int> _repository;
        private TrxAsistenRep _repAsisten = new TrxAsistenRep();
        //Inject the DataAccessRepository using Construction Injection 
        public TrxAsistenController(IDataAccessRepository<trxAsisten, int> r, TrxAsistenRep repAsisten)
        {
            _repository = r;
            _repAsisten = repAsisten;
        }
        public IEnumerable<trxAsisten> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxAsisten))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxAsisten))]
        public IHttpActionResult Post(trxAsisten myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxAsisten myData)
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
        [Route("api/TrxAsisten/GetByRekanan/{idRekanan}")]
        public IEnumerable<trxAsisten> GetByRekanan(System.Guid idRekanan)
        {
            IEnumerable<trxAsisten> ContactsRekanan;
            ContactsRekanan = _repAsisten.GetByRekanan(idRekanan);
            return ContactsRekanan;
        }
    }
}
