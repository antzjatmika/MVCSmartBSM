using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class TrxTenagaPendukungImpController : ApiController
    {
        private IDataAccessRepository<trxTenagaPendukungImp, int> _repository;
        private TrxTenagaPendukungImpRep _repTPendukung = new TrxTenagaPendukungImpRep();
        //Inject the DataAccessRepository using Construction Injection 
        public TrxTenagaPendukungImpController(IDataAccessRepository<trxTenagaPendukungImp, int> r, TrxTenagaPendukungImpRep repTPendukung)
        {
            _repository = r;
            _repTPendukung = repTPendukung;
        }
        public IEnumerable<trxTenagaPendukungImp> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxTenagaPendukungImp))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxTenagaPendukungImp))]
        public IHttpActionResult Post(trxTenagaPendukungImp myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxTenagaPendukungImp myData)
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
        [Route("api/TrxTenagaPendukungImp/GetByRekanan/{idRekanan}")]
        public IEnumerable<trxTenagaPendukungImp> GetByRekanan(System.Guid idRekanan)
        {
            IEnumerable<trxTenagaPendukungImp> TenagaPendukungByRekanan;
            TenagaPendukungByRekanan = _repTPendukung.GetByRekanan(idRekanan);
            return TenagaPendukungByRekanan;
        }
    }
}
