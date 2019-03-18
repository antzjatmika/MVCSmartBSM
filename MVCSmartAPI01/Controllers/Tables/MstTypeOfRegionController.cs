using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class MstTypeOfRegionController : ApiController
    {
        private IDataAccessRepository<mstTypeOfRegion, int> _repository;
        //Inject the DataAccessRepository using Construction Injection 
        public MstTypeOfRegionController(IDataAccessRepository<mstTypeOfRegion, int> r)
        {
            _repository = r;
        }
        public IEnumerable<mstTypeOfRegion> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(mstTypeOfRegion))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(mstTypeOfRegion))]
        public IHttpActionResult Post(mstTypeOfRegion myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, mstTypeOfRegion myData)
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
