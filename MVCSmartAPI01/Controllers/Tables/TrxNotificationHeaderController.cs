using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;
using System.Data.SqlClient;

namespace APIService.Controllers
{
    public class TrxNotificationHeaderController : ApiController
    {
        private IDataAccessRepository<trxNotificationHeader, int> _repository;
        private TrxNotificationHeaderRep _repHeader;
        private TrxNotificationDetailRep _repDetail;
        private TrxNotificationContentRep _repContent;
        private MstRekananRep _repRekanan;
        
        //Inject the DataAccessRepository using Construction Injection 
        public TrxNotificationHeaderController(IDataAccessRepository<trxNotificationHeader, int> r, TrxNotificationHeaderRep repHeader
            , MstRekananRep repRekanan, TrxNotificationDetailRep repDetail, TrxNotificationContentRep repContent)
        {
            _repository = r;
            _repHeader = repHeader;
            _repDetail = repDetail;
            _repContent = repContent;
            _repRekanan = repRekanan;
        }
        public IEnumerable<trxNotificationHeader> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxNotificationHeader))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        //[ResponseType(typeof(trxNotificationHeader))]
        //public IHttpActionResult Post(trxNotificationHeader myData)
        //{
        //    _repository.Post(myData);
        //    return Ok(myData);
        //}

        //[ResponseType(typeof(void))]
        //public IHttpActionResult Put(int id, trxNotificationHeader myData)
        //{
        //    _repository.Put(id, myData);
        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        private string PopulateRecipient(int IdNotification, string ArrRecipient, Guid CreatedUser, bool FlagKirim, bool IsSaluranEmail, bool IsSaluranWhatsapp)
        {
            string strReturn = "Executed OK";
            string strRecipientClean = string.Empty;
            DateTime dtmSentDate = new DateTime(1900, 1, 1);

            if (ArrRecipient.Length > 5)
            {
                if (FlagKirim)
                {
                    dtmSentDate = DateTime.Today;
                }
                strRecipientClean = ArrRecipient.Replace("[\"", "").Replace("\"", "").Replace("\"]", "").Replace("[","").Replace("]","");
                try
                {
                    using (var context = new DB_SMARTEntities1())
                    {
                        var paramIdNotification = new SqlParameter("@IdNotification", IdNotification);
                        var paramArrRecipient = new SqlParameter("@ArrRecipient", strRecipientClean);
                        var paramCreatedUser = new SqlParameter("@CreatedUser", CreatedUser);
                        var paramFlagKirim = new SqlParameter("@FlagKirim", FlagKirim);
                        var paramSentDate = new SqlParameter("@SentDate", dtmSentDate);
                        var paramIsSaluranEmail = new SqlParameter("@FlagSaluranEmail", IsSaluranEmail);
                        var paramIsSaluranWhatsapp = new SqlParameter("@FlagSaluranWhatsapp", IsSaluranWhatsapp);
                        var result = context.Database.ExecuteSqlCommand("EXEC spPopulateRecipient @IdNotification, @ArrRecipient, @CreatedUser, @FlagKirim, @SentDate, @FlagSaluranEmail, @FlagSaluranWhatsapp"
                            , paramIdNotification, paramArrRecipient, paramCreatedUser, paramFlagKirim, paramSentDate, paramIsSaluranEmail, paramIsSaluranWhatsapp);
                    }
                }
                catch (Exception ex)
                {
                    strReturn = ex.Message;
                }
            }
            return strReturn;
        }

        private void UpdateInsertDraft(AddEditTDraftCaptionForm myDataForm)
        {
            if(myDataForm.IdNotification < 0)
            {
                int intNewIdNotification = 0;
                //  insert noti header
                //tambah baru header
                trxNotificationHeader myDataHeader = new trxNotificationHeader();
                myDataHeader.IdTipeNotification = 0;
                myDataHeader.IdLevelUrgensi = myDataForm.IdLevelUrgensi;
                myDataHeader.CaptionInfo = myDataForm.CaptionInfo;
                myDataHeader.IdDataProfilePart = 0;
                myDataHeader.flgValidity = true;
                //myDataHeader.CreatedUser = myDataForm.SubjectFromStr;
                myDataHeader.CreatedUser = myDataForm.SubjectFrom.ToString();
                myDataHeader.CreatedDate = DateTime.Today;
                intNewIdNotification = _repHeader.PostNew(myDataHeader); //RETURN IdNotification diperlukan untuk Next

                //  insert noti content
                trxNotificationContent myDataContent = new trxNotificationContent();
                myDataContent.IdNotification = intNewIdNotification;
                myDataContent.BodyContent = myDataForm.BodyContent;
                myDataContent.CreatedUser = myDataForm.SubjectFromStr;
                myDataContent.CreatedDate = DateTime.Today;
                myDataContent.Attachment = myDataForm.Attachment;
                myDataContent.FileExt1 = myDataForm.FileExt1;
                myDataContent.FileExt2 = myDataForm.FileExt2; 
                myDataContent.FileExt3 = myDataForm.FileExt3;
                _repContent.Post(myDataContent);

                if (myDataForm.IsRekanan)
                {
                    trxNotificationDetail myDataDetail = new trxNotificationDetail();
                    myDataDetail.IdNotification = intNewIdNotification;
                    myDataDetail.SubjectTo = myDataForm.SubjectTo;
                    myDataDetail.SubjectFrom = myDataForm.SubjectFrom;
                    if (myDataForm.FlagKirim)
                    {
                        myDataDetail.flgTraySent = true;
                        myDataDetail.TSentDate = DateTime.Today;
                    }
                    else
                    {
                        myDataDetail.flgTraySent = false;
                    }
                    myDataDetail.flgTrayRead = false;
                    myDataDetail.CreatedUser = myDataForm.SubjectFrom.ToString();
                    myDataDetail.CreatedDate = DateTime.Today;
                    _repDetail.Post(myDataDetail);
                }
                else
                {
                    //  insert noti recipient
                    PopulateRecipient(intNewIdNotification, myDataForm.SubjectToStr, myDataForm.SubjectFrom, myDataForm.FlagKirim, myDataForm.IsSaluranEmail, myDataForm.IsSaluranWhatsapp);
                }
            }
            else
            {
                //update noti header
                trxNotificationHeader myDataHeader = new trxNotificationHeader();
                myDataHeader.IdNotification = myDataForm.IdNotification;
                myDataHeader.IdTipeNotification = 0;
                myDataHeader.IdLevelUrgensi = myDataForm.IdLevelUrgensi;
                myDataHeader.CaptionInfo = myDataForm.CaptionInfo;
                myDataHeader.IdDataProfilePart = 0;
                myDataHeader.flgValidity = true;
                myDataHeader.CreatedUser = myDataForm.SubjectFromStr;
                myDataHeader.CreatedDate = DateTime.Today;
                _repHeader.Put(myDataHeader.IdNotification, myDataHeader);

                //update noti content
                trxNotificationContent myDataContent = new trxNotificationContent();
                //myDataContent.IdNotificationContent = myDataForm.IdNotificationContent;
                myDataContent.IdNotification = myDataForm.IdNotification;
                myDataContent.BodyContent = myDataForm.BodyContent;
                myDataContent.CreatedUser = myDataForm.SubjectFromStr;
                myDataContent.CreatedDate = DateTime.Today;
                myDataContent.Attachment = myDataForm.Attachment;
                myDataContent.FileExt1 = myDataForm.FileExt1;
                myDataContent.FileExt2 = myDataForm.FileExt2;
                myDataContent.FileExt3 = myDataForm.FileExt3;
                _repContent.Put(myDataForm.IdNotificationContent, myDataContent);

                if (myDataForm.IsRekanan)
                {
                    trxNotificationDetail myDataDetail = new trxNotificationDetail();
                    myDataDetail.IdNotification = myDataForm.IdNotification;
                    myDataDetail.SubjectTo = myDataForm.SubjectTo;
                    myDataDetail.SubjectFrom = myDataForm.SubjectFrom;
                    if (myDataForm.FlagKirim)
                    {
                        myDataDetail.flgTraySent = true;
                        myDataDetail.TSentDate = DateTime.Today;
                    }
                    else
                    {
                        myDataDetail.flgTraySent = false;
                    }
                    myDataDetail.flgTrayRead = false;
                    myDataDetail.CreatedUser = myDataForm.SubjectFrom.ToString();
                    myDataDetail.CreatedDate = DateTime.Today;
                    _repDetail.Put(myDataForm.IdNotificationDetail, myDataDetail);
                }
                else
                {
                    //  insert noti recipient
                    PopulateRecipient(myDataForm.IdNotification, myDataForm.SubjectToStr, myDataForm.SubjectFrom, myDataForm.FlagKirim, myDataForm.IsSaluranEmail, myDataForm.IsSaluranWhatsapp);
                }
            }
        }
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, AddEditTDraftCaptionForm myDataForm)
        {
            UpdateInsertDraft(myDataForm);
            //DRAFT
            //jika id = -1, tambah baru
            //  insert noti header
            //  insert noti content
            //  insert noti recipient
            //  insert noti attachment
            
            //jika id > 0, update yang sudah ada
            //  update noti header
            //  update noti content
            //  update noti recipient (delete - insert)
            //  update noti attachment

            //KIRIM
            //jika id = -1, tambah baru
            //  sda

            //jika id > 0, update yang sudah ada
            //  sda

            //  insert noti detail

            return Ok(myDataForm);
        }
        [ResponseType(typeof(AddEditTDraftCaptionForm))]
        public IHttpActionResult Post(AddEditTDraftCaptionForm myData)
        {

            return Ok(myData);
        }
        //[ResponseType(typeof(void))]
        //[Route("api/TrxNotificationHeader/PopulateMe/{id}/{myData}")]
        //public IHttpActionResult Put(int id, trxNotificationHeader myData)
        //{
        //    _repository.Put(id, myData);
        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(int id)
        {
            _repository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("api/TrxNotificationHeader/GetNotificationBySubject/{SubjectId}/{LevelUrgensi}")]
        public IEnumerable<VmNotiBySubject> GetNotificationBySubject(System.Guid SubjectId, int LevelUrgensi)
        {
            IEnumerable<VmNotiBySubject> NotifyBySujectColls = new List<VmNotiBySubject>();
            NotifyBySujectColls = _repHeader.GetNotificationBySubject(SubjectId, LevelUrgensi);
            return NotifyBySujectColls;
        }



        [Route("api/TrxNotificationHeader/GetTInboxCaptionBySubjectTo/{SubjectTo}")]
        public inboxCaptionForm GetTInboxCaptionBySubjectTo(System.Guid SubjectTo)
        {
            inboxCaptionForm InboxCaptionForm = new inboxCaptionForm();
            var InboxCaptionColls = _repHeader.GetTInboxCaptionBySubjectTo(SubjectTo);
            InboxCaptionForm.InboxCaptionColls = InboxCaptionColls;
            List<SimpleRef> LevelUrgensiList = new List<SimpleRef>();
            LevelUrgensiList.Add(new SimpleRef() { RefId = 1, RefDescription = "Kritikal" });
            LevelUrgensiList.Add(new SimpleRef() { RefId = 2, RefDescription = "Penting" });
            LevelUrgensiList.Add(new SimpleRef() { RefId = 3, RefDescription = "Normal" });
            InboxCaptionForm.LevelUrgensiColls = LevelUrgensiList;

            return InboxCaptionForm;
        }
        [Route("api/TrxNotificationHeader/GetTInboxDetailBySubjectTo/{SubjectTo}")]
        public IEnumerable<fGetTInboxDetailBySubjectTo_Result> GetTInboxDetailBySubjectTo(System.Guid SubjectTo)
        {
            IEnumerable<fGetTInboxDetailBySubjectTo_Result> NotifyBySujectColls = new List<fGetTInboxDetailBySubjectTo_Result>();
            NotifyBySujectColls = _repHeader.GetTInboxDetailBySubjectTo(SubjectTo);
            return NotifyBySujectColls;
        }
        [Route("api/TrxNotificationHeader/GetTDraftCaptionBySubjectFrom/{SubjectFrom}")]
        public draftCaptionForm GetTDraftCaptionBySubjectFrom(System.Guid SubjectFrom)
        {
            draftCaptionForm myDataForm = new draftCaptionForm();
            List<SimpleRef> LevelUrgensiList = new List<SimpleRef>();
            LevelUrgensiList.Add(new SimpleRef() { RefId = 1, RefDescription = "Kritikal" });
            LevelUrgensiList.Add(new SimpleRef() { RefId = 2, RefDescription = "Penting" });
            LevelUrgensiList.Add(new SimpleRef() { RefId = 3, RefDescription = "Normal" });

            var NotifyBySujectColls = _repHeader.GetTDraftCaptionBySubjectFrom(SubjectFrom.ToString());
            myDataForm.DraftCaptionColls = NotifyBySujectColls;
            myDataForm.LevelUrgensiColls = LevelUrgensiList;
            return myDataForm;
        }
        [Route("api/TrxNotificationHeader/GetTDraftDetailBySubjectFrom/{SubjectFrom}")]
        public draftDetailForm GetTDraftDetailBySubjectFrom(System.Guid SubjectFrom)
        {
            draftDetailForm DraftDetailForm = new draftDetailForm();
            List<SimpleRef> LevelUrgensiList = new List<SimpleRef>();
            LevelUrgensiList.Add(new SimpleRef() { RefId = 1, RefDescription = "Kritikal" });
            LevelUrgensiList.Add(new SimpleRef() { RefId = 2, RefDescription = "Penting" });
            LevelUrgensiList.Add(new SimpleRef() { RefId = 3, RefDescription = "Normal" });
            DraftDetailForm.LevelUrgensiColls = LevelUrgensiList;

            IEnumerable<fGetTDraftDetailBySubjectFrom_Result> NotifyBySujectColls = new List<fGetTDraftDetailBySubjectFrom_Result>();
            DraftDetailForm.DraftDetailColls = _repHeader.GetTDraftDetailBySubjectFrom(SubjectFrom);

            return DraftDetailForm;
        }
        [Route("api/TrxNotificationHeader/GetTSentCaptionBySubjectFrom/{SubjectFrom}/{IdTipeSaluran}")]
        public sentCaptionForm GetTSentCaptionBySubjectFrom(System.Guid SubjectFrom, int IdTipeSaluran)
        {
            sentCaptionForm SentCaptionForm = new sentCaptionForm();
            List<SimpleRef> LevelUrgensiList = new List<SimpleRef>();
            LevelUrgensiList.Add(new SimpleRef() { RefId = 1, RefDescription = "Kritikal" });
            LevelUrgensiList.Add(new SimpleRef() { RefId = 2, RefDescription = "Penting" });
            LevelUrgensiList.Add(new SimpleRef() { RefId = 3, RefDescription = "Normal" });

            var SentCaptionColls = _repHeader.GetTSentCaptionBySubjectFrom(SubjectFrom, IdTipeSaluran);
            SentCaptionForm.SentCaptionColls = SentCaptionColls;
            SentCaptionForm.LevelUrgensiColls = LevelUrgensiList;

            return SentCaptionForm;
        }
        [Route("api/TrxNotificationHeader/GetTSentDetailBySubjectFrom/{SubjectFrom}")]
        public sentDetailForm GetTSentDetailBySubjectFrom(System.Guid SubjectFrom)
        {
            sentDetailForm SentDetailForm = new sentDetailForm();
            List<SimpleRef> LevelUrgensiList = new List<SimpleRef>();
            LevelUrgensiList.Add(new SimpleRef() { RefId = 1, RefDescription = "Kritikal" });
            LevelUrgensiList.Add(new SimpleRef() { RefId = 2, RefDescription = "Penting" });
            LevelUrgensiList.Add(new SimpleRef() { RefId = 3, RefDescription = "Normal" });
            SentDetailForm.LevelUrgensiColls = LevelUrgensiList;
            IEnumerable<fGetTSentDetailBySubjectFrom_Result> NotifyBySujectColls = new List<fGetTSentDetailBySubjectFrom_Result>();
            SentDetailForm.SentDetailColls = _repHeader.GetTSentDetailBySubjectFrom(SubjectFrom);
            return SentDetailForm;
        }
        [Route("api/TrxNotificationHeader/GetDraftCaptionForm/{IdNotification}/{SupervisorId}/{SubjectFrom}/{UserName}")]
        public AddEditTDraftCaptionForm GetDraftCaptionForm(int IdNotification, int SupervisorId, Guid SubjectFrom, string UserName)
        {
            AddEditTDraftCaptionForm myDataForm = new AddEditTDraftCaptionForm();

            IEnumerable<fGetRekananByIdSupervisor_Result> myRekananList = new List<fGetRekananByIdSupervisor_Result>();
            fGetSupervisorByRek_Result myDataSupervisor = new fGetSupervisorByRek_Result();

            if (SupervisorId > 0) //penerima adalah beberapa rekanan, pengirim adalah admin pcp
            {
                myRekananList = _repRekanan.GetBySupervisorId(SupervisorId);
                myDataForm.SubjectToStr = "(daftar rekanan)";
            }
            else //penerima adalah admin pcp, pengirim adalah rekanan
            {
                myDataSupervisor = _repRekanan.GetSupervisorByRek(SubjectFrom);
                myDataForm.SubjectTo = myDataSupervisor.IdRekanan;
                myDataForm.SubjectToStr = myDataSupervisor.UserName;
            }

            myDataForm.SubjectFrom = SubjectFrom;
            myDataForm.SubjectFromStr = UserName;

            List<SimpleRef> LevelUrgensiList = new List<SimpleRef>();
            myDataForm.IdLevelUrgensi = 3; //default normal
            LevelUrgensiList.Add(new SimpleRef() { RefId = 1, RefDescription = "Kritikal" });
            LevelUrgensiList.Add(new SimpleRef() { RefId = 2, RefDescription = "Penting" });
            LevelUrgensiList.Add(new SimpleRef() { RefId = 3, RefDescription = "Normal" });

            if (IdNotification > 0)
            {
                trxNotificationHeader myDataHeader = _repHeader.Get(IdNotification);
                myDataForm.CaptionInfo = myDataHeader.CaptionInfo;
                myDataForm.IdTipeNotification = myDataHeader.IdTipeNotification;
                myDataForm.IdLevelUrgensi = myDataHeader.IdLevelUrgensi;

                trxNotificationContent myDataContent = _repContent.GetByIdNotification(IdNotification);
                myDataForm.IdNotificationContent = myDataContent.IdNotificationContent;
                myDataForm.BodyContent = myDataContent.BodyContent;
                myDataForm.Attachment = myDataContent.Attachment;
                myDataForm.FileExt1 = myDataContent.FileExt1;
                myDataForm.FileExt2 = myDataContent.FileExt2;
                myDataForm.FileExt3 = myDataContent.FileExt3;

                trxNotificationDetail myDataDetail = _repDetail.GetByIdNotification(IdNotification);
                myDataForm.IdNotificationDetail = myDataDetail.IdNotificationDetail;
            }
            myDataForm.SubjectToColls = myRekananList;
            myDataForm.LevelUrgensiColls = LevelUrgensiList;
            return myDataForm;
        }

        [Route("api/TrxNotificationHeader/GetNotiByIdNotification/{IdNotification}")]
        public IEnumerable<fGetNotiByIdNotification_Result> GetNotiByIdNotification(int IdNotification)
        {
            IEnumerable<fGetNotiByIdNotification_Result> myDataList = new List<fGetNotiByIdNotification_Result>();
            myDataList = _repHeader.GetNotiByIdNotification(IdNotification);
            return myDataList;
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/TrxNotificationHeader/ReadInfoByIdNotification/{IdNotificationDetail}/{FlgTraySent}")]
        public readInfoByIdNotiForm ReadInfoByIdNotification(int IdNotificationDetail, bool FlgTraySent)
        {
            readInfoByIdNotiForm myData = new readInfoByIdNotiForm();
            var myNoti = _repHeader.ReadInfoByIdNotification(IdNotificationDetail, FlgTraySent);
            myData.ReadInfoByNoti = myNoti;

            List<SimpleRef> LevelUrgensiList = new List<SimpleRef>();
            LevelUrgensiList.Add(new SimpleRef() { RefId = 1, RefDescription = "Kritikal" });
            LevelUrgensiList.Add(new SimpleRef() { RefId = 2, RefDescription = "Penting" });
            LevelUrgensiList.Add(new SimpleRef() { RefId = 3, RefDescription = "Normal" });
            myData.LevelUrgensiColls = LevelUrgensiList;

            return myData;
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/TrxNotificationHeader/ReadInfoByIdNotificationHead/{IdNotification}")]
        public readInfoByIdNotiForm ReadInfoByIdNotificationHead(int IdNotification)
        {
            readInfoByIdNotiForm myData = new readInfoByIdNotiForm();
            var myNoti = _repHeader.ReadInfoByIdNotificationHead(IdNotification);
            myData.ReadInfoByNoti = myNoti;

            List<SimpleRef> LevelUrgensiList = new List<SimpleRef>();
            LevelUrgensiList.Add(new SimpleRef() { RefId = 1, RefDescription = "Kritikal" });
            LevelUrgensiList.Add(new SimpleRef() { RefId = 2, RefDescription = "Penting" });
            LevelUrgensiList.Add(new SimpleRef() { RefId = 3, RefDescription = "Normal" });
            myData.LevelUrgensiColls = LevelUrgensiList;

            return myData;
        }
        [Route("api/TrxNotificationHeader/GetSentByIdNotification/{IdNotification}/{IdTipeSaluran}")]
        public IEnumerable<fGetSentByIdNotification_Result> GetSentByIdNotification(int IdNotification, int IdTipeSaluran)
        {
            IEnumerable<fGetSentByIdNotification_Result> myDataList = new List<fGetSentByIdNotification_Result>();
            myDataList = _repHeader.GetSentByIdNotification(IdNotification, IdTipeSaluran);
            return myDataList;
        }
    }
}
