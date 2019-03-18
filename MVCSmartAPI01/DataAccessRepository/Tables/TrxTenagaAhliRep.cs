using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxTenagaAhliRep : IDataAccessRepository<trxTenagaAhli, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxTenagaAhli> Get()
        {
            return ctx.trxTenagaAhlis.ToList();
        }
        //Get Specific Data based on Id
        public trxTenagaAhli Get(int id)
        {
            return ctx.trxTenagaAhlis.Find(id);
        }
        public IEnumerable<trxTenagaAhli> GetByRekanan(Guid idRekanan)
        {
            return ctx.trxTenagaAhlis.Where(x => x.IdRekanan.Equals(idRekanan)).ToList();
        }
        //Create a new Data
        public void Post(trxTenagaAhli entity)
        {
            ctx.trxTenagaAhlis.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxTenagaAhli entity)
        {
            var myData = ctx.trxTenagaAhlis.Find(id);
            if (myData != null)
            {
                myData.IdRekanan = entity.IdRekanan;
                myData.NamaLengkap = entity.NamaLengkap;
                myData.Jabatan = entity.Jabatan;
                myData.MulaiMendudukiJabatan = entity.MulaiMendudukiJabatan;
                myData.MulaiBekerjaSebagaiPenilai = entity.MulaiBekerjaSebagaiPenilai;
                myData.RiwayatPekerjaan = entity.RiwayatPekerjaan;
                myData.LingkupPekerjaan = entity.LingkupPekerjaan;
                myData.AlumniPTNPTS = entity.AlumniPTNPTS;
                myData.JenjangPendidikan = entity.JenjangPendidikan;
                myData.KeanggotaanMAPPI = entity.KeanggotaanMAPPI;
                myData.NoIjinPenilai = entity.NoIjinPenilai;
                myData.KantorPusatCabang = entity.KantorPusatCabang;
                myData.Catatan = entity.Catatan;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxTenagaAhlis.Find(id);
            if (myData != null)
            {
                ctx.trxTenagaAhlis.Remove(myData);
                ctx.SaveChanges();
            }
        }

        internal IEnumerable<trxTenagaAhli> GetByGuidHeader(Guid guidHeader)
        {
            var myDataList = new List<trxTenagaAhli>();
            myDataList = ctx.trxTenagaAhlis.Where(x => x.GuidHeader.Equals(guidHeader)).ToList();
            return myDataList;
        }
    }
}