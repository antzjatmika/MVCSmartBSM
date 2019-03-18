//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCSmartClient01.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class rptAsuransi
    {
        [Display(Name = "ID Report Asuransi")]
        public int IdRptAsuransi { get; set; }
        [Display(Name = "Id Rekanan")]
        public System.Guid IdRekanan { get; set; }
        [Display(Name = "Jenis Asuransi")]
        public int r_IdJenisAsuransi { get; set; }
        [Display(Name = "Cadangan Teknis")]
        public int r_CadanganTeknis { get; set; }
        [Display(Name = "Total Investasi")]
        public int r_TotalInvestasi { get; set; }
        [Display(Name = "Hasil Investasi")]
        public int r_HasilInvestasi { get; set; }
        [Display(Name = "Laba Before Tax")]
        public int r_LabaBeforeTax { get; set; }
        [Display(Name = "Premi Netto")]
        public int r_PremiNetto { get; set; }
        [Display(Name = "Premi Bruto Tahun Ini")]
        public int r_PremiBruttoPrevY { get; set; }
        [Display(Name = "Premi Bruto Tahun Lalu")]
        public int r_PremiBruttoCurY { get; set; }
        [Display(Name = "Total Aktiva")]
        public int r_TotalAktiva { get; set; }
        [Display(Name = "Aktiva Lancar")]
        public int r_AktivaLancar { get; set; }
        [Display(Name = "Kewajiban Lancar")]
        public int r_KewajibanLancar { get; set; }
        [Display(Name = "Beban Komisi Bersih")]
        public int r_BebanKomisiBersih { get; set; }
        [Display(Name = "Beban Usaha")]
        public int r_BebanUsaha { get; set; }
        [Display(Name = "Tanah dan Bangunan")]
        public int r_TanahBangunan { get; set; }
        [Display(Name = "Jumlah Kewajiban Kepada Pemegang Polis")]
        public int r_JumlahKewajibanPolis { get; set; }
        [Display(Name = "Jumlah Klaim dan Manfaat")]
        public int r_JumlahKlaimManfaat { get; set; }
        [Display(Name = "")]
        public int r_LabaRugi { get; set; }
        [Display(Name = "RBC Q4 Tahun Ini")]
        public int r_RBC_Q4 { get; set; }
        [Display(Name = "Total Ekuitas Tahun Lalu")]
        public int r_TotalEkuitasPrevY { get; set; }
        [Display(Name = "Total Ekuitas Tahun Ini")]
        public int r_TotalEkuitasCurY { get; set; }
        [Display(Name = "Jaringan Kantor")]
        public int r_JaringanKantor { get; set; }
        [Display(Name = "Kapasitas Treaty")]
        public int r_KapasitasTreaty { get; set; }
        [Display(Name = "Rasio Dana Investasi Group")]
        public int r_RasioDanaInvestasi { get; set; }
        [Display(Name = "Reputasi Umum")]
        public int a_IdReputasiUmum { get; set; }
        [Display(Name = "Karakter Bonafiditas Owner - Umum")]
        public int a_IdKarakterBonafiditasUmum { get; set; }
        [Display(Name = "Kinerjas Pembayaran Klain - Umum dan Jiwa")]
        public int a_IdKinerjaPembayaranKlaim { get; set; }
        [Display(Name = "Pelayanan Umum dan Jiwa")]
        public int a_IdPelayananUmumJiwa { get; set; }
        [Display(Name = "Portfolio Bank Lain - Umum dan Jiwa")]
        public int a_IdPortfolioBankLain { get; set; }
        [Display(Name = "Spesialisasi Bisnis - Umum dan Jiwa")]
        public int a_IdSpesialisasiBisnisUmum { get; set; }
        [Display(Name = "Dukungan Lembaga Penunjang - Umum dan Jiwa")]
        public int a_IdDukunganLembagaP { get; set; }
        [Display(Name = "Reputasi Jiwa")]
        public int a_IdReputasiJiwa { get; set; }
        [Display(Name = "Karakter dan Bonafiditas Owner - Jiwa")]
        public int a_IdKarakterBonafiditasJiwa { get; set; }
        [Display(Name = "Rating - Jiwa")]
        public int a_IdRatingJiwa { get; set; }
        [Display(Name = "Jenis Score Kualitatif")]
        public int a_IdJenisScoreKualitatif { get; set; }
    }
}
