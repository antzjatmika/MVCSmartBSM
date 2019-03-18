using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxMonitoringFeeBased_ARCRep : IDataAccessRepository<trxMonitoringFeeBased_ARC, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxMonitoringFeeBased_ARC> Get()
        {
            return ctx.trxMonitoringFeeBased_ARC.ToList();
        }
        //Get Specific Data based on Id
        public trxMonitoringFeeBased_ARC Get(int id)
        {
            return ctx.trxMonitoringFeeBased_ARC.Find(id);
        }

        //Create a new Data
        public void Post(trxMonitoringFeeBased_ARC entity)
        {
            ctx.trxMonitoringFeeBased_ARC.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxMonitoringFeeBased_ARC entity)
        {
            var myData = ctx.trxMonitoringFeeBased_ARC.Find(id);
            if (myData != null)
            {
                myData.IdFeeBased = entity.IdFeeBased;
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
            var myData = ctx.trxMonitoringFeeBased_ARC.Find(id);
            if (myData != null)
            {
                ctx.trxMonitoringFeeBased_ARC.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}