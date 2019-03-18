using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;
using Omu.ValueInjecter;

namespace APIService.Controllers
{
    public class trxDetailPekerjaanTMPController : ApiController
    {
        private IDataAccessRepository<trxDetailPekerjaanTMP, int> _repository;
        private TrxDetailPekerjaanTMPRep _repDetailPek;
        private MstTypeOfRegionRep _repRegion;
        private MstSegmentasiRep _repSegmen;
        //Inject the DataAccessRepository using Construction Injection 
        public trxDetailPekerjaanTMPController(IDataAccessRepository<trxDetailPekerjaanTMP, int> r, TrxDetailPekerjaanTMPRep repDetailPek, MstTypeOfRegionRep repRegion, MstSegmentasiRep repSegmen)
        {
            _repository = r;
            _repDetailPek = repDetailPek;
            _repRegion = repRegion;
            _repSegmen = repSegmen;
        }
        public IEnumerable<trxDetailPekerjaanTMP> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxDetailPekerjaanTMP))]
        public IHttpActionResult Post(trxDetailPekerjaanTMP myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxDetailPekerjaanTMP myData)
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
        [HttpGet]
        [Route("api/TrxDetailPekerjaanTMP/GetByRekanan/{idRekanan}")]
        public IEnumerable<trxDetailPekerjaanTMP> GetByRekanan(System.Guid idRekanan)
        {
            IEnumerable<trxDetailPekerjaanTMP> DetailPekByRekanan;
            DetailPekByRekanan = _repDetailPek.GetByRekanan(idRekanan);
            return DetailPekByRekanan;
        }
        [HttpGet]
        [Route("api/TrxDetailPekerjaanTMP/GetByGuidHeader/{GuidHeader}")]
        public IEnumerable<trxDetailPekerjaanTMP> GetByGuidHeader(System.Guid GuidHeader)
        {
            IEnumerable<trxDetailPekerjaanTMP> DetailPekByGuidHeader;
            DetailPekByGuidHeader = _repDetailPek.GetByGuidHeader(GuidHeader);
            return DetailPekByGuidHeader;
        }
    }
}
