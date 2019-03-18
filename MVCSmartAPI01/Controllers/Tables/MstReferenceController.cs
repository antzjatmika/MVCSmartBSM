using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    //[Authorize]
    public class MstReferenceController : ApiController
    {
        private IDataAccessRepository<mstReference, int> _repository;
        private MstReferenceRep _repReff;
        //Inject the DataAccessRepository using Construction Injection 
        public MstReferenceController(IDataAccessRepository<mstReference, int> r, MstReferenceRep repReff)
        {
            _repository = r;
            _repReff = repReff;
        }
        public IEnumerable<mstReference> Get()
        {
            return _repository.Get();
        }
        [ResponseType(typeof(mstReference))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(mstReference))]
        public IHttpActionResult Post(mstReference myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, mstReference myData)
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
        [Route("api/MstReference/GetByType/{refType}")]
        public IEnumerable<mstReference> GetByType(string refType)
        {
            return _repReff.GetByType(refType);
        }
    }
}
