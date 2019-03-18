using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class TrxFileDataPekerjaanController : ApiController
    {
        private IDataAccessRepository<trxFileDataPekerjaan, int> _repository;
        //Inject the DataAccessRepository using Construction Injection 
        public TrxFileDataPekerjaanController(IDataAccessRepository<trxFileDataPekerjaan, int> r)
        {
            _repository = r;
        }
        public IEnumerable<trxFileDataPekerjaan> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxFileDataPekerjaan))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxFileDataPekerjaan))]
        public IHttpActionResult Post(trxFileDataPekerjaan myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxFileDataPekerjaan myData)
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
