using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class EmlNotificationLogRep : IDataAccessRepository<emlNotificationLog, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<emlNotificationLog> Get()
        {
            return ctx.emlNotificationLogs.ToList();
        }
        //Get Specific Data based on Id
        public emlNotificationLog Get(int id)
        {
            return ctx.emlNotificationLogs.Find(id);
        }

        //Create a new Data
        public void Post(emlNotificationLog entity)
        {
            ctx.emlNotificationLogs.Add(entity);
            ctx.SaveChanges();
        }
        //Update Existing Data
        public void Put(int id, emlNotificationLog entity)
        {
            var myData = ctx.emlNotificationLogs.Find(id);
            if (myData != null)
            {
                myData.SendAttemptSeq = entity.SendAttemptSeq;
                myData.SendAttemptDate = entity.SendAttemptDate;
                myData.LogMessage = entity.LogMessage;
                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.emlNotificationLogs.Find(id);
            if (myData != null)
            {
                ctx.emlNotificationLogs.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}