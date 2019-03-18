using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;
using System.Linq.Dynamic;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxDetailPekerjaanBLGRep : IDataAccessRepository<trxDetailPekerjaanBLG, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxDetailPekerjaanBLG> Get()
        {
            return ctx.trxDetailPekerjaanBLGs.ToList();
        }
        //Get Specific Data based on Id
        public trxDetailPekerjaanBLG Get(int id)
        {
            return ctx.trxDetailPekerjaanBLGs.Find(id);
        }
        public IEnumerable<trxDetailPekerjaanBLG> GetByRekanan(Guid IdRekanan)
        {
            return ctx.trxDetailPekerjaanBLGs.Where(x => x.IdRekanan.Equals(IdRekanan)).ToList();
        }
        public IEnumerable<trxDetailPekerjaanBLG> GetByRekananXLS(Guid IdRekanan, string strFilterExp1, string strFilterExp2)
        {
            string strError = string.Empty;
            List<trxDetailPekerjaanBLG> myDataList = new List<trxDetailPekerjaanBLG>();
            try
            {
                var query1 = (from excelSmart in ctx.trxDetailPekerjaanBLGs.Where(x => x.IdRekanan.Equals(IdRekanan)) select excelSmart).AsQueryable().Where(strFilterExp1);
                var query2 = (from excelSmart in query1 select excelSmart).AsQueryable().Where(strFilterExp2);
                myDataList = query2.ToList<trxDetailPekerjaanBLG>();
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            return myDataList;
        }
        public IEnumerable<fPekerjaanBLGByTypeOfRekanan_Result> PekerjaanByTypeOfRekanan()
        {
            IEnumerable<fPekerjaanBLGByTypeOfRekanan_Result> myDataList = new List<fPekerjaanBLGByTypeOfRekanan_Result>();
            try
            {
                myDataList = ctx.fPekerjaanBLGByTypeOfRekanan().ToList();
            }
            catch(Exception ex)
            {
                string strError = ex.Message;
            }
            return myDataList;
        }

        public IEnumerable<fPekerjaanBLGByTypeOfRekanan_Result> XLS_PekerjaanByTypeOfRek(string strFilterExp1, string strFilterExp2)
        {
            string strError = string.Empty;
            List<fPekerjaanBLGByTypeOfRekanan_Result> myDataList = new List<fPekerjaanBLGByTypeOfRekanan_Result>();
            try
            {
                var query1 = (from excelSmart in ctx.fPekerjaanBLGByTypeOfRekanan() select excelSmart).AsQueryable().Where(strFilterExp1);
                var query2 = (from excelSmart in query1 select excelSmart).AsQueryable().Where(strFilterExp2);
                myDataList = query2.ToList<fPekerjaanBLGByTypeOfRekanan_Result>();
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            return myDataList;
        }

        //Create a new Data
        public void Post(trxDetailPekerjaanBLG entity)
        {
            try
            {
                ctx.trxDetailPekerjaanBLGs.Add(entity);
                ctx.SaveChanges();
            }
            catch(Exception ex)
            {
                string str = ex.Message;
            }
        }
        //Update Exisiting Data
        public void Put(int id, trxDetailPekerjaanBLG entity)
        {
            var myData = ctx.trxDetailPekerjaanBLGs.Find(id);
            if (myData != null)
            {
                myData.IdRekanan = entity.IdRekanan;
                myData.DebiturName = entity.DebiturName;
                myData.DebiturLocation = entity.DebiturLocation;
                myData.IdRegion = entity.IdRegion;
                myData.IdSegmentasi = entity.IdSegmentasi;
                myData.JenisProperty = entity.JenisProperty;
                myData.Lokasi = entity.Lokasi;
                myData.JenisLelang = entity.JenisLelang;
                myData.NoPerjanjian = entity.NoPerjanjian;
                myData.TanggalPerjanjian = entity.TanggalPerjanjian;
                myData.NilaiLimitLelang = entity.NilaiLimitLelang;
                myData.NilaiTransaksi = entity.NilaiTransaksi;
                myData.FeeImbalanJasa = entity.FeeImbalanJasa;
                myData.TanggalMulai = entity.TanggalMulai;
                myData.TanggalSelesaiPekerjaan = entity.TanggalSelesaiPekerjaan;
                myData.UnitKerjaBank = entity.UnitKerjaBank;
                myData.PICBank = entity.PICBank;
                myData.Keterangan = entity.Keterangan;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxDetailPekerjaanBLGs.Find(id);
            if (myData != null)
            {
                ctx.trxDetailPekerjaanBLGs.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}