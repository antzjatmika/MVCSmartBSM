using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

using System.Data.Entity.Validation;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxFileScannedRep : IDataAccessRepository<trxFileScanned, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxFileScanned> Get()
        {
            return ctx.trxFileScanneds.ToList();
        }
        //Get Specific Data based on Id
        public trxFileScanned Get(int id)
        {
            return ctx.trxFileScanneds.Find(id);
        }
        public IEnumerable<trxFileScanned> GetByImageBaseName(Guid imageBaseName)
        {
            return ctx.trxFileScanneds.Where(x => x.ImageBaseName.Equals(imageBaseName)).ToList();
        }

        //Create a new Data
        public void Post(trxFileScanned entity)
        {
            try
            {
                ctx.trxFileScanneds.Add(entity);
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
        public void Put(int id, trxFileScanned entity)
        {
            var myData = ctx.trxFileScanneds.Find(id);
            if (myData != null)
            {
                myData.ImageBaseName = entity.ImageBaseName;
                myData.NamaInternal = entity.NamaInternal;
                myData.EkstensiFile = entity.EkstensiFile;
                myData.Nama = entity.Nama;
                myData.Catatan = entity.Catatan;
                myData.CreatedUser = entity.CreatedUser;
                myData.CreatedDate = entity.CreatedDate;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxFileScanneds.Find(id);
            if (myData != null)
            {
                ctx.trxFileScanneds.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}