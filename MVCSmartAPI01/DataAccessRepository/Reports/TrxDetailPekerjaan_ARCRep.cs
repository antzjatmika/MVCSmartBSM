using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxDetailPekerjaan_ARCRep : IDataAccessRepository<trxDetailPekerjaan_ARC, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxDetailPekerjaan_ARC> Get()
        {
            return ctx.trxDetailPekerjaan_ARC.ToList();
        }
        //Get Specific Data based on Id
        public trxDetailPekerjaan_ARC Get(int id)
        {
            return ctx.trxDetailPekerjaan_ARC.Find(id);
        }
        public IEnumerable<trxDetailPekerjaan_ARC> GetByRekanan(Guid IdRekanan)
        {
            return ctx.trxDetailPekerjaan_ARC.Where(x => x.IdRekanan.Equals(IdRekanan)).ToList();
        }
        //Create a new Data
        public void Post(trxDetailPekerjaan_ARC entity)
        {
            ctx.trxDetailPekerjaan_ARC.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxDetailPekerjaan_ARC entity)
        {
            var myData = ctx.trxDetailPekerjaan_ARC.Find(id);
            if (myData != null)
            {
                myData.IdDetailPekerjaan = entity.IdDetailPekerjaan;
                myData.IdRekanan = entity.IdRekanan;
                myData.TahunLaporan = entity.TahunLaporan;
                myData.BulanLaporan = entity.BulanLaporan;
                myData.DebiturName = entity.DebiturName;
                myData.DebiturLocation = entity.DebiturLocation;
                myData.BidangUsahaDebitur = entity.BidangUsahaDebitur;
                myData.JenisJasa = entity.JenisJasa;
                myData.NamaPemberiPekerjaan = entity.NamaPemberiPekerjaan;
                myData.UnitKerja = entity.UnitKerja;
                myData.TotalAsetPerusahaan = entity.TotalAsetPerusahaan;
                myData.FeeNominal = entity.FeeNominal;
                myData.NilaiPenutupan = entity.NilaiPenutupan;
                myData.TanggalMulaiPekerjaan = entity.TanggalMulaiPekerjaan;
                myData.TanggalSelesaiPekerjaan = entity.TanggalSelesaiPekerjaan;
                myData.NomorPolis = entity.NomorPolis;
                myData.PICRekanan = entity.PICRekanan;
                myData.PICBank = entity.PICBank;
                myData.Keterangan = entity.Keterangan;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxDetailPekerjaan_ARC.Find(id);
            if (myData != null)
            {
                ctx.trxDetailPekerjaan_ARC.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}