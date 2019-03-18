using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class MstRegionController : ApiController
    {
        private IDataAccessRepository<mstRegionAdmin, Guid> _repository;
        private MstRegionAdminRep _repRegion = new MstRegionAdminRep();
        //Inject the DataAccessRepository using Construction Injection 
        public MstRegionController(IDataAccessRepository<mstRegionAdmin, Guid> r, MstRegionAdminRep repRegion)
        {
            _repository = r;
            _repRegion = repRegion;
        }
        public IEnumerable<mstRegionAdmin> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(mstRegionAdmin))]
        public IHttpActionResult Get(Guid id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(mstRegionAdmin))]
        public IHttpActionResult Post(mstRegionAdmin myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(Guid id, mstRegionAdmin myData)
        {
            _repository.Put(id, myData);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(Guid id)
        {
            _repository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
