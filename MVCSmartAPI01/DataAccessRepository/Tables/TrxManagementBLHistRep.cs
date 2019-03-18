using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

using System.Data.Entity.Validation;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxManagementBLHistRep : IDataAccessRepository<trxManagementBlackListHistory, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxManagementBlackListHistory> Get()
        {
            return ctx.trxManagementBlackListHistories.ToList();
        }
        //Get Specific Data based on Id
        public trxManagementBlackListHistory Get(int id)
        {
            return ctx.trxManagementBlackListHistories.Find(id);
        }
        public trxManagementBlackListHistory GetByKTP_NPWP(string nomorKTP, string nomorNPWP)
        {
            trxManagementBlackListHistory trxResult = new trxManagementBlackListHistory();
            trxResult = ctx.trxManagementBlackListHistories.Where(x => x.NomorKTP.Equals(nomorKTP)).FirstOrDefault();
            if(trxResult == null)
            {
                trxResult = ctx.trxManagementBlackListHistories.Where(x => x.NomorNPWP.Equals(nomorNPWP)).FirstOrDefault();
            }
            return trxResult;
        }
        public trxManagementBlackListHistory GetByNPWP(string nomorNPWP)
        {
            return ctx.trxManagementBlackListHistories.Where(x => x.NomorNPWP.Equals(nomorNPWP)).FirstOrDefault();
        }

        //Create a new Data
        public void Post(trxManagementBlackListHistory entity)
        {
            try
            {
                ctx.trxManagementBlackListHistories.Add(entity);
                ctx.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Console.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }
        }

        public void Put(int id, trxManagementBlackListHistory entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}