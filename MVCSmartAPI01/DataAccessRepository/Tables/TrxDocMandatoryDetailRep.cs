using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

using System.Data.Entity.Validation;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxDocMandatoryDetailRep : IDataAccessRepository<trxDocMandatoryDetail, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxDocMandatoryDetail> Get()
        {
            return ctx.trxDocMandatoryDetails.ToList();
        }
        //Get Specific Data based on Id
        public trxDocMandatoryDetail Get(int id)
        {
            return ctx.trxDocMandatoryDetails.Find(id);
        }
        public IEnumerable<trxDocMandatoryDetail> GetDetailByRekanan(System.Guid IdRekanan, int IdTypeOfDocument)
        {
            return ctx.trxDocMandatoryDetails.Where(x => x.IdRekanan.Equals(IdRekanan) && x.IdTypeOfDocument.Equals(IdTypeOfDocument)).ToList();
        }

        //Create a new Data
        public void Post(trxDocMandatoryDetail entity)
        {
            try
            {
                ctx.trxDocMandatoryDetails.Add(entity);
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
        public void Put(int id, trxDocMandatoryDetail entity)
        {
            var myData = ctx.trxDocMandatoryDetails.Find(id);
            if (myData != null)
            {
                myData.IdRekanan = entity.IdRekanan;
                myData.IdTypeOfDocument = entity.IdTypeOfDocument;
                myData.nomorDokumen = entity.nomorDokumen;
                myData.badanPembuatDokumen = entity.badanPembuatDokumen;
                myData.tanggalAwal = entity.tanggalAwal;
                myData.tanggalAkhir = entity.tanggalAkhir;
                myData.catatan = entity.catatan;
                myData.FileExt = entity.FileExt;
                myData.FileExt2 = entity.FileExt2;
                myData.ProcInfo = entity.ProcInfo;
                myData.LMDate = entity.LMDate;
                myData.CreatedUser = entity.CreatedUser;
                myData.CreatedDate = entity.CreatedDate;
                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxDocMandatoryDetails.Find(id);
            if (myData != null)
            {
                ctx.trxDocMandatoryDetails.Remove(myData);
                ctx.SaveChanges();
            }
        }
        public IEnumerable<fTotDocumentDetailByRek_Result> GetTotDocumentDetailByRek(Guid idRekanan)
        {
            IEnumerable<fTotDocumentDetailByRek_Result> myDataList = ctx.fTotDocumentDetailByRek(idRekanan).ToList();
            return myDataList;
        }
        public List<trxDocMandatoryDetail> GetTenagaAhliByRek(Guid idRekanan)
        {
            List<trxDocMandatoryDetail> myDataList = new List<trxDocMandatoryDetail>();
            myDataList = ctx.trxDocMandatoryDetails.Where(x => x.IdRekanan.Equals(idRekanan) && x.IdTypeOfDocument.Equals(27)).ToList();
            return myDataList;
        }
        public List<trxDocMandatoryDetail> GetTenagaAhliByRek(Guid idRekanan, int IdTypeOfDocument)
        {
            List<trxDocMandatoryDetail> myDataList = new List<trxDocMandatoryDetail>();
            myDataList = ctx.trxDocMandatoryDetails.Where(x => x.IdRekanan.Equals(idRekanan) && x.IdTypeOfDocument.Equals(IdTypeOfDocument)).ToList();
            return myDataList;
        }

    }
}