using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class MstWilayahController : ApiController
    {
        private IDataAccessRepository<mstWilayah, int> _repository;
        //Inject the DataAccessRepository using Construction Injection 
        public MstWilayahController(IDataAccessRepository<mstWilayah, int> r)
        {
            _repository = r;
        }
        public IEnumerable<mstWilayah> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(mstWilayah))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        //[ResponseType(typeof(mstWilayah))]
        //[Route("api/MstWilayah/ByMe/{id}")]
        //public IHttpActionResult GetByMe(int id)
        //{
        //    return Ok(_repository.Get(id));
        //}

        [ResponseType(typeof(mstWilayah))]
        public IHttpActionResult Post(mstWilayah myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, mstWilayah myData)
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
