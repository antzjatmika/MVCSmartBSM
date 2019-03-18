using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

using System.Data.Entity.Validation;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxTenagaAhliHeaderRep : IDataAccessRepository<trxTenagaAhliHeader, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxTenagaAhliHeader> Get()
        {
            return ctx.trxTenagaAhliHeaders.ToList();
        }
        //Get Specific Data based on Id
        public trxTenagaAhliHeader Get(int id)
        {
            return ctx.trxTenagaAhliHeaders.Find(id);
        }
        public IEnumerable<trxTenagaAhliHeader> GetByRekanan(int idTipeTenaga, Guid idRekanan)
        {
            return ctx.trxTenagaAhliHeaders.Where(x => x.IdTipeTenaga.Equals(idTipeTenaga) && x.IdRekanan.Equals(idRekanan)).ToList().OrderByDescending(x => x.CreatedDate);
        }

        public IEnumerable<trxTenagaAhliUpload> GetUploadByRekanan(Guid idRekanan)
        {
            return ctx.trxTenagaAhliUploads.Where(x => x.IdRekanan.Equals(idRekanan)).ToList().OrderByDescending(x => x.CreatedDate);
        }

        //Create a new Data
        public void Post(trxTenagaAhliHeader entity)
        {
            try
            {
                ctx.trxTenagaAhliHeaders.Add(entity);
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
        public void Put(int id, trxTenagaAhliHeader entity)
        {
            var myData = ctx.trxTenagaAhliHeaders.Find(id);
            if (myData != null)
            {
                myData.IdRekanan = entity.IdRekanan;
                myData.JudulDokumen = entity.JudulDokumen;
                myData.Catatan = entity.Catatan;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxTenagaAhliHeaders.Find(id);
            if (myData != null)
            {
                ctx.trxTenagaAhliHeaders.Remove(myData);
                ctx.SaveChanges();
            }
        }
        internal trxTenagaAhliHeader GetByGuidHeader(Guid guidHeader)
        {
            var myData = new trxTenagaAhliHeader();
            myData = ctx.trxTenagaAhliHeaders.Where(x => x.GuidHeader.Equals(guidHeader)).First();
            return myData;
        }

    }
}