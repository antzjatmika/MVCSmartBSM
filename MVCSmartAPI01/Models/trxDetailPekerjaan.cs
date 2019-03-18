//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCSmartAPI01.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class trxDetailPekerjaan
    {
        public int IdDetailPekerjaan { get; set; }
        public System.Guid GuidHeader { get; set; }
        public System.Guid IdRekanan { get; set; }
        public int IdRegion { get; set; }
        public int IdSegmentasi { get; set; }
        public Nullable<int> TahunLaporan { get; set; }
        public Nullable<int> BulanLaporan { get; set; }
        public string DebiturName { get; set; }
        public string DebiturLocation { get; set; }
        public string BidangUsahaDebitur { get; set; }
        public string JenisJasa { get; set; }
        public string NomorLaporan { get; set; }
        public string PenanggungJawab { get; set; }
        public string NamaPemberiPekerjaan { get; set; }
        public string UnitKerja { get; set; }
        public string TotalAsetPerusahaan { get; set; }
        public Nullable<decimal> FeeNominal { get; set; }
        public Nullable<decimal> NilaiPenutupan { get; set; }
        public Nullable<System.DateTime> TanggalMulaiPekerjaan { get; set; }
        public Nullable<System.DateTime> TanggalSelesaiPekerjaan { get; set; }
        public Nullable<int> TahunBuku { get; set; }
        public string NomorPolis { get; set; }
        public string PICRekanan { get; set; }
        public string PICBank { get; set; }
        public string Keterangan { get; set; }
        public Nullable<long> LimitKreditDiOrder { get; set; }
        public string CreatedUser { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}
