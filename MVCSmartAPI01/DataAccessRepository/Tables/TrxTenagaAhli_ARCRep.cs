using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxTenagaAhli_ARCRep : IDataAccessRepository<trxTenagaAhli_ARC, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxTenagaAhli_ARC> Get()
        {
            return ctx.trxTenagaAhli_ARC.ToList();
        }
        //Get Specific Data based on Id
        public trxTenagaAhli_ARC Get(int id)
        {
            return ctx.trxTenagaAhli_ARC.Find(id);
        }

        //Create a new Data
        public void Post(trxTenagaAhli_ARC entity)
        {
            ctx.trxTenagaAhli_ARC.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxTenagaAhli_ARC entity)
        {
            var myData = ctx.trxTenagaAhli_ARC.Find(id);
            if (myData != null)
            {
                myData.IdAction = entity.IdAction;
                myData.IdTenagaAhli = entity.IdTenagaAhli;
                myData.IdRekanan = entity.IdRekanan;
                myData.Name = entity.Name;
                myData.Title = entity.Title;
                myData.Sertifikasi = entity.Sertifikasi;
                myData.Gelar = entity.Gelar;
                myData.AsosiasiProfesi = entity.AsosiasiProfesi;
                myData.Experience = entity.Experience;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxTenagaAhli_ARC.Find(id);
            if (myData != null)
            {
                ctx.trxTenagaAhli_ARC.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}