using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxNotificationHeaderRep : IDataAccessRepository<trxNotificationHeader, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxNotificationHeader> Get()
        {
            return ctx.trxNotificationHeaders.ToList();
        }
        //Get Specific Data based on Id
        public trxNotificationHeader Get(int id)
        {
            return ctx.trxNotificationHeaders.Find(id);
        }

        //Create a new Data
        public int PostNew(trxNotificationHeader entity)
        {
            int intNewId = 0;
            ctx.trxNotificationHeaders.Add(entity);
            ctx.SaveChanges();
            intNewId = entity.IdNotification;
            return intNewId;
        }
        public void Post(trxNotificationHeader entity)
        {
            ctx.trxNotificationHeaders.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxNotificationHeader entity)
        {
            var myData = ctx.trxNotificationHeaders.Find(id);
            if (myData != null)
            {
                myData.IdTipeNotification = entity.IdTipeNotification ;
                myData.IdLevelUrgensi = entity.IdLevelUrgensi ;
                myData.CaptionInfo = entity.CaptionInfo ;
                myData.IdDataProfilePart = entity.IdDataProfilePart ;
                myData.flgValidity = entity.flgValidity ;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxNotificationHeaders.Find(id);
            if (myData != null)
            {
                ctx.trxNotificationHeaders.Remove(myData);
                ctx.SaveChanges();
            }
        }
        public List<VmNotiBySubject> GetNotificationBySubject(System.Guid SubjectId, int LevelUrgensi)
        {
            var NotiCollectionResult = ctx.fGetNotificationBySubject(SubjectId, LevelUrgensi).ToList<fGetNotificationBySubject_Result>();
            List<VmNotiBySubject> NotiCollection = new List<VmNotiBySubject>();
            for (int i = 0; i < NotiCollectionResult.Count; i++)
            {
                NotiCollection.Add(new VmNotiBySubject()
                    {
                        IdTipeNotification = NotiCollectionResult[i].IdTipeNotification,
                        IdLevelUrgensi = NotiCollectionResult[i].IdLevelUrgensi,
                        CaptionInfo = NotiCollectionResult[i].CaptionInfo,
                        Pengirim = NotiCollectionResult[i].Pengirim,
                        BodyContent = NotiCollectionResult[i].BodyContent,
                        flgTrayRead = NotiCollectionResult[i].flgTrayRead,
                        IdNotificationDetail = NotiCollectionResult[i].IdNotificationDetail,
                        IdNotificationContent = NotiCollectionResult[i].IdNotificationContent
                    });
            }
            return NotiCollection;
        }
        public IEnumerable<fGetTInboxCaptionBySubjectTo_Result> GetTInboxCaptionBySubjectTo(Guid SubjectTo)
        {
            IEnumerable<fGetTInboxCaptionBySubjectTo_Result> myDataList = new List<fGetTInboxCaptionBySubjectTo_Result>();
            myDataList = ctx.fGetTInboxCaptionBySubjectTo(SubjectTo);
            return myDataList;
        }
        public IEnumerable<fGetTInboxDetailBySubjectTo_Result> GetTInboxDetailBySubjectTo(Guid SubjectTo)
        {
            IEnumerable<fGetTInboxDetailBySubjectTo_Result> myDataList = new List<fGetTInboxDetailBySubjectTo_Result>();
            myDataList = ctx.fGetTInboxDetailBySubjectTo(SubjectTo);
            return myDataList;
        }
        public IEnumerable<fGetTDraftCaptionBySubjectFrom_Result> GetTDraftCaptionBySubjectFrom(string SubjectFrom)
        {
            IEnumerable<fGetTDraftCaptionBySubjectFrom_Result> myDataList = new List<fGetTDraftCaptionBySubjectFrom_Result>();
            myDataList = ctx.fGetTDraftCaptionBySubjectFrom(SubjectFrom);
            return myDataList;
        }
        public IEnumerable<fGetTDraftDetailBySubjectFrom_Result> GetTDraftDetailBySubjectFrom(Guid SubjectFrom)
        {
            IEnumerable<fGetTDraftDetailBySubjectFrom_Result> myDataList = new List<fGetTDraftDetailBySubjectFrom_Result>();
            myDataList = ctx.fGetTDraftDetailBySubjectFrom(SubjectFrom);
            return myDataList;
        }

        public IEnumerable<fGetTSentCaptionBySubjectFrom_Result> GetTSentCaptionBySubjectFrom(Guid SubjectFrom, int IdTipeSaluran)
        {
            IEnumerable<fGetTSentCaptionBySubjectFrom_Result> myDataList = new List<fGetTSentCaptionBySubjectFrom_Result>();
            myDataList = ctx.fGetTSentCaptionBySubjectFrom(SubjectFrom, IdTipeSaluran);
            return myDataList;
        }
        public IEnumerable<fGetTSentDetailBySubjectFrom_Result> GetTSentDetailBySubjectFrom(Guid SubjectFrom)
        {
            IEnumerable<fGetTSentDetailBySubjectFrom_Result> myDataList = new List<fGetTSentDetailBySubjectFrom_Result>();
            myDataList = ctx.fGetTSentDetailBySubjectFrom(SubjectFrom);
            return myDataList;
        }
        public IEnumerable<fGetNotiByIdNotification_Result> GetNotiByIdNotification(int IdNotification)
        {
            IEnumerable<fGetNotiByIdNotification_Result> myDataList = new List<fGetNotiByIdNotification_Result>();
            myDataList = ctx.fGetNotiByIdNotification(IdNotification);
            return myDataList;
        }
        public fReadInfoByIdNotification_Result ReadInfoByIdNotification(int IdNotificationDetail, bool FlgTraySent)
        {
            fReadInfoByIdNotification_Result myData = new fReadInfoByIdNotification_Result();
            myData = ctx.fReadInfoByIdNotification(IdNotificationDetail, FlgTraySent).First();
            return myData;
        }
        public fReadInfoByIdNotification_Result ReadInfoByIdNotificationHead(int IdNotification)
        {
            trxNotificationDetail myDataDetail = new trxNotificationDetail();
            try
            {
                myDataDetail = ctx.trxNotificationDetail.Where(x => x.IdNotification.Equals(IdNotification) && x.flgTraySent.Equals(true)).FirstOrDefault();
            }
            catch(Exception ex)
            {
                string fff = ex.Message;
            }
            fReadInfoByIdNotification_Result myData = new fReadInfoByIdNotification_Result();
            if (myDataDetail != null)
            {
                myData = ctx.fReadInfoByIdNotification(myDataDetail.IdNotificationDetail, myDataDetail.flgTraySent).First();
            }
            return myData;
        }
        public IEnumerable<fGetSentByIdNotification_Result> GetSentByIdNotification(int IdNotification, int IdTipeSaluran)
        {
            IEnumerable<fGetSentByIdNotification_Result> myDataList = new List<fGetSentByIdNotification_Result>();
            myDataList = ctx.fGetSentByIdNotification(IdNotification, IdTipeSaluran);
            return myDataList;
        }
    
    }
}