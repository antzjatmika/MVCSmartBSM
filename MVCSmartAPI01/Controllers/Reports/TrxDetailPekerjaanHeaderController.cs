using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;
using Omu.ValueInjecter;
using System.Data.SqlClient;

namespace APIService.Controllers
{
    public class TrxDetailPekerjaanHeaderController : ApiController
    {
        private IDataAccessRepository<trxDetailPekerjaanHeader, int> _repository;
        private TrxDetailPekerjaanHeaderRep _repDetailPek;
        private TrxDetailPekerjaanRep _repDetail;
        private TrxDetailPekerjaanAS_1MRep _repDetailAS_1M;
        private MstSubRegionRep _repSubRegion;
        private MstSegmentasiRep _repSegmen;

        //Inject the DataAccessRepository using Construction Injection 
        public TrxDetailPekerjaanHeaderController(IDataAccessRepository<trxDetailPekerjaanHeader, int> r, TrxDetailPekerjaanHeaderRep repDetailPek
            , TrxDetailPekerjaanRep repDetail, TrxDetailPekerjaanAS_1MRep repDetailAS_1M, MstSegmentasiRep repSegmen, MstSubRegionRep repSubRegion)
        {
            _repository = r;
            _repDetailPek = repDetailPek;
            _repDetail = repDetail;
            _repDetailAS_1M = repDetailAS_1M;
            _repSegmen = repSegmen;
            _repSubRegion = repSubRegion;
        }
        public IEnumerable<trxDetailPekerjaanHeader> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxDetailPekerjaanHeader))]
        public IHttpActionResult Get(int id)
        {
            trxDetailPekerjaanHeader myData = _repository.Get(id);

            //return Ok (_repository.Get(id));
            return Ok(myData); 
        }

        [ResponseType(typeof(trxDetailPekerjaanHeader))]
        public IHttpActionResult Post(trxDetailPekerjaanHeader myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxDetailPekerjaanHeader myData)
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
        [Route("api/TrxDetailPekerjaanHeader/DelHeaderByRekanan/{idRekanan}/{PekHeader}")]
        public IEnumerable<trxDetailPekerjaanHeader> DelHeaderByRekanan(Guid idRekanan, Guid PekHeader)
        {
            IEnumerable<trxDetailPekerjaanHeader> DetailPekByRekanan;
            //DELETE HEADER & CHILD
            string strReturn = "OKBOSS";
            try
            {
                using (var context = new DB_SMARTEntities1())
                {
                    var paramIdHeader = new SqlParameter("@GuidHeader", PekHeader);
                    var result = context.Database.ExecuteSqlCommand("EXEC spDelHeaderByRekanan @GuidHeader", paramIdHeader);
                }
            }
            catch (Exception ex)
            {
                strReturn = ex.Message;
            }
            DetailPekByRekanan = _repDetailPek.GetByRekanan(idRekanan);
            return DetailPekByRekanan;
        }
        [HttpGet]
        [Route("api/TrxDetailPekerjaanHeader/GetByRekanan/{idRekanan}")]
        public IEnumerable<trxDetailPekerjaanHeader> GetByRekanan(System.Guid idRekanan)
        {
            IEnumerable<trxDetailPekerjaanHeader> DetailPekByRekanan;
            DetailPekByRekanan = _repDetailPek.GetByRekanan(idRekanan);
            return DetailPekByRekanan;
        }
        [HttpGet]
        [Route("api/TrxDetailPekerjaanHeader/GetByGuidHeader/{GuidHeader}")]
        public trxDetailPekerjaanHeaderForm GetByGuidHeader(System.Guid GuidHeader)
        {
            trxDetailPekerjaanHeaderForm myDataForm = new trxDetailPekerjaanHeaderForm();
            trxDetailPekerjaanHeader myData = _repDetailPek.GetByGuidHeader(GuidHeader);
            myDataForm.InjectFrom(myData);
            IEnumerable<trxDetailPekerjaan> myDataList = _repDetail.GetByGuidHeader(GuidHeader);
            myDataForm.PekerjaanColls = myDataList;
            return myDataForm;
        }
        [HttpGet]
        [Route("api/TrxDetailPekerjaanHeader/GetByGuidHeaderAS_1M/{GuidHeader}")]
        public trxDetailPekerjaanHeaderForm GetByGuidHeaderAS_1M(System.Guid GuidHeader)
        {
            trxDetailPekerjaanHeaderForm myDataForm = new trxDetailPekerjaanHeaderForm();
            trxDetailPekerjaanHeader myData = _repDetailPek.GetByGuidHeader(GuidHeader);
            myDataForm.InjectFrom(myData);
            IEnumerable<trxDetailPekerjaanAS_1M> myDataList = _repDetailAS_1M.GetByGuidHeader(GuidHeader);
            myDataForm.PekerjaanAS_1MColls = myDataList;
            myDataForm.SubRegionColls = _repSubRegion.Get();
            myDataForm.TypeOfSegmentasi5Colls = _repSegmen.Get();
            return myDataForm;
        }
        [AcceptVerbs("GET", "POST")]
        [Route("api/TrxDetailPekerjaanHeader/ExecTransDataPek/{pGuidHeader}")]
        public string TransDataDetailPekerjaan(Guid pGuidHeader)
        {
            string strReturn = "Executed OK";
            try
            {
                using (var context = new DB_SMARTEntities1())
                {
                    var paramIdHeader = new SqlParameter("@GuidHeader", pGuidHeader);
                    var result = context.Database.ExecuteSqlCommand("EXEC spTranDataDetailPekerjaan @GuidHeader", paramIdHeader);
                }
            }
            catch (Exception ex)
            {
                strReturn = ex.Message;
            }
            return strReturn;
        }

        [AcceptVerbs("GET", "POST")]
        [Route("api/TrxDetailPekerjaanHeader/ExecTransDataPekAS_1M/{pGuidHeader}")]
        public string TransDataDetailPekerjaanAS_1M(Guid pGuidHeader)
        {
            string strReturn = "Executed OK";
            try
            {
                using (var context = new DB_SMARTEntities1())
                {
                    var paramIdHeader = new SqlParameter("@GuidHeader", pGuidHeader);
                    var result = context.Database.ExecuteSqlCommand("EXEC spTranDataDetailPekerjaanAS_1M @GuidHeader", paramIdHeader);
                }
            }
            catch (Exception ex)
            {
                strReturn = ex.Message;
            }
            return strReturn;
        }
    }
}
