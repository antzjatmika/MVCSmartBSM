using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class RptAsuransiRep : IDataAccessRepository<rptAsuransi, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<rptAsuransi> Get()
        {
            return ctx.rptAsuransis.ToList();
        }
        public IEnumerable<rptAsuransi> GetByRekanan(Guid IdRekanan)
        {
            return ctx.rptAsuransis.ToList().Where(x => x.IdRekanan.Equals(IdRekanan));
        }
        //Get Specific Data based on Id
        public rptAsuransi Get(int id)
        {
            return ctx.rptAsuransis.Find(id);
        }

        //Create a new Data
        public void Post(rptAsuransi entity)
        {
            ctx.rptAsuransis.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, rptAsuransi entity)
        {
            throw new NotImplementedException();
        }
        public void PutByRekanan(Guid IdRekanan, rptAsuransi entity)
        {
            var myData = ctx.rptAsuransis.Find(IdRekanan);
            if (myData != null)
            {
                myData.r_IdJenisAsuransi = entity.r_IdJenisAsuransi;
                myData.r_CadanganTeknis = entity.r_CadanganTeknis;
                myData.r_TotalInvestasi = entity.r_TotalInvestasi;
                myData.r_HasilInvestasi = entity.r_HasilInvestasi;
                myData.r_LabaBeforeTax = entity.r_LabaBeforeTax;
                myData.r_PremiNetto = entity.r_PremiNetto;
                myData.r_PremiBruttoPrevY = entity.r_PremiBruttoPrevY;
                myData.r_PremiBruttoCurY = entity.r_PremiBruttoCurY;
                myData.r_TotalAktiva = entity.r_TotalAktiva;
                myData.r_AktivaLancar = entity.r_AktivaLancar;
                myData.r_KewajibanLancar = entity.r_KewajibanLancar;
                myData.r_BebanKomisiBersih = entity.r_BebanKomisiBersih;
                myData.r_BebanUsaha = entity.r_BebanUsaha;
                myData.r_TanahBangunan = entity.r_TanahBangunan;
                myData.r_JumlahKewajibanPolis = entity.r_JumlahKewajibanPolis;
                myData.r_JumlahKlaimManfaat = entity.r_JumlahKlaimManfaat;
                myData.r_LabaRugi = entity.r_LabaRugi;
                myData.r_RBC_Q4 = entity.r_RBC_Q4;
                myData.r_TotalEkuitasPrevY = entity.r_TotalEkuitasPrevY;
                myData.r_TotalEkuitasCurY = entity.r_TotalEkuitasCurY;
                myData.r_JaringanKantor = entity.r_JaringanKantor;
                myData.r_KapasitasTreaty = entity.r_KapasitasTreaty;
                myData.r_RasioDanaInvestasi = entity.r_RasioDanaInvestasi;

                ctx.SaveChanges();
            }
        }
        public void PutByASP(Guid IdRekanan, rptAsuransi entity)
        {
            var myData = ctx.rptAsuransis.Find(IdRekanan);
            if (myData != null)
            {
                myData.a_IdReputasiUmum = entity.a_IdReputasiUmum;
                myData.a_IdKarakterBonafiditasUmum = entity.a_IdKarakterBonafiditasUmum;
                myData.a_IdKinerjaPembayaranKlaim = entity.a_IdKinerjaPembayaranKlaim;
                myData.a_IdPelayananUmumJiwa = entity.a_IdPelayananUmumJiwa;
                myData.a_IdPortfolioBankLain = entity.a_IdPortfolioBankLain;
                myData.a_IdSpesialisasiBisnisUmum = entity.a_IdSpesialisasiBisnisUmum;
                myData.a_IdDukunganLembagaP = entity.a_IdDukunganLembagaP;
                myData.a_IdReputasiJiwa = entity.a_IdReputasiJiwa;
                myData.a_IdKarakterBonafiditasJiwa = entity.a_IdKarakterBonafiditasJiwa;
                myData.a_IdRatingJiwa = entity.a_IdRatingJiwa;
                myData.a_IdJenisScoreKualitatif = entity.a_IdJenisScoreKualitatif;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.rptAsuransis.Find(id);
            if (myData != null)
            {
                ctx.rptAsuransis.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}