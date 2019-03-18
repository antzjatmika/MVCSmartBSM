using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

using System.Data.Entity.Validation;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxDocMandatoryFileRep : IDataAccessRepository<trxDocMandatoryFile, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxDocMandatoryFile> Get()
        {
            return ctx.trxDocMandatoryFiles.ToList();
        }
        //Get Specific Data based on Id
        public trxDocMandatoryFile Get(int id)
        {
            return ctx.trxDocMandatoryFiles.Find(id);
        }
        public IEnumerable<trxDocMandatoryFile> GetFileByDetail(int IdDocMandatoryDetail)
        {
            return ctx.trxDocMandatoryFiles.Where(x => x.IdDocMandatoryDetail.Equals(IdDocMandatoryDetail)).ToList();
        }

        //Create a new Data
        public void Post(trxDocMandatoryFile entity)
        {
            try
            {
                ctx.trxDocMandatoryFiles.Add(entity);
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
        //Update Exisiting Data
        public void Put(int id, trxDocMandatoryFile entity)
        {
            var myData = ctx.trxDocMandatoryFiles.Find(id);
            if (myData != null)
            {
                myData.namaFile = entity.namaFile;
                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxDocMandatoryFiles.Find(id);
            if (myData != null)
            {
                ctx.trxDocMandatoryFiles.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}