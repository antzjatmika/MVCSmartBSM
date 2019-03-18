using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class TrxTenagaPendukungController : ApiController
    {
        private IDataAccessRepository<trxTenagaPendukung, int> _repository;
        private TrxTenagaPendukungRep _repPendukung = new TrxTenagaPendukungRep();
        //Inject the DataAccessRepository using Construction Injection 
        public TrxTenagaPendukungController(IDataAccessRepository<trxTenagaPendukung, int> r, TrxTenagaPendukungRep repPendukung)
        {
            _repository = r;
            _repPendukung = repPendukung;
        }
        public IEnumerable<trxTenagaPendukung> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxTenagaPendukung))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxTenagaPendukung))]
        public IHttpActionResult Post(trxTenagaPendukung myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxTenagaPendukung myData)
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
        [Route("api/TrxTenagaPendukung/GetByRekanan/{idRekanan}")]
        public IEnumerable<trxTenagaPendukung> GetByRekanan(System.Guid idRekanan)
        {
            IEnumerable<trxTenagaPendukung> PendukungRekanan;
            PendukungRekanan = _repPendukung.GetByRekanan(idRekanan);
            return PendukungRekanan;
        }
    }
}
