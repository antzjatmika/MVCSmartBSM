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
    public class TrxDetailPekerjaanAS_3MController : ApiController
    {
        private IDataAccessRepository<trxDetailPekerjaanAS_3M, int> _repository;
        private TrxDetailPekerjaanAS_3MRep _repDetailPek;
        private MstTypeOfRegionRep _repRegion;
        private MstSegmentasiRep _repSegmen;
        private MstReferenceRep _repReff;
        MstSubRegionRep _repSubRegion;
        //Inject the DataAccessRepository using Construction Injection 
        public TrxDetailPekerjaanAS_3MController(IDataAccessRepository<trxDetailPekerjaanAS_3M, int> r, TrxDetailPekerjaanAS_3MRep repDetailPek, MstTypeOfRegionRep repRegion
            , MstSegmentasiRep repSegmen, MstReferenceRep repReff, MstSubRegionRep repSubRegion)
        {
            _repository = r;
            _repDetailPek = repDetailPek;
            _repRegion = repRegion;
            _repSegmen = repSegmen;
            _repReff = repReff;
            _repSubRegion = repSubRegion;
        }
        public IEnumerable<trxDetailPekerjaanAS_3M> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxDetailPekerjaanAS_3M))]
        public IHttpActionResult Get(int id)
        {
            trxDetailPekerjaanAS_3M mySingle = new trxDetailPekerjaanAS_3M();
            if (id > 0)
            {
                trxDetailPekerjaanAS_3M myData = _repository.Get(id);

                mySingle.InjectFrom(myData);
            }
            //mySingle.SubRegionColls = _repSubRegion.Get();
            //public IEnumerable<mstSubRegion> SubRegionColls { get; set; }
            return Ok(mySingle); 
        }

        [ResponseType(typeof(trxDetailPekerjaanAS_3M))]
        public IHttpActionResult Post(trxDetailPekerjaanAS_3M myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxDetailPekerjaanAS_3M myData)
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
        [Route("api/trxDetailPekerjaanAS_3M/GetByRekanan/{idRekanan}")]
        public IEnumerable<trxDetailPekerjaanAS_3M> GetByRekanan(System.Guid idRekanan)
        {
            IEnumerable<trxDetailPekerjaanAS_3M> DetailPekByRekanan;
            DetailPekByRekanan = _repDetailPek.GetByRekanan(idRekanan);
            return DetailPekByRekanan;
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        [Route("api/trxDetailPekerjaanAS_3M/GetByRekananXLS/{idRekanan}/{strFilterExpre1}/{strFilterExpre2}")]
        public IEnumerable<trxDetailPekerjaanAS_3M> GetByRekananXLS(System.Guid idRekanan, string strFilterExpre1, string strFilterExpre2)
        {
            string[] arrReturn = new string[] { };
            arrReturn = XLSExportHelper.ParseQueryPekerjaan(strFilterExpre1, strFilterExpre2);
            var rekananCollection = _repDetailPek.GetByRekananXLS(idRekanan, arrReturn[0], arrReturn[1]);
            return rekananCollection;
        }
        [HttpGet]
        [Route("api/trxDetailPekerjaanAS_3M/PekerjaanByTypeOfRekanan/{IdTypeOfRekanan}")]
        public IEnumerable<fGetPekerjaan3MByIdTypeOfRekanan_Result> PekerjaanByTypeOfRekanan(int IdTypeOfRekanan)
        {
            IEnumerable<fGetPekerjaan3MByIdTypeOfRekanan_Result> myDataList = _repDetailPek.PekerjaanByTypeOfRekanan(IdTypeOfRekanan);
            return myDataList;
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        [Route("api/trxDetailPekerjaanAS_3M/XLS_PekerjaanByTypeOfRek/{intTypeOfRekanan}/{strFilterExpre1}/{strFilterExpre2}")]
        public IEnumerable<fPekerjaanAS_3MByTypeOfRekanan_Result> XLS_PekerjaanByTypeOfRekAS(int intTypeOfRekanan, string strFilterExpre1, string strFilterExpre2)
        {
            string[] arrReturn = new string[] { };
            arrReturn = XLSExportHelper.ParseQueryPekerjaan(strFilterExpre1, strFilterExpre2);
            var rekananCollection = _repDetailPek.XLS_PekerjaanByTypeOfRek(intTypeOfRekanan, arrReturn[0], arrReturn[1]);
            return rekananCollection;
        }

    }
}
