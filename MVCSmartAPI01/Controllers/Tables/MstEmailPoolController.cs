using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class mstEmailPoolController : ApiController
    {
        private IDataAccessRepository<mstEmailPool, int> _repository;
        private mstEmailPoolRep _repPool = new mstEmailPoolRep();
        //Inject the DataAccessRepository using Construction Injection 
        public mstEmailPoolController(IDataAccessRepository<mstEmailPool, int> r, mstEmailPoolRep repPool)
        {
            _repository = r;
            _repPool = repPool;
        }
        public IEnumerable<mstEmailPool> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(mstEmailPool))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(mstEmailPool))]
        public IHttpActionResult Post(mstEmailPool myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, mstEmailPool myData)
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
        [Route("api/mstEmailPool/GetByRekanan/{idRekanan}")]
        public IEnumerable<mstEmailPool> GetByRekanan(System.Guid idRekanan)
        {
            IEnumerable<mstEmailPool> ContactsRekanan;
            ContactsRekanan = _repPool.GetByRekanan(idRekanan);
            return ContactsRekanan;
        }
    }
}
