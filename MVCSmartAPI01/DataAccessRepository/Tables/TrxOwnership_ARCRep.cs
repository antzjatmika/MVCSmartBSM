using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxOwnership_ARCRep : IDataAccessRepository<trxOwnership_ARC, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxOwnership_ARC> Get()
        {
            return ctx.trxOwnership_ARC.ToList();
        }
        //Get Specific Data based on Id
        public trxOwnership_ARC Get(int id)
        {
            return ctx.trxOwnership_ARC.Find(id);
        }

        //Create a new Data
        public void Post(trxOwnership_ARC entity)
        {
            ctx.trxOwnership_ARC.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxOwnership_ARC entity)
        {
            var myData = ctx.trxOwnership_ARC.Find(id);
            if (myData != null)
            {
                myData.IdAction = entity.IdAction;
                myData.IdOwnership = entity.IdOwnership;
                myData.IdRekanan = entity.IdRekanan;
                myData.Name = entity.Name;
                myData.Title = entity.Title;
                myData.KTP = entity.KTP;
                myData.KTPEndDate = entity.KTPEndDate;
                myData.Sertifikasi = entity.Sertifikasi;
                myData.PercentSaham = entity.PercentSaham;
                myData.NominalSaham = entity.NominalSaham;
                myData.IsKeyPerson = entity.IsKeyPerson;
                myData.NPWP = entity.NPWP;
                myData.IsActive = entity.IsActive;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxOwnership_ARC.Find(id);
            if (myData != null)
            {
                ctx.trxOwnership_ARC.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}