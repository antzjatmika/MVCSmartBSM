using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class trxNotarisItemController : ApiController
    {
        private IDataAccessRepository<trxNotarisItem, int> _repository;
        private trxNotarisItemRep _repNotarisItem = new trxNotarisItemRep();
        //Inject the DataAccessRepository using Construction Injection 
        public trxNotarisItemController(IDataAccessRepository<trxNotarisItem, int> r, trxNotarisItemRep repOwner)
        {
            _repository = r;
            _repNotarisItem = repOwner;
        }
        public IEnumerable<trxNotarisItem> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxNotarisItem))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxNotarisItem))]
        public IHttpActionResult Post(trxNotarisItem myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxNotarisItem myData)
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
        [Route("api/trxNotarisItem/GetByRekanan/{idRekanan}")]
        public IEnumerable<trxNotarisItem> GetByRekanan(System.Guid idRekanan)
        {
            IEnumerable<trxNotarisItem> OwnershipByRekanan;
            OwnershipByRekanan = _repNotarisItem.GetByRekanan(idRekanan);
            return OwnershipByRekanan;
        }
    }
}
