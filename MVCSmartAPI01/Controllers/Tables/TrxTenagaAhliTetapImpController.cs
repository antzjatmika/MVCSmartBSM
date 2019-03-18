using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class TrxTenagaAhliTetapImpController : ApiController
    {
        private IDataAccessRepository<trxTenagaAhliTetapImp, int> _repository;
        private TrxTenagaAhliTetapImpRep _repTAhli = new TrxTenagaAhliTetapImpRep();
        //Inject the DataAccessRepository using Construction Injection 
        public TrxTenagaAhliTetapImpController(IDataAccessRepository<trxTenagaAhliTetapImp, int> r, TrxTenagaAhliTetapImpRep repTAhli)
        {
            _repository = r;
            _repTAhli = repTAhli;
        }
        public IEnumerable<trxTenagaAhliTetapImp> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxTenagaAhliTetapImp))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxTenagaAhliTetapImp))]
        public IHttpActionResult Post(trxTenagaAhliTetapImp myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxTenagaAhliTetapImp myData)
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
        [Route("api/TrxTenagaAhliTetapImp/GetByRekanan/{idRekanan}")]
        public IEnumerable<trxTenagaAhliTetapImp> GetByRekanan(System.Guid idRekanan)
        {
            IEnumerable<trxTenagaAhliTetapImp> TenagaAhliByRekanan;
            TenagaAhliByRekanan = _repTAhli.GetByRekanan(idRekanan);
            return TenagaAhliByRekanan;
        }
        [Route("api/TrxTenagaAhliTetapImp/GetByPointer/{imagePointer}")]
        public IEnumerable<trxTenagaAhliTetapImp> GetByPointer(System.Guid imagePointer)
        {
            IEnumerable<trxTenagaAhliTetapImp> TenagaAhliByRekanan;
            TenagaAhliByRekanan = _repTAhli.GetByPointer(imagePointer);
            return TenagaAhliByRekanan;
        }
    }
}
