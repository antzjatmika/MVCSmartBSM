using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxTenagaAhliTMPRep : IDataAccessRepository<trxTenagaAhliTMP, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxTenagaAhliTMP> Get()
        {
            return ctx.trxTenagaAhliTMPs.ToList();
        }
        //Get Specific Data based on Id
        public trxTenagaAhliTMP Get(int id)
        {
            return ctx.trxTenagaAhliTMPs.Find(id);
        }
        public IEnumerable<trxTenagaAhliTMP> GetByRekanan(Guid idRekanan)
        {
            return ctx.trxTenagaAhliTMPs.Where(x => x.IdRekanan.Equals(idRekanan)).ToList();
        }
        //Create a new Data
        public void Post(trxTenagaAhliTMP entity)
        {
            ctx.trxTenagaAhliTMPs.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxTenagaAhliTMP entity)
        {
            var myData = ctx.trxTenagaAhliTMPs.Find(id);
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
            var myData = ctx.trxTenagaAhliTMPs.Find(id);
            if (myData != null)
            {
                ctx.trxTenagaAhliTMPs.Remove(myData);
                ctx.SaveChanges();
            }
        }

        internal List<trxTenagaAhliTMP> GetByGuidHeader(Guid guidHeader)
        {
            var myDataList = new List<trxTenagaAhliTMP>();
            myDataList = ctx.trxTenagaAhliTMPs.Where(x => x.GuidHeader.Equals(guidHeader)).ToList();
            return myDataList;
        }
    }
}