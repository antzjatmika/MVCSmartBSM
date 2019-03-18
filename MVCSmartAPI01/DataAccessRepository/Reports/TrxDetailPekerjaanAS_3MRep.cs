using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;
using System.Linq.Dynamic;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxDetailPekerjaanAS_3MRep : IDataAccessRepository<trxDetailPekerjaanAS_3M, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxDetailPekerjaanAS_3M> Get()
        {
            return ctx.trxDetailPekerjaanAS_3M.ToList();
        }
        //Get Specific Data based on Id
        public trxDetailPekerjaanAS_3M Get(int id)
        {
            return ctx.trxDetailPekerjaanAS_3M.Find(id);
        }
        public IEnumerable<trxDetailPekerjaanAS_3M> GetByRekanan(Guid IdRekanan)
        {
            return ctx.trxDetailPekerjaanAS_3M.Where(x => x.IdRekanan.Equals(IdRekanan)).ToList();
        }
        public IEnumerable<trxDetailPekerjaanAS_3M> GetByRekananXLS(Guid IdRekanan, string strFilterExp1, string strFilterExp2)
        {
            string strError = string.Empty;
            List<trxDetailPekerjaanAS_3M> myDataList = new List<trxDetailPekerjaanAS_3M>();
            try
            {
                var query1 = (from excelSmart in ctx.trxDetailPekerjaanAS_3M.Where(x => x.IdRekanan.Equals(IdRekanan)) select excelSmart).AsQueryable().Where(strFilterExp1);
                var query2 = (from excelSmart in query1 select excelSmart).AsQueryable().Where(strFilterExp2);
                myDataList = query2.ToList<trxDetailPekerjaanAS_3M>();
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            return myDataList;
        }
        public IEnumerable<fGetPekerjaan3MByIdTypeOfRekanan_Result> PekerjaanByTypeOfRekanan(int IdTypeOfRekanan)
        {
            //IEnumerable<fPekerjaanAS_3MByTypeOfRekanan_Result> myDataList = ctx.fPekerjaanAS_3MByTypeOfRekanan(IdTypeOfRekanan).ToList();
            IEnumerable<fGetPekerjaan3MByIdTypeOfRekanan_Result> myDataList = ctx.fGetPekerjaan3MByIdTypeOfRekanan(IdTypeOfRekanan).ToList();
            return myDataList;
        }
        public IEnumerable<fPekerjaanAS_3MByTypeOfRekanan_Result> XLS_PekerjaanByTypeOfRek(int intTypeOfRekanan, string strFilterExp1, string strFilterExp2)
        {
            string strError = string.Empty;
            List<fPekerjaanAS_3MByTypeOfRekanan_Result> myDataList = new List<fPekerjaanAS_3MByTypeOfRekanan_Result>();
            try
            {
                var query1 = (from excelSmart in ctx.fPekerjaanAS_3MByTypeOfRekanan(intTypeOfRekanan) select excelSmart).AsQueryable().Where(strFilterExp1);
                var query2 = (from excelSmart in query1 select excelSmart).AsQueryable().Where(strFilterExp2);
                myDataList = query2.ToList<fPekerjaanAS_3MByTypeOfRekanan_Result>();
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            return myDataList;
        }
        //Create a new Data
        public void Post(trxDetailPekerjaanAS_3M entity)
        {
            ctx.trxDetailPekerjaanAS_3M.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxDetailPekerjaanAS_3M entity)
        {
            var myData = ctx.trxDetailPekerjaanAS_3M.Find(id);
            if (myData != null)
            {
                myData.IdRekanan = entity.IdRekanan;
                myData.IdRegion = entity.IdRegion;
                myData.TahunLaporan = entity.TahunLaporan;
                myData.TriwulanLaporan = entity.TriwulanLaporan;
                myData.NamaAsuransi = entity.NamaAsuransi;
                myData.JenisAsuransi = entity.JenisAsuransi;
                myData.LabaRugiKomprehensif = entity.LabaRugiKomprehensif;
                myData.CadanganTeknis = entity.CadanganTeknis;
                myData.HasilInvestasi = entity.HasilInvestasi;
                myData.LabaBeforeTax = entity.LabaBeforeTax;
                myData.PremiNetto = entity.PremiNetto;
                myData.RBCPercent = entity.RBCPercent;
                myData.TotalEkuitasSebelumnya = entity.TotalEkuitasSebelumnya;
                myData.TotalEkuitasBerjalan = entity.TotalEkuitasBerjalan;
                myData.RataRataEkuitas = entity.RataRataEkuitas;
                myData.Keterangan = entity.Keterangan;
                myData.FileExt = entity.FileExt;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxDetailPekerjaanAS_3M.Find(id);
            if (myData != null)
            {
                ctx.trxDetailPekerjaanAS_3M.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}