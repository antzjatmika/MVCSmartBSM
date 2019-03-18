using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxRekananDocumentRep : IDataAccessRepository<trxRekananDocument, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxRekananDocument> Get()
        {
            return ctx.trxRekananDocuments.ToList();
        }
        //Get Specific Data based on Id
        public trxRekananDocument Get(int id)
        {
            return ctx.trxRekananDocuments.Find(id);
        }
        //public IEnumerable<trxRekananDocument> GetByRekanan(int idTypeOfRekanan)
        //{
        //    return ctx.trxRekananDocuments.Where(x => x.IdRekanan.Equals(idTypeOfRekanan)).ToList();
        //}
        public IEnumerable<trxDocumentMandatory> GetByRekanan(int idTypeOfRekanan)
        {
            return ctx.trxDocumentMandatories.Where(x => x.IdTypeOfRekanan.Equals(idTypeOfRekanan)).ToList();
        }
        //Create a new Data
        public IEnumerable<trxDocMandatoryDetail> GetDetailByRekanan(System.Guid IdRekanan, int IdTypeOfDocument)
        {
            return ctx.trxDocMandatoryDetails.Where(x => x.IdRekanan.Equals(IdRekanan) && x.IdTypeOfDocument.Equals(IdTypeOfDocument)).ToList();
        }
        public void Post(trxRekananDocument entity)
        {
            ctx.trxRekananDocuments.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxRekananDocument entity)
        {
            var myData = ctx.trxRekananDocuments.Find(id);
            if (myData != null)
            {
                myData.IdRekanan = entity.IdRekanan;
                myData.IdTypeOfDocument = entity.IdTypeOfDocument;
                myData.IssuedBy = entity.IssuedBy;
                myData.IssuedDate = entity.IssuedDate;
                myData.EndDate = entity.EndDate;
                myData.Notaris = entity.Notaris;
                myData.BlobDocument = entity.BlobDocument;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxRekananDocuments.Find(id);
            if (myData != null)
            {
                ctx.trxRekananDocuments.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}