using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxMonitoringFeeBasedRep : IDataAccessRepository<trxMonitoringFeeBased, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxMonitoringFeeBased> Get()
        {
            return ctx.trxMonitoringFeeBaseds.ToList();
        }
        //Get Specific Data based on Id
        public trxMonitoringFeeBased Get(int id)
        {
            return ctx.trxMonitoringFeeBaseds.Find(id);
        }

        //Create a new Data
        public void Post(trxMonitoringFeeBased entity)
        {
            ctx.trxMonitoringFeeBaseds.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxMonitoringFeeBased entity)
        {
            var myData = ctx.trxMonitoringFeeBaseds.Find(id);
            if (myData != null)
            {
                myData.IdRekanan = entity.IdRekanan;
                myData.IdRegion = entity.IdRegion;
                myData.IdArea = entity.IdArea;
                myData.IdCabang = entity.IdCabang;
                myData.NomorPolis = entity.NomorPolis;
                myData.Produk = entity.Produk;
                myData.NilaiPertanggungan = entity.NilaiPertanggungan;
                myData.Periode = entity.Periode;
                myData.NilaiPremi = entity.NilaiPremi;
                myData.NilaiFeeBased = entity.NilaiFeeBased;
                myData.NilaiPPNBasedNet = entity.NilaiPPNBasedNet;
                myData.NilaiPPNBased = entity.NilaiPPNBased;
                myData.PercentFeeBased = entity.PercentFeeBased;
                myData.IdChannel = entity.IdChannel;
                myData.SudahBayar = entity.SudahBayar;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxMonitoringFeeBaseds.Find(id);
            if (myData != null)
            {
                ctx.trxMonitoringFeeBaseds.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}