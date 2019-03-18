using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

using System.Data.Entity.Validation;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxTenagaPendukungRep : IDataAccessRepository<trxTenagaPendukung, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxTenagaPendukung> Get()
        {
            return ctx.trxTenagaPendukungs.ToList();
        }
        //Get Specific Data based on Id
        public trxTenagaPendukung Get(int id)
        {
            return ctx.trxTenagaPendukungs.Find(id);
        }
        public IEnumerable<trxTenagaPendukung> GetByRekanan(Guid idRekanan)
        {
            return ctx.trxTenagaPendukungs.Where(x => x.IdRekanan.Equals(idRekanan)).ToList();
        }

        //Create a new Data
        public void Post(trxTenagaPendukung entity)
        {
            try
            {
                ctx.trxTenagaPendukungs.Add(entity);
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
        public void Put(int id, trxTenagaPendukung entity)
        {
            var myData = ctx.trxTenagaPendukungs.Find(id);
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
            var myData = ctx.trxTenagaPendukungs.Find(id);
            if (myData != null)
            {
                ctx.trxTenagaPendukungs.Remove(myData);
                ctx.SaveChanges();
            }
        }
        internal IEnumerable<trxTenagaPendukung> GetByGuidHeader(Guid guidHeader)
        {
            var myDataList = new List<trxTenagaPendukung>();
            myDataList = ctx.trxTenagaPendukungs.Where(x => x.GuidHeader.Equals(guidHeader)).ToList();
            return myDataList;
        }
    }
}