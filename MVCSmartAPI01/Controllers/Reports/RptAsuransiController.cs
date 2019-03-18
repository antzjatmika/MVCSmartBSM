using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class RptAsuransiController : ApiController
    {
        private IDataAccessRepository<rptAsuransi, int> _repository;
        private RptAsuransiRep _repAsuransi;
        //Inject the DataAccessRepository using Construction Injection 
        public RptAsuransiController(IDataAccessRepository<rptAsuransi, int> r, RptAsuransiRep repAsuransi)
        {
            _repository = r;
            _repAsuransi = repAsuransi;
        }
        public IEnumerable<rptAsuransi> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(rptAsuransi))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(rptAsuransi))]
        public IHttpActionResult Post(rptAsuransi myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, rptAsuransi myData)
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
        [Route("api/RptAsuransi/GetByRekanan/{idRekanan}")]
        public IEnumerable<rptAsuransi> GetByRekanan(System.Guid idRekanan)
        {
            IEnumerable<rptAsuransi> AsuransiByRekanan;
            AsuransiByRekanan = _repAsuransi.GetByRekanan(idRekanan);
            return AsuransiByRekanan;
        }
    }
}
