using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class TrxDataOrganisasi_ARCController : ApiController
    {
        private IDataAccessRepository<trxDataOrganisasi_ARC, int> _repository;
        //Inject the DataAccessRepository using Construction Injection 
        public TrxDataOrganisasi_ARCController(IDataAccessRepository<trxDataOrganisasi_ARC, int> r)
        {
            _repository = r;
        }
        public IEnumerable<trxDataOrganisasi_ARC> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxDataOrganisasi_ARC))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxDataOrganisasi_ARC))]
        public IHttpActionResult Post(trxDataOrganisasi_ARC myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxDataOrganisasi_ARC myData)
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
