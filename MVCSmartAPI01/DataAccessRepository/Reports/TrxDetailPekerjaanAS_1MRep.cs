using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;
using System.Linq.Dynamic;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxDetailPekerjaanAS_1MRep : IDataAccessRepository<trxDetailPekerjaanAS_1M, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxDetailPekerjaanAS_1M> Get()
        {
            return ctx.trxDetailPekerjaanAS_1M.ToList();
        }
        //Get Specific Data based on Id
        public trxDetailPekerjaanAS_1M Get(int id)
        {
            return ctx.trxDetailPekerjaanAS_1M.Find(id);
        }
        public IEnumerable<trxDetailPekerjaanAS_1M> GetByRekanan(Guid IdRekanan)
        {
            return ctx.trxDetailPekerjaanAS_1M.Where(x => x.IdRekanan.Equals(IdRekanan)).ToList();
        }
        public IEnumerable<fGetLapAS1MByRekanan_Result> GetLapAS1MByRekanan(Guid IdRekanan)
        {
            return ctx.fGetLapAS1MByRekanan(IdRekanan).ToList();
        }
        public IEnumerable<fPekerjaanAS_1MByTypeOfRekanan_Result> PekerjaanByTypeOfRekanan(int IdTypeOfRekanan)
        {
            IEnumerable<fPekerjaanAS_1MByTypeOfRekanan_Result> myDataList = ctx.fPekerjaanAS_1MByTypeOfRekanan(IdTypeOfRekanan).ToList();
            return myDataList;
        }
        public IEnumerable<fPekerjaanAS_1MByTypeOfRekanan_Result> XLS_PekerjaanByTypeOfRek(int intTypeOfRekanan, string strFilterExp1, string strFilterExp2)
        {
            string strError = string.Empty;
            List<fPekerjaanAS_1MByTypeOfRekanan_Result> myDataList = new List<fPekerjaanAS_1MByTypeOfRekanan_Result>();
            try
            {
                var query1 = (from excelSmart in ctx.fPekerjaanAS_1MByTypeOfRekanan(intTypeOfRekanan) select excelSmart).AsQueryable().Where(strFilterExp1);
                var query2 = (from excelSmart in query1 select excelSmart).AsQueryable().Where(strFilterExp2);
                myDataList = query2.ToList<fPekerjaanAS_1MByTypeOfRekanan_Result>();
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            return myDataList;
        }

        public IEnumerable<trxDetailPekerjaanAS_1M> GetByGuidHeader(Guid GuidHeader)
        {
            IEnumerable<trxDetailPekerjaanAS_1M> myDataList = new List<trxDetailPekerjaanAS_1M>();
            try
            {
                myDataList = ctx.trxDetailPekerjaanAS_1M.Where(x => x.GuidHeader.Equals(GuidHeader)).ToList();
            }
            catch(Exception ex)
            {
                string err = ex.Message;
            }
            return myDataList;
        }
        //Create a new Data
        public void Post(trxDetailPekerjaanAS_1M entity)
        {
            ctx.trxDetailPekerjaanAS_1M.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxDetailPekerjaanAS_1M entity)
        {
            var myData = ctx.trxDetailPekerjaanAS_1M.Find(id);
            if (myData != null)
            {
                myData.IdRekanan = entity.IdRekanan;
                myData.TahunLaporan = entity.TahunLaporan;
                myData.BulanLaporan = entity.BulanLaporan;
                myData.IdSegmentation = entity.IdSegmentation;
                myData.IdRegion = entity.IdRegion;
                myData.NomorPolis = entity.NomorPolis;
                myData.NamaDebitur = entity.NamaDebitur;
                myData.ProdukAsuransi = entity.ProdukAsuransi;
                myData.Premi = entity.Premi;
                myData.FeeBasedPercent = entity.FeeBasedPercent;
                myData.FeeBasedNominal = entity.FeeBasedNominal;
                myData.PPnNominal = entity.PPnNominal;
                myData.PICGroup = entity.PICGroup;
                myData.DirectIndirect = entity.DirectIndirect;
                myData.Keterangan = entity.Keterangan;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxDetailPekerjaanAS_1M.Find(id);
            if (myData != null)
            {
                ctx.trxDetailPekerjaanAS_1M.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}