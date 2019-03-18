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
    public class TrxDataOrganisasiController : ApiController
    {
        private IDataAccessRepository<trxDataOrganisasi, int> _repository;
        private TrxDataOrganisasiRep _repOrganisasi;
        //Inject the DataAccessRepository using Construction Injection 
        public TrxDataOrganisasiController(IDataAccessRepository<trxDataOrganisasi, int> r, TrxDataOrganisasiRep rOrganisasi)
        {
            _repository = r;
            _repOrganisasi = rOrganisasi;
        }
        public IEnumerable<trxDataOrganisasi> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxDataOrganisasiForm))]
        public IHttpActionResult Get(int id)
        {
            trxDataOrganisasi orgTemp = new trxDataOrganisasi();
            trxDataOrganisasiForm orgSingle = new trxDataOrganisasiForm();
            if (id > 0)
            {
                orgTemp = _repository.Get(id);
                orgSingle.InjectFrom(orgTemp);
            }
            return Ok(orgSingle); 
        }

        [ResponseType(typeof(trxDataOrganisasiForm))]
        public IHttpActionResult Post(trxDataOrganisasiForm myData)
        {
            trxDataOrganisasi orgTemp = new trxDataOrganisasi();
            orgTemp.InjectFrom(myData);
            _repository.Post(orgTemp);
            return Ok(orgTemp);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxDataOrganisasiForm myData)
        {
            trxDataOrganisasi orgTemp = new trxDataOrganisasi();
            orgTemp.InjectFrom(myData);
            _repository.Put(id, orgTemp);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(int id)
        {
            _repository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("api/TrxDataOrganisasi/GetByRekananId/{rekananId}")]
        public trxDataOrganisasiForm GetByRekananId(System.Guid rekananId)
        {
            trxDataOrganisasiForm OrgByRekananId = new trxDataOrganisasiForm();
            OrgByRekananId = _repOrganisasi.GetByRekananId(rekananId);
            return OrgByRekananId;
        }        
        [Route("api/TrxDataOrganisasi/GetDataOrganisasiByRek/{rekananId}")]
        public List<fDataOrganisasiByRek_Result> GetDataOrganisasiByRek(System.Guid rekananId)
        {
            List<fDataOrganisasiByRek_Result> myDataList = new List<fDataOrganisasiByRek_Result>();
            myDataList = _repOrganisasi.GetDataOrganisasiByRek(rekananId);
            return myDataList;
        }

         [Route("api/TrxDataOrganisasi/GetDataOrganisasiByRek_Type/{rekananId}/{IdTypeRekanan}")]
         public List<fDataOrganisasiByRek_Type_Result> GetDataOrganisasiByRek(System.Guid rekananId, int IdTypeRekanan)
         {
             //List<fDataOrganisasiByRek_Result> myDataList = new List<fDataOrganisasiByRek_Result>();
             List<fDataOrganisasiByRek_Type_Result> myDataList = new List<fDataOrganisasiByRek_Type_Result>();
             myDataList = _repOrganisasi.GetDataOrganisasiByRek_Type(rekananId, IdTypeRekanan);
             return myDataList;
         }
         //[AcceptVerbs("GET", "POST")]
         //[Route("api/TrxDataOrganisasi/ExecTransDataTA/{pintIdHeader}/{pstrIdRekanan}/{pstrXLSPointer}")]
         //public string TransDataTenagaAhli(int pintIdHeader, string pstrIdRekanan, string pstrXLSPointer = "kosong")
         //{
         //    string strReturn = "Executed OK";
         //    try
         //    {
         //        using (var context = new DB_SMARTEntities())
         //        {
         //            var paramIdHeader = new SqlParameter("@IdHeader", pintIdHeader);
         //            var paramIdRekanan = new SqlParameter("@IdRekanan", pstrIdRekanan);
         //            var paramXLSPointer = new SqlParameter("@XLSPointer", pstrXLSPointer);
         //            var result = context.Database.ExecuteSqlCommand("EXEC spTranDataTenagaAhli @IdHeader, @IdRekanan, @XLSPointer"
         //                , paramIdHeader, paramIdRekanan, paramXLSPointer);
         //        }
         //    }
         //    catch(Exception ex)
         //    {
         //        strReturn = ex.Message;
         //    }
         //    return strReturn; 
         //}

    }
}
