using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class TrxTenagaAhliTidakTetapImpController : ApiController
    {
        private IDataAccessRepository<trxTenagaAhliTidakTetapImp, int> _repository;
        private TrxTenagaAhliTidakTetapImpRep _repTAhli = new TrxTenagaAhliTidakTetapImpRep();
        //Inject the DataAccessRepository using Construction Injection 
        public TrxTenagaAhliTidakTetapImpController(IDataAccessRepository<trxTenagaAhliTidakTetapImp, int> r, TrxTenagaAhliTidakTetapImpRep repTAhli)
        {
            _repository = r;
            _repTAhli = repTAhli;
        }
        public IEnumerable<trxTenagaAhliTidakTetapImp> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxTenagaAhliTidakTetapImp))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxTenagaAhliTidakTetapImp))]
        public IHttpActionResult Post(trxTenagaAhliTidakTetapImp myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxTenagaAhliTidakTetapImp myData)
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
        [Route("api/TrxTenagaAhliTidakTetapImp/GetByRekanan/{idRekanan}")]
        public IEnumerable<trxTenagaAhliTidakTetapImp> GetByRekanan(System.Guid idRekanan)
        {
            IEnumerable<trxTenagaAhliTidakTetapImp> TenagaAhliByRekanan;
            TenagaAhliByRekanan = _repTAhli.GetByRekanan(idRekanan);
            return TenagaAhliByRekanan;
        }
    }
}
