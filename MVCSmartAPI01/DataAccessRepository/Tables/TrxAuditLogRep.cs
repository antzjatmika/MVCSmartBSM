using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxAuditLogRep : IDataAccessRepository<trxAuditLog, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxAuditLog> Get()
        {
            return ctx.trxAuditLogs.ToList();
        }
        //Get Specific Data based on Id
        public trxAuditLog Get(int id)
        {
            return ctx.trxAuditLogs.Find(id);
        }

        //Create a new Data
        public void Post(trxAuditLog entity)
        {
            ctx.trxAuditLogs.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxAuditLog entity)
        {
            var myData = ctx.trxAuditLogs.Find(id);
            if (myData != null)
            {
                myData.UserName = entity.UserName;
                myData.ActionType = entity.ActionType;
                myData.DetailedInfo = entity.DetailedInfo;
                myData.TransactionDate = entity.TransactionDate;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxAuditLogs.Find(id);
            if (myData != null)
            {
                ctx.trxAuditLogs.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}