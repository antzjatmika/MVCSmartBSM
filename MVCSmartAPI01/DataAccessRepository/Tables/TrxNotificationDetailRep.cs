using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxNotificationDetailRep : IDataAccessRepository<trxNotificationDetail, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxNotificationDetail> Get()
        {
            return ctx.trxNotificationDetail.ToList();
        }
        //Get Specific Data based on Id
        public trxNotificationDetail Get(int id)
        {
            return ctx.trxNotificationDetail.Find(id);
        }
        public trxNotificationDetail GetByIdNotification(int IdNotification)
        {
            trxNotificationDetail myData = new trxNotificationDetail();
            try
            {
                myData = ctx.trxNotificationDetail.Where(x => x.IdNotification.Equals(IdNotification)).ToList().First();
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
            }
            return myData;
        }

        //Create a new Data
        public void Post(trxNotificationDetail entity)
        {
            ctx.trxNotificationDetail.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxNotificationDetail entity)
        {
            var myData = ctx.trxNotificationDetail.Find(id);
            if (myData != null)
            {
                myData.SubjectTo = entity.SubjectTo;
                myData.SubjectFrom = entity.SubjectFrom;
                myData.flgTraySent = entity.flgTraySent;
                myData.TSentDate = entity.TSentDate;
                myData.flgTrayRead = entity.flgTrayRead;
                myData.TReadDate = entity.TReadDate;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxNotificationDetail.Find(id);
            if (myData != null)
            {
                ctx.trxNotificationDetail.Remove(myData);
                ctx.SaveChanges();
            }
        }
        public void SendNotiDetail(int IdNotificationDetail)
        {
            var myData = ctx.trxNotificationDetail.Find(IdNotificationDetail);
            if (myData != null)
            {
                myData.flgTraySent = true;
                myData.TSentDate = DateTime.Today;
                ctx.SaveChanges();
            }
        }
        public void ReadNotiDetail(int IdNotificationDetail)
        {
            var myData = ctx.trxNotificationDetail.Find(IdNotificationDetail);
            if (myData != null)
            {
                myData.flgTrayRead = true;
                myData.TReadDate = DateTime.Today;
                ctx.SaveChanges();
            }
        }
    }
}