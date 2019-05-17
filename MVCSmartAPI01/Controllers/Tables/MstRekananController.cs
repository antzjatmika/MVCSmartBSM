using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;
using System.Collections;
using log4net;
using Omu.ValueInjecter;
using DevExpress.Data.Filtering;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Net.Http;
using LocalAccountsApp.Controllers;
using MVCSmartAPI01;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace APIService.Controllers
{
    public class MstRekananController : ApiController
    {
        private IDataAccessRepository<mstRekanan, System.Guid> _repository;
        private MstTypeOfRegionRep _repRegion;
        private MstTypeOfRekananRep _repTypeOfRekanan;
        private MstTypeOfBadanUsahaRep _repTypeOfBadanUsaha;
        private MstWilayahRep _repWilayah;
        private MstKecamatanRep _repKecamatan;
        private MstRekananRep _repRekanan;
        private MstContactRep _repContact;
        private mstEmailPoolRep _repEmailPool;
        private ILog _auditer = LogManager.GetLogger("Audit");
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                try
                {
                    _userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                }
                catch (Exception ex)
                {
                    string ss = ex.Message;
                }

                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //Inject the DataAccessRepository using Construction Injection 
        public MstRekananController(IDataAccessRepository<mstRekanan, System.Guid> r, MstTypeOfRegionRep rRegion
            , MstTypeOfRekananRep rTRekanan, MstTypeOfBadanUsahaRep rTBadan, MstWilayahRep rTWilayah, MstKecamatanRep rTKecamatan
            , MstRekananRep rRekanan, MstContactRep rContact, mstEmailPoolRep repEmailPool, TrxRegistrationRequestRep repRegRequest)
        {
            _repository = r;
            _repRegion = rRegion;
            _repTypeOfRekanan = rTRekanan;
            _repTypeOfBadanUsaha = rTBadan;
            _repWilayah = rTWilayah;
            _repKecamatan = rTKecamatan;
            _repRekanan = rRekanan;
            _repContact = rContact;
            _repEmailPool = repEmailPool;
        }
        public mstRekananMulti Get()
        {
            mstRekananMulti RekananBySupervisorId = new mstRekananMulti();
            LogicalThreadContext.Properties["UserName"] = User.Identity.Name;
            LogicalThreadContext.Properties["ActionType"] = "GetRekanan";
            _auditer.Info("Display List Rekanan");
            //re-populate MstRegion
            var myRegionColls = _repRegion.GetActive();
            RekananBySupervisorId.TypeOfRegionColls = myRegionColls;

            //re-populate MstTypeOfRekanan
            var myTypeOfRekananColls = _repTypeOfRekanan.GetActive();
            RekananBySupervisorId.TypeOfRekananColls = myTypeOfRekananColls;

            //re-populate MstTypeOfBadan
            var myTypeOfBadanColls = _repTypeOfBadanUsaha.GetActive();
            RekananBySupervisorId.TypeOfBadanUsahaColls = myTypeOfBadanColls;

            //re-populate MstWilayah
            //var myWilayahColls = _repWilayah.GetActive();
            //RekananBySupervisorId.WilayahColls = myWilayahColls;

            //re-populate MstKecamatan
            //var myKecamatanColls = _repKecamatan.GetActive();
            //RekananBySupervisorId.KecamatanColls = myKecamatanColls;

            //populate rekanan
            RekananBySupervisorId.MstRekananMulti = _repository.Get();
            return RekananBySupervisorId;
        }

        [ResponseType(typeof(mstRekananForm))]
        public IHttpActionResult Get(System.Guid id)
        {
            mstRekanan rekTemp = new mstRekanan();
            mstRekananForm RekananSingle = new mstRekananForm();
            if(id != System.Guid.Empty)
            {
                rekTemp = _repository.Get(id);
                //RekananSingle
                RekananSingle.InjectFrom(rekTemp);
            }
            //re-populate MstRegion
            var myRegionColls = _repRegion.GetActive();
            RekananSingle.TypeOfRegionColls = myRegionColls;

            //re-populate MstTypeOfRekanan
            var myTypeOfRekananColls = _repTypeOfRekanan.GetActive();
            RekananSingle.TypeOfRekananColls = myTypeOfRekananColls;

            //re-populate MstTypeOfBadan
            var myTypeOfBadanColls = _repTypeOfBadanUsaha.GetActive();
            RekananSingle.TypeOfBadanUsahaColls = myTypeOfBadanColls;

            //re-populate MstWilayah
            var myWilayahColls = _repWilayah.GetActive();
            RekananSingle.WilayahColls = myWilayahColls;

            RekananSingle.ClassAB = new List<SimpleRef>() { new SimpleRef { RefId = 1, RefDescription = "A" }, new SimpleRef { RefId = 2, RefDescription = "B" } };
            RekananSingle.ClassABC = new List<SimpleRef>() { new SimpleRef { RefId = 1, RefDescription = "A" }, new SimpleRef { RefId = 2, RefDescription = "B" }, new SimpleRef { RefId = 3, RefDescription = "C" } };

            return Ok(RekananSingle);
        }

        [ResponseType(typeof(mstRekananForm))]
        public IHttpActionResult Post(mstRekananForm myData)
        {
            //membuat data rekanan yang baru, akan meng-insert data contact
            mstContact contactTemp = new mstContact();
            contactTemp.UserId = myData.CreatedUser;
            contactTemp.IdRekanan = myData.IdRekanan;
            contactTemp.Name = myData.UserName;
            contactTemp.Title = string.Empty;
            contactTemp.CreatedUser = myData.CreatedUser;
            contactTemp.CreatedDate = DateTime.Today;
            contactTemp.IsActive = true;
            _repContact.Post(contactTemp);

            mstRekanan rekTemp = new mstRekanan();

            rekTemp.InjectFrom(myData);
            rekTemp.LMDate = DateTime.Today;
            _repository.Post(rekTemp);
            return Ok(rekTemp);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(System.Guid id, mstRekananForm myData)
        {
            mstRekanan rekTemp = new mstRekanan();
            rekTemp.InjectFrom(myData);
            rekTemp.Note = "Update: Profile";
            rekTemp.LMDate = DateTime.Today;
            _repository.Put(id, rekTemp);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(System.Guid id)
        {
            _repository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("api/MstRekanan/GetBySupervisorId/{supervisorId}")]
        public mstRekananMulti GetBySupervisorId(int supervisorId)
        {
            mstRekananMulti RekananBySupervisorId = new mstRekananMulti();
            LogicalThreadContext.Properties["UserName"] = User.Identity.Name;
            LogicalThreadContext.Properties["ActionType"] = "GetRekanan";
            _auditer.Info("Display List Rekanan By SupervisorId");
            //re-populate MstRegion
            var myRegionColls = _repRegion.GetActive();
            RekananBySupervisorId.TypeOfRegionColls = myRegionColls;

            //re-populate MstTypeOfRekanan
            var myTypeOfRekananColls = _repTypeOfRekanan.GetActive();
            RekananBySupervisorId.TypeOfRekananColls = myTypeOfRekananColls;

            //re-populate MstTypeOfBadan
            var myTypeOfBadanColls = _repTypeOfBadanUsaha.GetActive();
            RekananBySupervisorId.TypeOfBadanUsahaColls = myTypeOfBadanColls;

            //re-populate MstWilayah
            //var myWilayahColls = _repWilayah.GetActive();
            //RekananBySupervisorId.WilayahColls = myWilayahColls;

            //re-populate MstKecamatan
            //var myKecamatanColls = _repKecamatan.GetActive();
            //RekananBySupervisorId.KecamatanColls = myKecamatanColls;

            var rekananCollection = _repRekanan.GetBySupervisorId(supervisorId);
            RekananBySupervisorId.MstRekananExtMulti = rekananCollection;

            RekananBySupervisorId.ClassAll = new List<SimpleRef>() { 
                new SimpleRef { RefId = 0, RefDescription = "n/a" }
                , new SimpleRef { RefId = 1, RefDescription = "A" }
                , new SimpleRef { RefId = 2, RefDescription = "B" }
                , new SimpleRef { RefId = 3, RefDescription = "C" } };

            return RekananBySupervisorId;
        }

        [Route("api/MstRekanan/GetManagementRekanan")]
        public IEnumerable<fManagementRekanan_Result> GetManagementRekanan()
        {
            //List<fManagementRekanan_Result> lstManagemenRekRest = new List<fManagementRekanan_Result>();
            LogicalThreadContext.Properties["UserName"] = User.Identity.Name;
            LogicalThreadContext.Properties["ActionType"] = "GetManagemenRekanan";
            _auditer.Info("Display List Management Rekanan");
            var lstManagemenRekRest = _repRekanan.GetManagementRekanan();
            return lstManagemenRekRest;
        }

        [Route("api/MstRekanan/GetManagementP2PK/{idTypeOfRekanan}/{isActive}")]
        public IEnumerable<trxManagementP2PK> GetManagementP2PK(int idTypeOfRekanan, bool isActive)
        {
            LogicalThreadContext.Properties["UserName"] = User.Identity.Name;
            LogicalThreadContext.Properties["ActionType"] = "GetManagementP2PK";
            _auditer.Info("Display List Management P2PK");
            var lstManagemenRekRest = _repRekanan.GetManagementP2PK(idTypeOfRekanan, isActive);
            return lstManagemenRekRest;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/MstRekanan/XLS_RekByIdSupervisor/{supervisorId}/{strFilterExpression}")]
        public IEnumerable<fXLS_RekByIdSupervisor_Result> XLS_RekByIdSupervisor(int supervisorId, string strFilterExpression)
        {
            string strFilterExp = string.Empty;
            LogicalThreadContext.Properties["UserName"] = User.Identity.Name;
            LogicalThreadContext.Properties["ActionType"] = "GetRekanan";
            _auditer.Info("Display List Rekanan By SupervisorId");

            if(!string.IsNullOrEmpty(strFilterExpression))
            {
                strFilterExp = CustomCriteriaToLinqWhereParser.Process(CriteriaOperator.Parse(strFilterExpression) as CriteriaOperator);
            }
            var rekananCollection = _repRekanan.XLS_RekByIdSupervisor(supervisorId, strFilterExp);
            return rekananCollection;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/MstRekanan/XLS_ManagementRekanan/{strFilterExpression}")]
        public IEnumerable<fManagementRekanan_Result> XLS_ManagementRekanan(string strFilterExpression)
        {
            string strFilterExp = string.Empty;
            LogicalThreadContext.Properties["UserName"] = User.Identity.Name;
            LogicalThreadContext.Properties["ActionType"] = "GetRekanan";
            _auditer.Info("Display List Management Rekanan");

            if (!string.IsNullOrEmpty(strFilterExpression))
            {
                strFilterExp = CustomCriteriaToLinqWhereParser.Process(CriteriaOperator.Parse(strFilterExpression) as CriteriaOperator);
            }
            var rekananCollection = _repRekanan.XLS_ManagementRekanan(strFilterExp);
            return rekananCollection;
        }

        [Route("api/MstRekanan/GetInfoLastModifiedByRek/{IdRekanan}")]
        public IEnumerable<fInfoLastModifiedByRek_Result> GetInfoLastModifiedByRek(Guid IdRekanan)
        {
            //List<fInfoLastModifiedByRek_Result> lstInfo = new List<fInfoLastModifiedByRek_Result>();
            var lstInfo = _repRekanan.GetInfoLastModifiedByRek(IdRekanan);
            return lstInfo;
        }
        [Route("api/MstRekanan/GetInfo2LastModifiedByRek/{IdRekanan}")]
        public IEnumerable<fInfo2LastModifiedByRek_Result> GetInfo2LastModifiedByRek(Guid IdRekanan)
        {
            var lstInfo = _repRekanan.GetInfo2LastModifiedByRek(IdRekanan);
            return lstInfo;
        }
        [AllowAnonymous]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        //[Route("api/MstRekanan/RegisterCalonRekanan/{strNamaRekanan}/{strNomorNPWP}/{strEmail}")]
        public string RegisterCalonRekanan1(string strNamaRekanan, string strNomorNPWP, string strEmail)
        {
            //cek master rekanan, jika sudah ada kirim informasi sudah terdaftar (berdasarkan NPWP)
            //jika belum ada maka cek periode pendaftaran 
            // - dalam masa pendaftaran maka kirim username dan password 
            // - tidak dalam masa pendaftaran maka kirim informasi bahwa pendaftaran blom dimulai
            string strNamaRekananFound = _repRekanan.CheckRekananByNPWP(strNomorNPWP);

            //var user = new ApplicationUser() { UserName = strEmail, Email = strEmail };
            //var result = UserManager.Create(user).Result;
            //IdentityResult result = UserManager.Create(user, "");
            //if (!result.Succeeded)
            //{
            //    return string.Empty;
            //}

            return string.Empty;
        }


        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        //[AllowAnonymous]
        //[System.Web.Http.AcceptVerbs("GET", "POST")]
        //[System.Web.Http.HttpGet]
        //[Route("api/MstRekanan/RegisterCalonRekanan/{strNamaRekanan}/{strNomorNPWP}/{strEmail}")]
        //public async Task<IHttpActionResult> RegisterCalonRekanan(string strNamaRekanan, string strNomorNPWP, string strEmail)
        //{
        //    string strPassword = "12345abcde";
        //    string strNamaRekananFound = _repRekanan.CheckRekananByNPWP(strNomorNPWP);
        //    var user = new ApplicationUser() { UserName = strEmail, Email = strEmail };
        //    try
        //    {
        //        IdentityResult result = await UserManager.CreateAsync(user, strPassword);
        //        if (!result.Succeeded)
        //        {
        //            return GetErrorResult(result);
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        string strErr = ex.Message;
        //    }

        //    return Ok();
        //}


        [AllowAnonymous]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/MstRekanan/RegisterCalonRekanan")]
        public string RegisterCalonRekanan(RegisterBindingModel model)
        {
            AspNetUser myAspNetUser = _repContact.GetAspNetUserByEmail(model.Email);
            Guid myIdRekanan = new Guid(model.NomorNPWP);
            Guid myUserId = new Guid(myAspNetUser.Id);

            string strReturn = "Executed OK";

            String myIdRekananS = Guid.NewGuid().ToString();
            String myUserIdS = Guid.NewGuid().ToString();

            try
            {
                using (var context = new DB_SMARTEntities1())
                {
                    var parIdTypeOfRekanan = new SqlParameter("@IdTypeOfRekanan", model.IdTypeOfRekanan);
                    var parIdRekanan = new SqlParameter("@IdRekanan", myIdRekanan);
                    var parNamaRekanan = new SqlParameter("@NamaRekanan", model.NamaRekanan);
                    var parAlamatEmail = new SqlParameter("@AlamatEmail", model.Email);
                    var parUserId = new SqlParameter("@UserId", myUserId);
                    var parBarePasskey = new SqlParameter("@BarePasskey", model.BarePassword);
                    var parIsActive = new SqlParameter("@IsActive", model.IsActive);
                    var result = context.Database.ExecuteSqlCommand("EXEC spRegisterCalonRekanan " +
                        "@IdTypeOfRekanan,@IdRekanan,@NamaRekanan,@AlamatEmail,@UserId,@BarePasskey,@IsActive"
                        , parIdTypeOfRekanan, parIdRekanan, parNamaRekanan, parAlamatEmail, parUserId, parBarePasskey, parIsActive);
                }
            }
            catch (Exception ex)
            {
                strReturn = ex.Message;
            }
            return strReturn;
        }

        [Route("api/MstRekanan/GetPengumumanByTypeOfRekanan/{IdTypeOfRekanan}")]
        public fGetPengumumanByTypeOfRekanan_Result GetPengumumanByTypeOfRekanan(int IdTypeOfRekanan)
        {
            var dataPengumuman = _repRekanan.GetPengumumanByTypeOfRekanan(IdTypeOfRekanan);
            return dataPengumuman;
        }

        
    }
}
