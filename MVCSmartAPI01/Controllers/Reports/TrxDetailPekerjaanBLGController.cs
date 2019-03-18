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
    public class TrxDetailPekerjaanBLGController : ApiController
    {
        private IDataAccessRepository<trxDetailPekerjaanBLG, int> _repository;
        private TrxDetailPekerjaanBLGRep _repDetailPek;
        private MstTypeOfRegionRep _repRegion;
        private MstSubRegionRep _repSubRegion;
        private MstSegmentasiRep _repSegmen;
        private MstReferenceRep _repReff;
        //Inject the DataAccessRepository using Construction Injection 
        public TrxDetailPekerjaanBLGController(IDataAccessRepository<trxDetailPekerjaanBLG, int> r, TrxDetailPekerjaanBLGRep repDetailPek, MstTypeOfRegionRep repRegion
            , MstSegmentasiRep repSegmen, MstReferenceRep repReff, MstSubRegionRep repSubRegion)
        {
            _repository = r;
            _repDetailPek = repDetailPek;
            _repRegion = repRegion;
            _repSegmen = repSegmen;
            _repReff = repReff;
            _repSubRegion = repSubRegion;
        }
        public IEnumerable<trxDetailPekerjaanBLG> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxDetailPekerjaanBLGSingle))]
        public IHttpActionResult Get(int id)
        {
            trxDetailPekerjaanBLGSingle mySingle = new trxDetailPekerjaanBLGSingle();

            if (id > 0)
            {
                trxDetailPekerjaanBLG myData = _repository.Get(id);
                mySingle.InjectFrom(myData);
            }
            else
            {
                //mySingle.MaxYear = DateTime.Today.Year;
                //mySingle.GuidHeader = Guid.Empty;
            }
            mySingle.TypeOfSegmentasi3Colls = _repSegmen.SegmenForKAP();
            mySingle.TypeOfSegmentasi5Colls = _repSegmen.Get();
            mySingle.SubRegionColls = _repSubRegion.Get();

            //return Ok (_repository.Get(id));
            return Ok(mySingle); 
        }

        [ResponseType(typeof(trxDetailPekerjaanBLG))]
        public IHttpActionResult Post(trxDetailPekerjaanBLG myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxDetailPekerjaanBLG myData)
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
        [Route("api/trxDetailPekerjaanBLG/GetByRekanan/{idRekanan}")]
        public IEnumerable<trxDetailPekerjaanBLG> GetByRekanan(System.Guid idRekanan)
        {
            IEnumerable<trxDetailPekerjaanBLG> DetailPekByRekanan;
            DetailPekByRekanan = _repDetailPek.GetByRekanan(idRekanan);
            return DetailPekByRekanan;
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        [Route("api/trxDetailPekerjaanBLG/GetByRekananXLS/{idRekanan}/{strFilterExpre1}/{strFilterExpre2}")]
        public IEnumerable<trxDetailPekerjaanBLG> GetByRekananXLS(System.Guid idRekanan, string strFilterExpre1, string strFilterExpre2)
        {
            string[] arrReturn = new string[] { };
            arrReturn = XLSExportHelper.ParseQueryPekerjaan(strFilterExpre1, strFilterExpre2);
            var rekananCollection = _repDetailPek.GetByRekananXLS(idRekanan, arrReturn[0], arrReturn[1]);
            return rekananCollection;
        }
        [HttpGet]
        [Route("api/trxDetailPekerjaanBLG/PekerjaanByTypeOfRekanan")]
        public IEnumerable<fPekerjaanBLGByTypeOfRekanan_Result> PekerjaanByTypeOfRekanan()
        {
            IEnumerable<fPekerjaanBLGByTypeOfRekanan_Result> myDataList = _repDetailPek.PekerjaanByTypeOfRekanan();
            return myDataList;
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        [Route("api/TrxDetailPekerjaanBLG/XLS_PekerjaanByTypeOfRek/{strFilterExpre1}/{strFilterExpre2}")]
        public IEnumerable<fPekerjaanBLGByTypeOfRekanan_Result> XLS_PekerjaanByTypeOfRekBLG(string strFilterExpre1, string strFilterExpre2)
        {
            string[] arrReturn = new string[] { };
            arrReturn = XLSExportHelper.ParseQueryPekerjaan(strFilterExpre1, strFilterExpre2);
            var rekananCollection = _repDetailPek.XLS_PekerjaanByTypeOfRek(arrReturn[0], arrReturn[1]);
            return rekananCollection;
        }

    }
}
