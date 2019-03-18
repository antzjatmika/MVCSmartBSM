using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxTenagaAhliTidakTetapImpRep : IDataAccessRepository<trxTenagaAhliTidakTetapImp, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxTenagaAhliTidakTetapImp> Get()
        {
            return ctx.trxTenagaAhliTidakTetapImps.ToList();
        }
        //Get Specific Data based on Id
        public trxTenagaAhliTidakTetapImp Get(int id)
        {
            return ctx.trxTenagaAhliTidakTetapImps.Find(id);
        }
        public IEnumerable<trxTenagaAhliTidakTetapImp> GetByRekanan(Guid idRekanan)
        {
            return ctx.trxTenagaAhliTidakTetapImps.Where(x => x.IdRekanan.Equals(idRekanan)).ToList();
        }
        //Create a new Data
        public void Post(trxTenagaAhliTidakTetapImp entity)
        {
            ctx.trxTenagaAhliTidakTetapImps.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxTenagaAhliTidakTetapImp entity)
        {
            var myData = ctx.trxTenagaAhliTidakTetapImps.Find(id);
            if (myData != null)
            {
                myData.IdRekanan = entity.IdRekanan;
                myData.NamaLengkap = entity.NamaLengkap;
                myData.TanggalMulai = entity.TanggalMulai;
                myData.JenisKantor = entity.JenisKantor;
                myData.NamaKantor = entity.NamaKantor;
                myData.NomorKTP = entity.NomorKTP;
                myData.NomorNPWP = entity.NomorNPWP;
                myData.NomorIAPI = entity.NomorIAPI;
                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxTenagaAhliTidakTetapImps.Find(id);
            if (myData != null)
            {
                ctx.trxTenagaAhliTidakTetapImps.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}