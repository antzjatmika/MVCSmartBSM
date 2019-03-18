using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class TrxTenagaAhliTMPController : ApiController
    {
        private IDataAccessRepository<trxTenagaAhliTMP, int> _repository;
        private TrxTenagaAhliTMPRep _repTAhli = new TrxTenagaAhliTMPRep();
        //Inject the DataAccessRepository using Construction Injection 
        public TrxTenagaAhliTMPController(IDataAccessRepository<trxTenagaAhliTMP, int> r, TrxTenagaAhliTMPRep repTAhli)
        {
            _repository = r;
            _repTAhli = repTAhli;
        }
        public IEnumerable<trxTenagaAhliTMP> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxTenagaAhliTMP))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxTenagaAhliTMP))]
        public IHttpActionResult Post(trxTenagaAhliTMP myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxTenagaAhliTMP myData)
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
        [Route("api/TrxTenagaAhliTMP/GetByRekanan/{idRekanan}")]
        public IEnumerable<trxTenagaAhliTMP> GetByRekanan(System.Guid idRekanan)
        {
            IEnumerable<trxTenagaAhliTMP> TenagaAhliByRekanan;
            TenagaAhliByRekanan = _repTAhli.GetByRekanan(idRekanan);
            return TenagaAhliByRekanan;
        }
        [Route("api/TrxTenagaAhliTMP/GetByGuidHeader/{guidHeader}")]
        public List<trxTenagaAhliTMP> GetByGuidHeader(Guid guidHeader)
        {
            List<trxTenagaAhliTMP> TenagaAhliByGuidHeader;
            TenagaAhliByGuidHeader = _repTAhli.GetByGuidHeader(guidHeader);
            return TenagaAhliByGuidHeader;
        }
    }
}
