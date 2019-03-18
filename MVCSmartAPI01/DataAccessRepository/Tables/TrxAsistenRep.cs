using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

using System.Data.Entity.Validation;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxAsistenRep : IDataAccessRepository<trxAsisten, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxAsisten> Get()
        {
            return ctx.trxAsistens.ToList();
        }
        //Get Specific Data based on Id
        public trxAsisten Get(int id)
        {
            return ctx.trxAsistens.Find(id);
        }
        public IEnumerable<trxAsisten> GetByRekanan(Guid idRekanan)
        {
            return ctx.trxAsistens.Where(x => x.IdRekanan.Equals(idRekanan)).ToList();
        }

        //Create a new Data
        public void Post(trxAsisten entity)
        {
            try
            {
                ctx.trxAsistens.Add(entity);
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
        public void Put(int id, trxAsisten entity)
        {
            var myData = ctx.trxAsistens.Find(id);
            if (myData != null)
            {
                myData.IdRekanan = entity.IdRekanan;
                myData.Name = entity.Name;
                myData.Alamat = entity.Alamat;
                myData.NomorKTP = entity.NomorKTP;
                myData.NomorNPWP = entity.NomorNPWP;
                myData.Email1 = entity.Email1;
                myData.Email2 = entity.Email2;
                myData.Handphone1 = entity.Handphone1;
                myData.Handphone2 = entity.Handphone2;
                myData.Telephone1 = entity.Telephone1;
                myData.Telephone2 = entity.Telephone2;
                myData.Telephone3 = entity.Telephone3;
                myData.Fax1 = entity.Fax1;
                myData.Fax2 = entity.Fax2;
                myData.IsActive = entity.IsActive;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxAsistens.Find(id);
            if (myData != null)
            {
                ctx.trxAsistens.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}