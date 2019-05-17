using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System;
namespace APIService.Controllers
{
    public class TrxRegistrationRequestController : ApiController
    {
        private IDataAccessRepository<trxRegistrationRequest, int> _repository;
        private TrxRegistrationRequestRep _repReg;
        //Inject the DataAccessRepository using Construction Injection 
        public TrxRegistrationRequestController(IDataAccessRepository<trxRegistrationRequest, int> r, TrxRegistrationRequestRep repReg)
        {
            _repository = r;
            _repReg = repReg;
        }
        public IEnumerable<trxRegistrationRequest> Get()
        {
            return _repository.Get();
        }
        [Route("api/TrxRegistrationRequest/GetView")]
        public IEnumerable<vwRegistrationRequest> GetView()
        {
            return _repReg.GetView();
        }
        [Route("api/TrxRegistrationRequest/GetViewIsActive/{IsActive}")]
        public IEnumerable<vwRegistrationRequest> GetViewIsActive(byte IsActive)
        {
            return _repReg.GetViewIsActive(IsActive);
        }
        [Route("api/TrxRegistrationRequest/GetViewUserAdmin")]
        public IEnumerable<vwUserAdminPCP> GetViewUserAdmin()
        {
            return _repReg.GetViewUserAdmin();
        }
        [ResponseType(typeof(trxRegistrationRequest))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxRegistrationRequest))]
        public IHttpActionResult Post(trxRegistrationRequest myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }
        private string MaskPasskey(string strInput, bool IsMasked)
        {
            var strBuilder = new StringBuilder();
            int intCharNew = 0;
            foreach (char c in strInput)
            {
                if (IsMasked)
                {
                    intCharNew = ((int)c) + 5;
                }
                else
                {
                    intCharNew = ((int)c) - 5;
                }
                strBuilder.Append((char)intCharNew);
            }
            return strBuilder.ToString();
        }

        [HttpGet]
        [Route("api/TrxRegistrationRequest/ChangePasswordByUserName/{myUserName}/{myBarePassword}")]
        public IHttpActionResult ChangePasswordByUserName(string myUserName, string myBarePassword)
        {
            string BarePassword = MaskPasskey(myBarePassword, true);

            _repReg.SetPasswordByUserName(myUserName, BarePassword);
            return StatusCode(HttpStatusCode.NoContent);
        }
        public string Decode(string content)
        {
            var decodedBytes = HttpServerUtility.UrlTokenDecode(content);
            string externalId = UTF8Encoding.UTF8.GetString(decodedBytes);

            return externalId;
        }

        [AllowAnonymous]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/TrxRegistrationRequest/ApprovalWithReason/{myUserName}/{IsActive}/{Catatan}/{CreatedUser}")]
        public string ApprovalWithReason(string myUserName, byte IsActive, string Catatan, string CreatedUser)
        {
            string strReturn = "OK";
            Catatan = Decode(Catatan);
            try
            {
                using (var context = new DB_SMARTEntities1())
                {
                    var parUserName = new SqlParameter("@UserName", myUserName);
                    var parIsActive = new SqlParameter("@IsActive", IsActive);
                    var parCatatan = new SqlParameter("@Catatan", Catatan);
                    var parCreatedUser = new SqlParameter("@CreatedUser", CreatedUser);
                    var result = context.Database.ExecuteSqlCommand("EXEC spSetApplovalLog " +
                        "@UserName, @IsActive, @Catatan, @CreatedUser"
                        , parUserName, parIsActive, parCatatan, parCreatedUser);
                }
            }
            catch (Exception ex)
            {
                strReturn = ex.Message;
            }
            return strReturn;
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxRegistrationRequest myData)
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
    }
}
