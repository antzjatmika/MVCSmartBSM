using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;
using Omu.ValueInjecter;

namespace APIService.Controllers
{
    public class TrxDetailPekerjaanAS_1MController : ApiController
    {
        private IDataAccessRepository<trxDetailPekerjaanAS_1M, int> _repository;
        private TrxDetailPekerjaanHeaderRep _repDetailPek;
        private TrxDetailPekerjaanAS_1MRep _repDetail;
        private MstTypeOfRegionRep _repRegion;
        private MstSegmentasiRep _repSegmen;
        private MstReferenceRep _repReff;
        private MstSubRegionRep _repSubRegion;
        //Inject the DataAccessRepository using Construction Injection 
        public TrxDetailPekerjaanAS_1MController(IDataAccessRepository<trxDetailPekerjaanAS_1M, int> r, TrxDetailPekerjaanAS_1MRep repDetail
            , MstTypeOfRegionRep repRegion, MstSegmentasiRep repSegmen, MstReferenceRep repReff, TrxDetailPekerjaanHeaderRep repDetailPek, MstSubRegionRep repSubRegion)
        {
            _repository = r;
            _repDetailPek = repDetailPek;
            _repDetail = repDetail;
            _repRegion = repRegion;
            _repSegmen = repSegmen;
            _repReff = repReff;
            _repSubRegion = repSubRegion;
        }
        public IEnumerable<trxDetailPekerjaanAS_1M> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxDetailPekerjaanAS_1M))]
        public IHttpActionResult Get(int id)
        {
            trxDetailPekerjaanAS_1M mySingle = new trxDetailPekerjaanAS_1M();
            if (id > 0)
            {
                trxDetailPekerjaanAS_1M myData = _repository.Get(id);
                mySingle.InjectFrom(myData);
            }
            return Ok(mySingle); 
        }

        [ResponseType(typeof(trxDetailPekerjaanAS_1M))]
        public IHttpActionResult Post(trxDetailPekerjaanAS_1M myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxDetailPekerjaanAS_1M myData)
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
        [Route("api/trxDetailPekerjaanAS_1M/GetByGuidHeader/{GuidHeader}")]
        public trxDetailPekerjaanHeaderForm GetByGuidHeader(System.Guid GuidHeader)
        {
            trxDetailPekerjaanHeaderForm myDataForm = new trxDetailPekerjaanHeaderForm();
            trxDetailPekerjaanHeader myData = _repDetailPek.GetByGuidHeader(GuidHeader);
            myDataForm.InjectFrom(myData);
            IEnumerable<trxDetailPekerjaanAS_1M> myDataList = _repDetail.GetByGuidHeader(GuidHeader);
            myDataForm.PekerjaanAS_1MColls = myDataList;
            return myDataForm;
        }
        [HttpGet]
        [Route("api/trxDetailPekerjaanAS_1M/GetByRekanan/{idRekanan}")]
        public IEnumerable<trxDetailPekerjaanAS_1M> GetByRekanan(System.Guid idRekanan)
        {
            IEnumerable<trxDetailPekerjaanAS_1M> DetailPekByRekanan;
            DetailPekByRekanan = _repDetail.GetByRekanan(idRekanan);
            return DetailPekByRekanan;
        }
        [HttpGet]
        [Route("api/trxDetailPekerjaanAS_1M/GetLapAS1MByRekanan/{idRekanan}")]
        public IEnumerable<fGetLapAS1MByRekanan_Result> GetLapAS1MByRekanan(System.Guid idRekanan)
        {
            IEnumerable<fGetLapAS1MByRekanan_Result> DetailPekByRekanan;
            DetailPekByRekanan = _repDetail.GetLapAS1MByRekanan(idRekanan);
            return DetailPekByRekanan;
        }
        [HttpGet]
        [Route("api/trxDetailPekerjaanAS_1M/PekerjaanByTypeOfRekanan/{IdTypeOfRekanan}")]
        public trxDetailPekerjaanAS_1MMulti PekerjaanByTypeOfRekanan(int IdTypeOfRekanan)
        {
            trxDetailPekerjaanAS_1MMulti myDataMulti = new trxDetailPekerjaanAS_1MMulti();
            IEnumerable<fPekerjaanAS_1MByTypeOfRekanan_Result> myDataList = _repDetail.PekerjaanByTypeOfRekanan(IdTypeOfRekanan);
            myDataMulti.XLSDetailPekerjaanColls = myDataList;
            myDataMulti.SubRegionColls = _repSubRegion.Get();
            myDataMulti.TypeOfSegmentasi5Colls = _repSegmen.Get();

            return myDataMulti;
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        [Route("api/trxDetailPekerjaanAS_1M/XLS_PekerjaanByTypeOfRek/{intTypeOfRekanan}/{strFilterExpre1}/{strFilterExpre2}")]
        public IEnumerable<fPekerjaanAS_1MByTypeOfRekanan_Result> XLS_PekerjaanByTypeOfRekAS(int intTypeOfRekanan, string strFilterExpre1, string strFilterExpre2)
        {
            trxDetailPekerjaanAS_1MMulti myDataMulti = new trxDetailPekerjaanAS_1MMulti();
            string[] arrReturn = new string[] { };
            arrReturn = XLSExportHelper.ParseQueryPekerjaan(strFilterExpre1, strFilterExpre2);
            var pekerjaanColl = _repDetail.XLS_PekerjaanByTypeOfRek(intTypeOfRekanan, arrReturn[0], arrReturn[1]);

            return pekerjaanColl;
        }

    }
}
