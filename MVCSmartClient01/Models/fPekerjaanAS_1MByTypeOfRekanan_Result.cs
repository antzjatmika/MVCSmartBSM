//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCSmartClient01.Models
{
    using System;
    
    public partial class fPekerjaanAS_1MByTypeOfRekanan_Result
    {
        public int IdDetailPekerjaan { get; set; }
        public System.Guid GuidHeader { get; set; }
        public System.Guid IdRekanan { get; set; }
        public Nullable<int> IdSegmentation { get; set; }
        public Nullable<int> IdRegion { get; set; }
        public int TahunLaporan { get; set; }
        public int BulanLaporan { get; set; }
        public string NomorPolis { get; set; }
        public string NamaDebitur { get; set; }
        public string ProdukAsuransi { get; set; }
        public Nullable<decimal> NilaiPertanggungan { get; set; }
        public decimal Premi { get; set; }
        public decimal FeeBasedPercent { get; set; }
        public decimal FeeBasedNominal { get; set; }
        public decimal PPnNominal { get; set; }
        public string PICGroup { get; set; }
        public string DirectIndirect { get; set; }
        public string Keterangan { get; set; }
        public string CreatedUser { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string RegistrationNumber { get; set; }
        public string Name { get; set; }
    }
}
