using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    [Authorize]
    public class MstTypeOfRekananController : ApiController
    {
        private IDataAccessRepository<mstTypeOfRekanan, int> _repository;
        //Inject the DataAccessRepository using Construction Injection 
        public MstTypeOfRekananController(IDataAccessRepository<mstTypeOfRekanan, int> r)
        {
            _repository = r;
        }
        public IEnumerable<mstTypeOfRekanan> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(mstTypeOfRekanan))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(mstTypeOfRekanan))]
        public IHttpActionResult Post(mstTypeOfRekanan myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, mstTypeOfRekanan myData)
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
