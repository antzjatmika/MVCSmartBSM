using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;
using Omu.ValueInjecter;
using System.Text;
using System.Web;

namespace APIService.Controllers
{
    public class TrxManagementController : ApiController
    {
        private IDataAccessRepository<trxManagement, int> _repository;
        private TrxManagementRep _repManagement = new TrxManagementRep();
        private TrxManagementBLHistRep _repManagementBLHist = new TrxManagementBLHistRep();
        //Inject the DataAccessRepository using Construction Injection 
        public TrxManagementController(IDataAccessRepository<trxManagement, int> r, TrxManagementRep repManagement
            , TrxManagementBLHistRep repManagementBLHistory)
        {
            _repository = r;
            _repManagement = repManagement;
            _repManagementBLHist = repManagementBLHistory;
        }
        public IEnumerable<trxManagement> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxManagement))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxManagement))]
        public IHttpActionResult Post(trxManagement myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxManagement myData)
        {
            _repository.Put(id, myData);
            return StatusCode(HttpStatusCode.NoContent);
        }
        [HttpGet]
        [Route("api/TrxManagement/DeleteManagementByRek/{IdData}")]
        public IHttpActionResult DeleteManagementByRek(int IdData)
        {
            //UPDATE DI trxManagement
            trxManagement myData = _repository.Get(IdData);
            myData.IsActive = false;
            myData.Catatan = "DELETE BY REKANAN";
            _repository.Put(IdData, myData);
            //INSERT DI trxManagement Blacklist
            trxManagementBlackListHistory ManagemenBLHist = new trxManagementBlackListHistory()
            {
                IdManagemen = myData.IdManagemen,
                Name = myData.Name,
                NomorKTP = myData.NomorKTP,
                NomorNPWP = myData.NomorNPWP,
                NomorProfesi = myData.NomorIAPI,
                ImageBaseName = myData.ImageBaseName,

                FileExtKTP = myData.FileExtKTP,
                FileExtNPWP = myData.FileExtNPWP,
                FileExtProfesi = myData.FileExtIAPI,

                TanggalMulaiBlackList = System.DateTime.Now,
                TanggalAkhirBlacklist = System.DateTime.Now,
                StatusBlackList = true,
                Catatan = "DELETE BY REKANAN",
                CreatedUser = "admin",
                CreatedDate = System.DateTime.Now
            };
            _repManagementBLHist.Post(ManagemenBLHist);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(int id)
        {
            _repository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("api/TrxManagement/GetByRekanan/{idRekanan}")]
        public IEnumerable<trxManagement> GetByRekanan(System.Guid idRekanan)
        {
            IEnumerable<trxManagement> ManagementRekanan;
            ManagementRekanan = _repManagement.GetByRekanan(idRekanan);
            return ManagementRekanan;
        }
        [Route("api/TrxManagement/GetByRekananActive/{idRekanan}")]
        public IEnumerable<trxManagement> GetByRekananActive(System.Guid idRekanan)
        {
            IEnumerable<trxManagement> ManagementRekanan;
            ManagementRekanan = _repManagement.GetByRekananActive(idRekanan);
            return ManagementRekanan;
        }
        [Route("api/TrxManagement/GetRekManagemenBL")]
        public IEnumerable<vwRekManagemenBL> GetRekManagemenBL()
        {
            IEnumerable<vwRekManagemenBL> LstRekManagemenBL;
            LstRekManagemenBL = _repManagement.GetRekManagemenBL();
            return LstRekManagemenBL;
        }
        [Route("api/TrxManagement/GetManagemenBL")]
        public IEnumerable<trxManagement> GetManagemenBL()
        {
            IEnumerable<trxManagement> LstManagemenBL;
            LstManagemenBL = _repManagement.GetManagemenBL();
            return LstManagemenBL;
        }
        public string Decode(string content)
        {
            var decodedBytes = HttpServerUtility.UrlTokenDecode(content);
            string externalId = UTF8Encoding.UTF8.GetString(decodedBytes);

            return externalId;
        }
        [AcceptVerbs("GET", "POST")]
        [Route("api/TrxManagement/BlacklistPartnerById/{IdManagemen}/{StatusBlacklist}/{myCatatan}/{AkhirBlacklist}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DoBlacklistById(int IdManagemen, int StatusBlacklist, string myCatatan, DateTime AkhirBlacklist)
        {
            //update TrxManagement
            myCatatan = Decode(myCatatan);
            trxManagement ManagementUPD = _repository.Get(IdManagemen);
            //ManagementUPD.IsActive = (StatusBlacklist == 1)? false: true;
            ManagementUPD.StatusBlackList = Convert.ToBoolean(StatusBlacklist);
            ManagementUPD.Catatan = myCatatan;
            ManagementUPD.TanggalAkhirBlacklist = AkhirBlacklist;
            _repository.Put(IdManagemen, ManagementUPD);

            //INSERT HISTORY
            trxManagementBlackListHistory ManagemenBLHist = new trxManagementBlackListHistory()
            {
                IdManagemen = ManagementUPD.IdManagemen,
                Name = ManagementUPD.Name,
                NomorKTP = ManagementUPD.NomorKTP,
                NomorNPWP = ManagementUPD.NomorNPWP,
                NomorProfesi = ManagementUPD.NomorIAPI,
                ImageBaseName = ManagementUPD.ImageBaseName,

                FileExtKTP = ManagementUPD.FileExtKTP,
                FileExtNPWP = ManagementUPD.FileExtNPWP,
                FileExtProfesi = ManagementUPD.FileExtIAPI,

                TanggalMulaiBlackList = System.DateTime.Now,
                TanggalAkhirBlacklist = AkhirBlacklist,
                StatusBlackList = Convert.ToBoolean(StatusBlacklist),
                Catatan = myCatatan,
                CreatedUser = "admin",
                CreatedDate = System.DateTime.Now
            };
            _repManagementBLHist.Post(ManagemenBLHist);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("api/TrxManagement/AddUpdateManagemenBL")]
        //[ResponseType(typeof(trxNotarisTabular))]
        [HttpPut, HttpPost]
        public IHttpActionResult AddUpdateManagemenBL(trxManagement myData)
        {
            trxManagement notTemp = new trxManagement();
            notTemp.InjectFrom(myData);

            if (myData.IdManagemen > 0)
            {
                _repManagement.PutPartner(notTemp.IdManagemen, notTemp);
            }
            else
            {
                _repManagement.PostPartner(notTemp);
            }
            return Ok(notTemp);
        }

    }
}
