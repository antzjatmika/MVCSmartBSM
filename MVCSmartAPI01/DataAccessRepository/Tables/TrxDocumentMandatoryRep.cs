using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxDocumentMandatoryRep : IDataAccessRepository<trxDocumentMandatory, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxDocumentMandatory> Get()
        {
            return ctx.trxDocumentMandatories.ToList();
        }
        //Get Specific Data based on Id
        public trxDocumentMandatory Get(int id)
        {
            return ctx.trxDocumentMandatories.Find(id);
        }

        //Create a new Data
        public void Post(trxDocumentMandatory entity)
        {
            ctx.trxDocumentMandatories.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxDocumentMandatory entity)
        {
            var myData = ctx.trxDocumentMandatories.Find(id);
            if (myData != null)
            {
                myData.IdTypeOfRekanan = entity.IdTypeOfRekanan;
                myData.IdTypeOfDocument = entity.IdTypeOfDocument;
                myData.IsMandatory = entity.IsMandatory;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxDocumentMandatories.Find(id);
            if (myData != null)
            {
                ctx.trxDocumentMandatories.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}