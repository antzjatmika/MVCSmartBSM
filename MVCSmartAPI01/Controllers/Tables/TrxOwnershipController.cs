using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class TrxOwnershipController : ApiController
    {
        private IDataAccessRepository<trxOwnership, int> _repository;
        private TrxOwnershipRep _repOwner = new TrxOwnershipRep();
        //Inject the DataAccessRepository using Construction Injection 
        public TrxOwnershipController(IDataAccessRepository<trxOwnership, int> r, TrxOwnershipRep repOwner)
        {
            _repository = r;
            _repOwner = repOwner;
        }
        public IEnumerable<trxOwnership> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxOwnership))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxOwnership))]
        public IHttpActionResult Post(trxOwnership myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxOwnership myData)
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
        [Route("api/TrxOwnership/GetByRekanan/{idRekanan}")]
        public IEnumerable<trxOwnership> GetByRekanan(System.Guid idRekanan)
        {
            IEnumerable<trxOwnership> OwnershipByRekanan;
            OwnershipByRekanan = _repOwner.GetByRekanan(idRekanan);
            return OwnershipByRekanan;
        }
    }
}
