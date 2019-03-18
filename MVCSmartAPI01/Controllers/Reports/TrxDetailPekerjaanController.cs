using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;
using Omu.ValueInjecter;
using DevExpress.Data.Filtering;

namespace APIService.Controllers
{
    public class TrxDetailPekerjaanController : ApiController
    {
        private IDataAccessRepository<trxDetailPekerjaan, int> _repository;
        private TrxDetailPekerjaanRep _repDetailPek;
        private MstTypeOfRegionRep _repRegion;
        private MstSubRegionRep _repSubRegion;
        private MstSegmentasiRep _repSegmen;
        private MstReferenceRep _repReff;
        //Inject the DataAccessRepository using Construction Injection 
        public TrxDetailPekerjaanController(IDataAccessRepository<trxDetailPekerjaan, int> r, TrxDetailPekerjaanRep repDetailPek, MstTypeOfRegionRep repRegion
            , MstSegmentasiRep repSegmen, MstReferenceRep repReff, MstSubRegionRep repSubRegion)
        {
            _repository = r;
            _repDetailPek = repDetailPek;
            _repRegion = repRegion;
            _repSegmen = repSegmen;
            _repReff = repReff;
            _repSubRegion = repSubRegion;
        }
        public IEnumerable<trxDetailPekerjaan> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxDetailPekerjaan))]
        public IHttpActionResult Get(int id)
        {
            trxDetailPekerjaanSingle mySingle = new trxDetailPekerjaanSingle();
            if (id > 0)
            {
                trxDetailPekerjaan myData = _repository.Get(id);
                mySingle.InjectFrom(myData);
            }
            else
            {
                mySingle.MaxYear = DateTime.Today.Year;
                mySingle.GuidHeader = Guid.Empty;
            }
            mySingle.TypeOfRegionColls = _repRegion.Get();
            mySingle.SubRegionColls = _repSubRegion.Get();
            mySingle.TypeOfSegmentasi3Colls = _repSegmen.SegmenForKAP();
            mySingle.TypeOfSegmentasi5Colls = _repSegmen.Get();
            mySingle.TypeTotalAsetColls = _repReff.GetByType("TotalAset");

            return Ok(mySingle); 
        }

        [ResponseType(typeof(trxDetailPekerjaan))]
        public IHttpActionResult Post(trxDetailPekerjaan myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxDetailPekerjaan myData)
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
        [Route("api/TrxDetailPekerjaan/GetByRekanan/{idRekanan}")]
        public IEnumerable<trxDetailPekerjaan> GetByRekanan(System.Guid idRekanan)
        {
            IEnumerable<trxDetailPekerjaan> DetailPekByRekanan;
            DetailPekByRekanan = _repDetailPek.GetByRekanan(idRekanan);
            return DetailPekByRekanan;
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        [Route("api/TrxDetailPekerjaan/GetByRekananXLS/{idRekanan}/{strFilterExpre1}/{strFilterExpre2}")]
        public IEnumerable<trxDetailPekerjaan> GetByRekananXLS(System.Guid idRekanan, string strFilterExpre1, string strFilterExpre2)
        {
            string[] arrReturn = new string[] { };
            arrReturn = XLSExportHelper.ParseQueryPekerjaan(strFilterExpre1, strFilterExpre2);
            var rekananCollection = _repDetailPek.GetByRekananXLS(idRekanan, arrReturn[0], arrReturn[1]);
            return rekananCollection;
        }
        [HttpGet]
        [Route("api/TrxDetailPekerjaan/PekerjaanByTypeOfRekanan/{IdTypeOfRekanan}")]
        public IEnumerable<fPekerjaanByTypeOfRekanan_Result> PekerjaanByTypeOfRekanan(int IdTypeOfRekanan)
        {
            IEnumerable<fPekerjaanByTypeOfRekanan_Result> DetailPekByRekanan;
            DetailPekByRekanan = _repDetailPek.PekerjaanByTypeOfRekanan(IdTypeOfRekanan);
            return DetailPekByRekanan;
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        [Route("api/TrxDetailPekerjaan/XLS_PekerjaanByTypeOfRek/{intTypeOfRekanan}/{strFilterExpre1}/{strFilterExpre2}")]
        public IEnumerable<fPekerjaanByTypeOfRekanan_Result> XLS_PekerjaanByTypeOfRek(int intTypeOfRekanan, string strFilterExpre1, string strFilterExpre2)
        {
            //string strTemp2 = strFilterExpre2.Replace(" ME ", " >= ");
            //strTemp2 = strTemp2.Replace(" LE ", " <= ");
            //strTemp2 = strTemp2.Replace(" NN ", " && ");
            //string strFilterExp1 = CustomCriteriaToLinqWhereParser.Process(CriteriaOperator.Parse(strFilterExpre1) as CriteriaOperator);
            //string strFilterExp2 = strFilterExpre2;

            //strFilterExp1 = "TanggalSelesaiPekerjaan >= 2001-01-01 && TanggalSelesaiPekerjaan <= 2017-09-09";
            string[] arrReturn = new string[] { };
            
            arrReturn = XLSExportHelper.ParseQueryPekerjaan(strFilterExpre1, strFilterExpre2);
            var rekananCollection = _repDetailPek.XLS_PekerjaanByTypeOfRek(intTypeOfRekanan, arrReturn[0], arrReturn[1]);
            return rekananCollection;
        }

    }
}
