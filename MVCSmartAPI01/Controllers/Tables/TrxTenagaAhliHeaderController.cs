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
    public class TrxTenagaAhliHeaderController : ApiController
    {
        private IDataAccessRepository<trxTenagaAhliHeader, int> _repository;
        private TrxTenagaAhliHeaderRep _repTAhliHeader = new TrxTenagaAhliHeaderRep();
        private TrxTenagaAhliRep _repTAhli = new TrxTenagaAhliRep();
        private TrxTenagaPendukungRep _repTPendukung = new TrxTenagaPendukungRep();
        private MstRekananRep _repRekanan;
        private TrxTenagaAhliUploadRep _repTAUpload;
        //Inject the DataAccessRepository using Construction Injection 
        public TrxTenagaAhliHeaderController(IDataAccessRepository<trxTenagaAhliHeader, int> r, TrxTenagaAhliHeaderRep repTAhliHeader, TrxTenagaAhliRep repTAhli
            , TrxTenagaPendukungRep repTPendukung, MstRekananRep repRekanan, TrxTenagaAhliUploadRep repTAUpload)
        {
            _repository = r;
            _repTAhliHeader = repTAhliHeader;
            _repTAhli = repTAhli;
            _repTPendukung = repTPendukung;
            _repRekanan = repRekanan;
            _repTAUpload = repTAUpload;
        }
        public IEnumerable<trxTenagaAhliHeader> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxTenagaAhliHeader))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxTenagaAhliHeader))]
        public IHttpActionResult Post(trxTenagaAhliHeader myData)
        {
            myData.LMDate = DateTime.Today;
            _repository.Post(myData);
            //Update catatan di mstRekanan
            _repRekanan.UpdateNote(myData.IdRekanan, myData.ProcInfo);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxTenagaAhliHeader myData)
        {
            myData.LMDate = DateTime.Today;
            _repository.Put(id, myData);
            //Update catatan di mstRekanan
            _repRekanan.UpdateNote(myData.IdRekanan, myData.ProcInfo);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("api/TrxTenagaAhliHeader/GetTAUpload/{id}")]
        [ResponseType(typeof(trxTenagaAhliUpload))]
        public IHttpActionResult GetTAUpload(int id)
        {
            return Ok(_repTAUpload.Get(id));
        }
        
        [Route("api/TrxTenagaAhliHeader/PostTAUpload")]
        [ResponseType(typeof(trxTenagaAhliUpload))]
        public IHttpActionResult PostTAUpload(trxTenagaAhliUpload myData)
        {
            myData.LMDate = DateTime.Today;
            _repTAUpload.Post(myData);
            //Update catatan di mstRekanan
            //_repRekanan.UpdateNote(myData.IdRekanan, myData.ProcInfo);
            return Ok(myData);
        }
        [Route("api/TrxTenagaAhliHeader/PutTAUpload/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTAUpload(int id, trxTenagaAhliUpload myData)
        {
            myData.LMDate = DateTime.Today;
            _repTAUpload.Put(id, myData);
            //Update catatan di mstRekanan
            //_repRekanan.UpdateNote(myData.IdRekanan, myData.ProcInfo);
            return StatusCode(HttpStatusCode.NoContent);
        }
        [AcceptVerbs("GET", "POST")]
        [Route("api/TrxTenagaAhliHeader/DeleteTAUpload/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteTAUpload(int id)
        {
            _repTAUpload.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
        
        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(int id)
        {
            _repository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("api/TrxTenagaAhliHeader/GetByRekanan/{idTipeTenaga}/{idRekanan}")]
        public IEnumerable<trxTenagaAhliHeader> GetByRekanan(int idTipeTenaga, System.Guid idRekanan)
        {
            IEnumerable<trxTenagaAhliHeader> TAHeaderRekanan;
            TAHeaderRekanan = _repTAhliHeader.GetByRekanan(idTipeTenaga, idRekanan);
            return TAHeaderRekanan;
        }

        [Route("api/TrxTenagaAhliHeader/GetUploadByRekanan/{idRekanan}")]
        public IEnumerable<trxTenagaAhliUpload> GetUploadByRekanan(System.Guid idRekanan)
        {
            IEnumerable<trxTenagaAhliUpload> TAHeaderRekanan;
            TAHeaderRekanan = _repTAhliHeader.GetUploadByRekanan(idRekanan);
            return TAHeaderRekanan;
        }

        [Route("api/TrxTenagaAhliHeader/GetByGuidHeader/{guidHeader}")]
        public trxTenagaAhliHeaderForm GetByGuidHeader(Guid guidHeader)
        {
            trxTenagaAhliHeaderForm TAHeaderForm = new trxTenagaAhliHeaderForm();
            //header
            //trxTenagaAhliHeader TAHeader = _repository.Get(guidHeader);
            trxTenagaAhliHeader TAHeader = _repTAhliHeader.GetByGuidHeader(guidHeader);
            TAHeaderForm.InjectFrom(TAHeader);
            //tenaga ahli collection
            TAHeaderForm.TenagaAhliColls = _repTAhli.GetByGuidHeader(guidHeader);
            //TAHeaderForm.TenagaAhliTTColls = _repTAhli.GetByGuidHeader(guidHeader,2);
            //tenaga pendukung collection
            TAHeaderForm.TenagaPendukungColls = _repTPendukung.GetByGuidHeader(guidHeader);
            return TAHeaderForm;
        }
        [AcceptVerbs("GET", "POST")]
        //[Route("api/TrxTenagaAhliHeader/ExecTransDataTA/{pGuidHeader}/{pstrIdRekanan}/{pstrXLSPointer}")]
        [Route("api/TrxTenagaAhliHeader/ExecTransDataTA/{pGuidHeader}")]
        //public string TransDataTenagaAhli(Guid pGuidHeader, string pstrIdRekanan, string pstrXLSPointer = "kosong")
        public string TransDataTenagaAhli(Guid pGuidHeader)
        {
            string strReturn = "Executed OK";
            try
            {
                using (var context = new DB_SMARTEntities1())
                {
                    var paramIdHeader = new SqlParameter("@GuidHeader", pGuidHeader);
                    //var paramIdRekanan = new SqlParameter("@IdRekanan", pstrIdRekanan);
                    //var paramXLSPointer = new SqlParameter("@XLSPointer", pstrXLSPointer);
                    //var result = context.Database.ExecuteSqlCommand("EXEC spTranDataTenagaAhli @GuidHeader, @IdRekanan, @XLSPointer"
                    //    , paramIdHeader, paramIdRekanan, paramXLSPointer);
                    var result = context.Database.ExecuteSqlCommand("EXEC spTranDataTenagaAhli @GuidHeader", paramIdHeader);
                }
            }
            catch (Exception ex)
            {
                strReturn = ex.Message;
            }
            return strReturn;
        }
        [AcceptVerbs("GET", "POST")]
        //[Route("api/TrxTenagaAhliHeader/ExecTransDataTATT/{pGuidHeader}/{pstrIdRekanan}/{pstrXLSPointer}")]
        [Route("api/TrxTenagaAhliHeader/ExecTransDataTATT/{pGuidHeader}")]
        //public string TransDataTenagaAhliTidakTetap(Guid pGuidHeader, string pstrIdRekanan, string pstrXLSPointer = "kosong")
        public string TransDataTenagaAhliTidakTetap(Guid pGuidHeader)
        {
            string strReturn = "Executed OK";
            try
            {
                using (var context = new DB_SMARTEntities1())
                {
                    var paramIdHeader = new SqlParameter("@GuidHeader", pGuidHeader);
                    //var paramIdRekanan = new SqlParameter("@IdRekanan", pstrIdRekanan);
                    //var paramXLSPointer = new SqlParameter("@XLSPointer", pstrXLSPointer);
                    //var result = context.Database.ExecuteSqlCommand("EXEC spTranDataTenagaAhliTidakTetap @GuidHeader, @IdRekanan, @XLSPointer"
                    var result = context.Database.ExecuteSqlCommand("EXEC spTranDataTenagaAhliTidakTetap @GuidHeader", paramIdHeader);
                        //, paramIdHeader, paramIdRekanan, paramXLSPointer);
                }
            }
            catch (Exception ex)
            {
                strReturn = ex.Message;
            }
            return strReturn;
        }
        [AcceptVerbs("GET", "POST")]
        //[Route("api/TrxTenagaAhliHeader/ExecTransDataTP/{pGuidHeader}/{pstrIdRekanan}/{pstrXLSPointer}")]
        [Route("api/TrxTenagaAhliHeader/ExecTransDataTP/{pGuidHeader}")]
        //public string TransDataTenagaPendukung(Guid pGuidHeader, string pstrIdRekanan, string pstrXLSPointer = "kosong")
        public string TransDataTenagaPendukung(Guid pGuidHeader)
        {
            string strReturn = "Executed OK";
            try
            {
                using (var context = new DB_SMARTEntities1())
                {
                    var paramIdHeader = new SqlParameter("@GuidHeader", pGuidHeader);
                    //var paramIdRekanan = new SqlParameter("@IdRekanan", pstrIdRekanan);
                    //var paramXLSPointer = new SqlParameter("@XLSPointer", pstrXLSPointer);
                    //var result = context.Database.ExecuteSqlCommand("EXEC spTranDataTenagaPendukung @GuidHeader, @IdRekanan, @XLSPointer"
                    //    , paramIdHeader, paramIdRekanan, paramXLSPointer);
                    var result = context.Database.ExecuteSqlCommand("EXEC spTranDataTenagaPendukung @GuidHeader", paramIdHeader);
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
