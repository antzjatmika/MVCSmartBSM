using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

using System.Data.Entity.Validation;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxDocMandatoryVerificationRep : IDataAccessRepository<trxDocMandatoryVerification, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxDocMandatoryVerification> Get()
        {
            return ctx.trxDocMandatoryVerification.ToList();
        }
        //Get Specific Data based on Id
        public trxDocMandatoryVerification Get(int id)
        {
            return ctx.trxDocMandatoryVerification.Find(id);
        }
        public IEnumerable<trxDocMandatoryVerification> GetByRekanan(Guid idRekanan)
        {
            return ctx.trxDocMandatoryVerification.Where(x => x.IdRekanan.Equals(idRekanan)).ToList();
        }
        public void StoreWiCheck(trxDocMandatoryVerification entity)
        {
            var myFound = ctx.trxDocMandatoryVerification.Where(x => x.IdRekanan.Equals(entity.IdRekanan) && x.IdTypeOfDocument.Equals(entity.IdTypeOfDocument)).FirstOrDefault();
            if (myFound != null)
            {
                Put(myFound.IdTrxDocMandatoryVerification, entity);
            }
            else
            {
                Post(entity);
            }
        }
        public void StoreWiCheck_List(List<trxDocMandatoryVerification> myList)
        {
            for (int i = 0; i < myList.Count; i++)
            {
                StoreWiCheck(myList[i]);
            }
        }
        //Create a new Data
        public void Post(trxDocMandatoryVerification entity)
        {
            try
            {
                ctx.trxDocMandatoryVerification.Add(entity);
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
        public void Put(int id, trxDocMandatoryVerification entity)
        {
            var myData = ctx.trxDocMandatoryVerification.Find(id);
            if (myData != null)
            {
                myData.IdRekanan = entity.IdRekanan;
                myData.IdTypeOfDocument = entity.IdTypeOfDocument;
                myData.IsVerified = entity.IsVerified;
                myData.Catatan = entity.Catatan;
                myData.LMDate = entity.LMDate;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.mstContacts.Find(id);
            if (myData != null)
            {
                ctx.mstContacts.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}