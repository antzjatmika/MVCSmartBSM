using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxTenagaAhliTetapImpRep : IDataAccessRepository<trxTenagaAhliTetapImp, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxTenagaAhliTetapImp> Get()
        {
            return ctx.trxTenagaAhliTetapImps.ToList();
        }
        //Get Specific Data based on Id
        public trxTenagaAhliTetapImp Get(int id)
        {
            return ctx.trxTenagaAhliTetapImps.Find(id);
        }
        public IEnumerable<trxTenagaAhliTetapImp> GetByRekanan(Guid idRekanan)
        {
            return ctx.trxTenagaAhliTetapImps.Where(x => x.IdRekanan.Equals(idRekanan)).ToList();
        }
        public IEnumerable<trxTenagaAhliTetapImp> GetByPointer(Guid ImagePointer)
        {
            return ctx.trxTenagaAhliTetapImps.Where(x => x.ImagePointer.Equals(ImagePointer)).ToList();
        }
        //Create a new Data
        public void Post(trxTenagaAhliTetapImp entity)
        {
            ctx.trxTenagaAhliTetapImps.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxTenagaAhliTetapImp entity)
        {
            var myData = ctx.trxTenagaAhliTetapImps.Find(id);
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
            var myData = ctx.trxTenagaAhliTetapImps.Find(id);
            if (myData != null)
            {
                ctx.trxTenagaAhliTetapImps.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}