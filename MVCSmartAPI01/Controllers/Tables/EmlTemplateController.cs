using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class EmlTemplateController : ApiController
    {
        private IDataAccessRepository<emlTemplate, int> _repository;
        //Inject the DataAccessRepository using Construction Injection 
        public EmlTemplateController(IDataAccessRepository<emlTemplate, int> r)
        {
            _repository = r;
        }
        public IEnumerable<emlTemplate> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(emlTemplate))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(emlTemplate))]
        public IHttpActionResult Post(emlTemplate myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, emlTemplate myData)
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
