using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class TrxTenagaAhliController : ApiController
    {
        private IDataAccessRepository<trxTenagaAhli, int> _repository;
        private TrxTenagaAhliRep _repTAhli = new TrxTenagaAhliRep();
        //Inject the DataAccessRepository using Construction Injection 
        public TrxTenagaAhliController(IDataAccessRepository<trxTenagaAhli, int> r, TrxTenagaAhliRep repTAhli)
        {
            _repository = r;
            _repTAhli = repTAhli;
        }
        public IEnumerable<trxTenagaAhli> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxTenagaAhli))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxTenagaAhli))]
        public IHttpActionResult Post(trxTenagaAhli myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxTenagaAhli myData)
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
        [Route("api/TrxTenagaAhli/GetByRekanan/{idRekanan}")]
        public IEnumerable<trxTenagaAhli> GetByRekanan(System.Guid idRekanan)
        {
            IEnumerable<trxTenagaAhli> TenagaAhliByRekanan;
            TenagaAhliByRekanan = _repTAhli.GetByRekanan(idRekanan);
            return TenagaAhliByRekanan;
        }
        [Route("api/TrxTenagaAhli/GetByGuidHeader/{idHeader}")]
        public IEnumerable<trxTenagaAhli> GetByGuidHeader(Guid guidHeader)
        {
            IEnumerable<trxTenagaAhli> TenagaAhliByIdHeader;
            TenagaAhliByIdHeader = _repTAhli.GetByGuidHeader(guidHeader);
            return TenagaAhliByIdHeader;
        }
    }
}
