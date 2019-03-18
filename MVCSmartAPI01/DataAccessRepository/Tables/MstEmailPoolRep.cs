using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

using System.Data.Entity.Validation;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class mstEmailPoolRep : IDataAccessRepository<mstEmailPool, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<mstEmailPool> Get()
        {
            return ctx.mstEmailPools.ToList();
        }
        //Get Specific Data based on Id
        public mstEmailPool Get(int id)
        {
            return ctx.mstEmailPools.Find(id);
        }
        public IEnumerable<mstEmailPool> GetByRekanan(Guid idRekanan)
        {
            return ctx.mstEmailPools.Where(x => x.IdRekanan.Equals(idRekanan)).ToList();
        }

        //Create a new Data
        public void Post(mstEmailPool entity)
        {
            try
            {
                ctx.mstEmailPools.Add(entity);
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
        public void Put(int id, mstEmailPool entity)
        {
            var myData = ctx.mstEmailPools.Find(id);
            if (myData != null)
            {
                myData.IdRekanan = entity.IdRekanan;
                myData.JudulEmail = entity.JudulEmail;
                myData.IsiEmail = entity.IsiEmail;
                myData.EmailTo = entity.EmailTo;
                myData.EmailFrom = entity.EmailFrom;
                myData.SentStatus = entity.SentStatus;
                myData.SentDate = entity.SentDate;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.mstEmailPools.Find(id);
            if (myData != null)
            {
                ctx.mstEmailPools.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}