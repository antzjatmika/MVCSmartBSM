using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

using System.Security.Claims;
using System.Web;
using System.Linq;

namespace APIService.Controllers
{
    public class MstSegmentasiController : ApiController
    {
        private IDataAccessRepository<mstSegmentasi, int> _repository;
        public MstSegmentasiController(IDataAccessRepository<mstSegmentasi, int> r)
        {
            _repository = r;
        }
        public IEnumerable<mstSegmentasi> Get()
        {
            return _repository.Get();
        }
        [ResponseType(typeof(mstSegmentasi))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(mstSegmentasi))]
        public IHttpActionResult Post(mstSegmentasi myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, mstSegmentasi myData)
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
