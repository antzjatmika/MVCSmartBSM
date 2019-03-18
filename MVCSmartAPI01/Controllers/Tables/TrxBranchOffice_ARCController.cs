using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class TrxBranchOffice_ARCController : ApiController
    {
        private IDataAccessRepository<trxBranchOffice_ARC, int> _repository;
        //Inject the DataAccessRepository using Construction Injection 
        public TrxBranchOffice_ARCController(IDataAccessRepository<trxBranchOffice_ARC, int> r)
        {
            _repository = r;
        }
        public IEnumerable<trxBranchOffice_ARC> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxBranchOffice_ARC))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxBranchOffice_ARC))]
        public IHttpActionResult Post(trxBranchOffice_ARC myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxBranchOffice_ARC myData)
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
