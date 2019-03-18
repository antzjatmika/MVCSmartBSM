using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

using System.Data.Entity.Validation;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxTenagaAhliUploadRep : IDataAccessRepository<trxTenagaAhliUpload, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxTenagaAhliUpload> Get()
        {
            return ctx.trxTenagaAhliUploads.ToList();
        }
        //Get Specific Data based on Id
        public trxTenagaAhliUpload Get(int id)
        {
            return ctx.trxTenagaAhliUploads.Find(id);
        }
        public IEnumerable<trxTenagaAhliUpload> GetDetailByRekanan(System.Guid IdRekanan, int IdTypeOfDocument)
        {
            return ctx.trxTenagaAhliUploads.Where(x => x.IdRekanan.Equals(IdRekanan)).ToList();
        }

        //Create a new Data
        public void Post(trxTenagaAhliUpload entity)
        {
            try
            {
                ctx.trxTenagaAhliUploads.Add(entity);
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
        public void Put(int id, trxTenagaAhliUpload entity)
        {
            var myData = ctx.trxTenagaAhliUploads.Find(id);
            if (myData != null)
            {
                myData.IdRekanan = entity.IdRekanan;
                myData.PeriodeBerlaku = entity.PeriodeBerlaku;
                myData.Catatan = entity.Catatan;
                myData.FileExt = entity.FileExt;
                myData.LMDate = entity.LMDate;
                myData.CreatedUser = entity.CreatedUser;
                myData.CreatedDate = entity.CreatedDate;
                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxTenagaAhliUploads.Find(id);
            if (myData != null)
            {
                ctx.trxTenagaAhliUploads.Remove(myData);
                ctx.SaveChanges();
            }
        }
        public IEnumerable<fTotDocumentDetailByRek_Result> GetTotDocumentDetailByRek(Guid idRekanan)
        {
            IEnumerable<fTotDocumentDetailByRek_Result> myDataList = ctx.fTotDocumentDetailByRek(idRekanan).ToList();
            return myDataList;
        }
        public List<trxTenagaAhliUpload> GetTenagaAhliByRek(Guid idRekanan)
        {
            List<trxTenagaAhliUpload> myDataList = new List<trxTenagaAhliUpload>();
            myDataList = ctx.trxTenagaAhliUploads.Where(x => x.IdRekanan.Equals(idRekanan)).ToList();
            return myDataList;
        }

    }
}