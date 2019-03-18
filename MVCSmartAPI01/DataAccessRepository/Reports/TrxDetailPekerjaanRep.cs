using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;
using System.Linq.Dynamic;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxDetailPekerjaanRep : IDataAccessRepository<trxDetailPekerjaan, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxDetailPekerjaan> Get()
        {
            return ctx.trxDetailPekerjaans.ToList();
        }
        //Get Specific Data based on Id
        public trxDetailPekerjaan Get(int id)
        {
            return ctx.trxDetailPekerjaans.Find(id);
        }
        public IEnumerable<trxDetailPekerjaan> GetByRekanan(Guid IdRekanan)
        {
            return ctx.trxDetailPekerjaans.Where(x => x.IdRekanan.Equals(IdRekanan)).ToList();
        }
        public IEnumerable<trxDetailPekerjaan> GetByRekananXLS(Guid IdRekanan, string strFilterExp1, string strFilterExp2)
        {
            string strError = string.Empty;
            List<trxDetailPekerjaan> myDataList = new List<trxDetailPekerjaan>();
            try
            {
                var query1 = (from excelSmart in ctx.trxDetailPekerjaans.Where(x => x.IdRekanan.Equals(IdRekanan)) select excelSmart).AsQueryable().Where(strFilterExp1);
                var query2 = (from excelSmart in query1 select excelSmart).AsQueryable().Where(strFilterExp2);
                myDataList = query2.ToList<trxDetailPekerjaan>();
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            return myDataList;
        }
        public IEnumerable<trxDetailPekerjaan> GetByGuidHeader(Guid GuidHeader)
        {
            return ctx.trxDetailPekerjaans.Where(x => x.GuidHeader.Equals(GuidHeader)).ToList();
        }
        public IEnumerable<fPekerjaanByTypeOfRekanan_Result> PekerjaanByTypeOfRekanan(int IdJenisRekanan)
        {
            IEnumerable<fPekerjaanByTypeOfRekanan_Result> myDataList = ctx.fPekerjaanByTypeOfRekanan(IdJenisRekanan).ToList();
            return myDataList;
        }
        public IEnumerable<fPekerjaanByTypeOfRekanan_Result> XLS_PekerjaanByTypeOfRek(int intTypeOfRekanan, string strFilterExp1, string strFilterExp2)
        {
            string strError = string.Empty;
            List<fPekerjaanByTypeOfRekanan_Result> myDataList = new List<fPekerjaanByTypeOfRekanan_Result>();
            try
            {
                var query1 = (from excelSmart in ctx.fPekerjaanByTypeOfRekanan(intTypeOfRekanan) select excelSmart).AsQueryable().Where(strFilterExp1);
                var query2 = (from excelSmart in query1 select excelSmart).AsQueryable().Where(strFilterExp2);
                myDataList = query2.ToList<fPekerjaanByTypeOfRekanan_Result>();
            }
            catch(Exception ex)
            {
                strError = ex.Message;
            }
            return myDataList;
        }
        //Create a new Data
        public void Post(trxDetailPekerjaan entity)
        {
            ctx.trxDetailPekerjaans.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxDetailPekerjaan entity)
        {
            var myData = ctx.trxDetailPekerjaans.Find(id);
            if (myData != null)
            {
                myData.IdRekanan = entity.IdRekanan;
                myData.IdRegion = entity.IdRegion;
                myData.IdSegmentasi = entity.IdSegmentasi;
                myData.TahunLaporan = entity.TahunLaporan;
                myData.BulanLaporan = entity.BulanLaporan;
                myData.DebiturName = entity.DebiturName;
                myData.DebiturLocation = entity.DebiturLocation;
                myData.BidangUsahaDebitur = entity.BidangUsahaDebitur;
                myData.JenisJasa = entity.JenisJasa;
                myData.NomorLaporan = entity.NomorLaporan;
                myData.PenanggungJawab = entity.PenanggungJawab;
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
                myData.LimitKreditDiOrder = entity.LimitKreditDiOrder;
                myData.TahunBuku = entity.TahunBuku;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxDetailPekerjaans.Find(id);
            if (myData != null)
            {
                ctx.trxDetailPekerjaans.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}