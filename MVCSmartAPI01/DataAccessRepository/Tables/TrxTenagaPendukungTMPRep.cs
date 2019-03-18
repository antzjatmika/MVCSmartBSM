using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

using System.Data.Entity.Validation;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxTenagaPendukungTMPRep : IDataAccessRepository<trxTenagaPendukungTMP, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxTenagaPendukungTMP> Get()
        {
            return ctx.trxTenagaPendukungTMPs.ToList();
        }
        //Get Specific Data based on Id
        public trxTenagaPendukungTMP Get(int id)
        {
            return ctx.trxTenagaPendukungTMPs.Find(id);
        }
        public IEnumerable<trxTenagaPendukungTMP> GetByRekanan(Guid idRekanan)
        {
            return ctx.trxTenagaPendukungTMPs.Where(x => x.IdRekanan.Equals(idRekanan)).ToList();
        }

        //Create a new Data
        public void Post(trxTenagaPendukungTMP entity)
        {
            try
            {
                ctx.trxTenagaPendukungTMPs.Add(entity);
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
        public void Put(int id, trxTenagaPendukungTMP entity)
        {
            var myData = ctx.trxTenagaPendukungTMPs.Find(id);
            if (myData != null)
            {
                myData.IdRekanan = entity.IdRekanan;
                myData.Name = entity.Name;
                myData.Alamat = entity.Alamat;
                myData.NomorKTP = entity.NomorKTP;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxTenagaPendukungTMPs.Find(id);
            if (myData != null)
            {
                ctx.trxTenagaPendukungTMPs.Remove(myData);
                ctx.SaveChanges();
            }
        }
        internal List<trxTenagaPendukungTMP> GetByGuidHeader(Guid guidHeader)
        {
            var myDataList = new List<trxTenagaPendukungTMP>();
            myDataList = ctx.trxTenagaPendukungTMPs.Where(x => x.GuidHeader.Equals(guidHeader)).ToList();
            return myDataList;
        }
    }
}