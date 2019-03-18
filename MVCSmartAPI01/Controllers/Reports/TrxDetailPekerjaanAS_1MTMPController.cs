using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;
using Omu.ValueInjecter;

namespace APIService.Controllers
{
    public class trxDetailPekerjaanAS_1MTMPController : ApiController
    {
        private IDataAccessRepository<trxDetailPekerjaanAS_1MTMP, int> _repository;
        private TrxDetailPekerjaanAS_1MTMPRep _repDetailPek;
        private MstTypeOfRegionRep _repRegion;
        private MstSegmentasiRep _repSegmen;
        private MstSubRegionRep _repSubRegion;
        //Inject the DataAccessRepository using Construction Injection 
        public trxDetailPekerjaanAS_1MTMPController(IDataAccessRepository<trxDetailPekerjaanAS_1MTMP, int> r, TrxDetailPekerjaanAS_1MTMPRep repDetailPek
            , MstTypeOfRegionRep repRegion, MstSegmentasiRep repSegmen, MstSubRegionRep repSubRegion)
        {
            _repository = r;
            _repDetailPek = repDetailPek;
            _repRegion = repRegion;
            _repSegmen = repSegmen;
            _repSubRegion = repSubRegion;
        }
        public IEnumerable<trxDetailPekerjaanAS_1MTMP> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxDetailPekerjaanAS_1MTMP))]
        public IHttpActionResult Post(trxDetailPekerjaanAS_1MTMP myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxDetailPekerjaanAS_1MTMP myData)
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
        [Route("api/TrxDetailPekerjaanAS_1MTMP/GetByRekanan/{idRekanan}")]
        public IEnumerable<trxDetailPekerjaanAS_1MTMP> GetByRekanan(System.Guid idRekanan)
        {
            IEnumerable<trxDetailPekerjaanAS_1MTMP> DetailPekByRekanan;
            DetailPekByRekanan = _repDetailPek.GetByRekanan(idRekanan);
            return DetailPekByRekanan;
        }
        [HttpGet]
        [Route("api/TrxDetailPekerjaanAS_1MTMP/GetByGuidHeader/{GuidHeader}")]
        //public IEnumerable<trxDetailPekerjaanAS_1MTMP> GetByGuidHeader(System.Guid GuidHeader)
        public trxDetailPekerjaanHeaderForm GetByGuidHeader(System.Guid GuidHeader)
        {
            trxDetailPekerjaanHeaderForm myDataForm = new trxDetailPekerjaanHeaderForm();
            List<trxDetailPekerjaanAS_1M> DetailPekByGuidHeaderNew = new List<trxDetailPekerjaanAS_1M>();
            IEnumerable<trxDetailPekerjaanAS_1MTMP> DetailPekByGuidHeader;
            DetailPekByGuidHeader = _repDetailPek.GetByGuidHeader(GuidHeader);
            foreach (trxDetailPekerjaanAS_1MTMP item in DetailPekByGuidHeader)
	        {
		        trxDetailPekerjaanAS_1M newItem = new trxDetailPekerjaanAS_1M();
                newItem.InjectFrom(item);
                DetailPekByGuidHeaderNew.Add(newItem);
	        }
            myDataForm.PekerjaanAS_1MColls = DetailPekByGuidHeaderNew;
            myDataForm.SubRegionColls = _repSubRegion.Get();
            myDataForm.TypeOfSegmentasi5Colls = _repSegmen.Get();
            return myDataForm;

            //IEnumerable<trxDetailPekerjaanAS_1MTMP> DetailPekByGuidHeader;
            //DetailPekByGuidHeader = _repDetailPek.GetByGuidHeader(GuidHeader);
            //return DetailPekByGuidHeader;
        }
    }
}
