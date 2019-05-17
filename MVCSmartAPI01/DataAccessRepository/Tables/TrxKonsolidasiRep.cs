using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

using System.Data.Entity.Validation;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxKonsolidasiRep : IDataAccessRepository<trxKonsolidasi, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxKonsolidasi> Get()
        {
            return ctx.trxKonsolidasi.ToList();
        }
        //Get Specific Data based on Id
        public trxKonsolidasi Get(int id)
        {
            return ctx.trxKonsolidasi.Find(id);
        }

        //Create a new Data
        public void Post(trxKonsolidasi entity)
        {
            try
            {
                ctx.trxKonsolidasi.Add(entity);
                ctx.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Console.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }
        }
        //Update Exisiting Data
        public void Put(int id, trxKonsolidasi entity)
        {
            var myData = ctx.trxKonsolidasi.Find(id);
            if (myData != null)
            {
                //myData.Nilai= entity.Nilai;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxKonsolidasi.Find(id);
            if (myData != null)
            {
                ctx.trxKonsolidasi.Remove(myData);
                ctx.SaveChanges();
            }
        }
        public List<fKonsoByPeriode_Result> GetKonsoByPeriode(Guid IdRekanan, int TahunBulan)
        {
            IdRekanan = new Guid();
            List<fKonsoByPeriode_Result> myData = new List<fKonsoByPeriode_Result>();
            myData = ctx.fKonsoByPeriode(IdRekanan, TahunBulan).ToList();
            return myData;
        }
        public List<fKonsoResumeByPeriode_Result> GetKonsoResumeByPeriode(Guid IdRekanan, int PeriodeAkhir, int TipeUraian)
        {
            List<fKonsoResumeByPeriode_Result> myData = new List<fKonsoResumeByPeriode_Result>();
            myData = ctx.fKonsoResumeByPeriode(IdRekanan, PeriodeAkhir, TipeUraian).ToList();
            return myData;
        }
        public List<fKategoriResiko_Result> GetKategoriResikoByPenambah(decimal decPenambah)
        {
            List<fKategoriResiko_Result> myData = new List<fKategoriResiko_Result>();
            myData = ctx.fKategoriResiko(decPenambah).ToList();
            return myData;
        }
        public List<fScoringByPeriode_Result> GetScoringByRekPeriode(Guid IdRekanan, int Periode)
        {
            List<fScoringByPeriode_Result> myData = new List<fScoringByPeriode_Result>();
            myData = ctx.fScoringByPeriode(IdRekanan, Periode).ToList();
            return myData;
        }
        public List<fScoringResumeByRek_Result> GetScoringResumeByRekPeriode(Guid IdRekanan, int Periode)
        {
            List<fScoringResumeByRek_Result> myData = new List<fScoringResumeByRek_Result>();
            myData = ctx.fScoringResumeByRek(IdRekanan, Periode).ToList();
            return myData;
        }
        public scoringResumeMulti GetScoringMultiByRekPeriode(Guid IdRekanan, int Periode)
        {
            scoringResumeMulti myDataResumeAll = new scoringResumeMulti();
            List<fScoringByPeriode_Result> myDataScore = new List<fScoringByPeriode_Result>();
            List<fScoringResumeByRek_Result> myDataResume = new List<fScoringResumeByRek_Result>();
            myDataResumeAll.myDataScore = ctx.fScoringByPeriode(IdRekanan, Periode).ToList();
            myDataResumeAll.myDataResume = ctx.fScoringResumeByRek(IdRekanan, Periode).ToList();

            return myDataResumeAll;
        }
        public List<fResumeRoaByPeriode_Result> GetResumeRoaByRekPeriode(Guid IdRekanan, int Periode)
        {
            List<fResumeRoaByPeriode_Result> myData = new List<fResumeRoaByPeriode_Result>();
            myData = ctx.fResumeRoaByPeriode(IdRekanan, Periode).ToList();
            return myData;
        }
        public List<fKonsoPairByParam_Result> GetKonsoPairByParam(Guid IdRekanan, int Periode)
        {
            List<fKonsoPairByParam_Result> myData = new List<fKonsoPairByParam_Result>();
            myData = ctx.fKonsoPairByParam(IdRekanan, Periode).ToList();
            return myData;
        }
    }
}