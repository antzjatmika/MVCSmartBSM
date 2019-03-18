using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxTenagaPendukungImpRep : IDataAccessRepository<trxTenagaPendukungImp, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxTenagaPendukungImp> Get()
        {
            return ctx.trxTenagaPendukungImps.ToList();
        }
        //Get Specific Data based on Id
        public trxTenagaPendukungImp Get(int id)
        {
            return ctx.trxTenagaPendukungImps.Find(id);
        }
        public IEnumerable<trxTenagaPendukungImp> GetByRekanan(Guid idRekanan)
        {
            return ctx.trxTenagaPendukungImps.Where(x => x.IdRekanan.Equals(idRekanan)).ToList();
        }
        //Create a new Data
        public void Post(trxTenagaPendukungImp entity)
        {
            ctx.trxTenagaPendukungImps.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxTenagaPendukungImp entity)
        {
            var myData = ctx.trxTenagaPendukungImps.Find(id);
            if (myData != null)
            {
                myData.IdRekanan = entity.IdRekanan;
                myData.NamaLengkap = entity.NamaLengkap;
                myData.TanggalMulai = entity.TanggalMulai;
                myData.LingkupPekerjaan = entity.LingkupPekerjaan;
                myData.NomorKTP = entity.NomorKTP;
                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxTenagaPendukungImps.Find(id);
            if (myData != null)
            {
                ctx.trxTenagaPendukungImps.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}