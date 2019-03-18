using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class TrxDocumentMandatoryController : ApiController
    {
        private IDataAccessRepository<trxDocumentMandatory, int> _repository;
        //Inject the DataAccessRepository using Construction Injection 
        public TrxDocumentMandatoryController(IDataAccessRepository<trxDocumentMandatory, int> r)
        {
            _repository = r;
        }
        public IEnumerable<trxDocumentMandatory> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxDocumentMandatory))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxDocumentMandatory))]
        public IHttpActionResult Post(trxDocumentMandatory myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxDocumentMandatory myData)
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
