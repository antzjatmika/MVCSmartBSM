using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxNotificationContentRep : IDataAccessRepository<trxNotificationContent, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxNotificationContent> Get()
        {
            return ctx.trxNotificationContents.ToList();
        }
        //Get Specific Data based on Id
        public trxNotificationContent Get(int id)
        {
            return ctx.trxNotificationContents.Find(id);
        }
        public trxNotificationContent GetByIdNotification(int IdNotification)
        {
            trxNotificationContent myData = new trxNotificationContent();
            try
            {
                myData = ctx.trxNotificationContents.Where(x => x.IdNotification.Equals(IdNotification)).ToList().First();
            }
            catch(Exception ex)
            {
                string strErr = ex.Message;
            }
            return myData;
        }
        //Create a new Data
        public void Post(trxNotificationContent entity)
        {
            ctx.trxNotificationContents.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxNotificationContent entity)
        {
            var myData = ctx.trxNotificationContents.Find(id);
            if (myData != null)
            {
                myData.BodyContent = entity.BodyContent;
                myData.FileExt1 = entity.FileExt1;
                myData.FileExt2 = entity.FileExt2;
                myData.FileExt3 = entity.FileExt3;
                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxNotificationContents.Find(id);
            if (myData != null)
            {
                ctx.trxNotificationContents.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}