using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class TrxTenagaPendukungTMPController : ApiController
    {
        private IDataAccessRepository<trxTenagaPendukungTMP, int> _repository;
        private TrxTenagaPendukungTMPRep _repPendukung = new TrxTenagaPendukungTMPRep();
        //Inject the DataAccessRepository using Construction Injection 
        public TrxTenagaPendukungTMPController(IDataAccessRepository<trxTenagaPendukungTMP, int> r, TrxTenagaPendukungTMPRep repPendukung)
        {
            _repository = r;
            _repPendukung = repPendukung;
        }
        public IEnumerable<trxTenagaPendukungTMP> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxTenagaPendukungTMP))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxTenagaPendukungTMP))]
        public IHttpActionResult Post(trxTenagaPendukungTMP myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxTenagaPendukungTMP myData)
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
        [Route("api/TrxTenagaPendukungTMP/GetByRekanan/{idRekanan}")]
        public IEnumerable<trxTenagaPendukungTMP> GetByRekanan(System.Guid idRekanan)
        {
            IEnumerable<trxTenagaPendukungTMP> PendukungRekanan;
            PendukungRekanan = _repPendukung.GetByRekanan(idRekanan);
            return PendukungRekanan;
        }
        [Route("api/TrxTenagaPendukungTMP/GetByGuidHeader/{guidHeader}")]
        public List<trxTenagaPendukungTMP> GetByGuidHeader(Guid guidHeader)
        {
            List<trxTenagaPendukungTMP> TenagaPendukungByGuidHeader;
            TenagaPendukungByGuidHeader = _repPendukung.GetByGuidHeader(guidHeader);
            return TenagaPendukungByGuidHeader;
        }
    }
}
