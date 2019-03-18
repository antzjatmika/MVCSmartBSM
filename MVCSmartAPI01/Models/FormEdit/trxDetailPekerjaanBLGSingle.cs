//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCSmartAPI01.Models
{
    using System;
    using System.Collections.Generic;

    public partial class trxDetailPekerjaanBLGSingle
    {
        public int IdDetailPekerjaan { get; set; }
        public Nullable<System.Guid> GuidHeader { get; set; }
        public System.Guid IdRekanan { get; set; }
        public Nullable<int> IdRegion { get; set; }
        public Nullable<int> IdSegmentasi { get; set; }
        public string DebiturName { get; set; }
        public string DebiturLocation { get; set; }
        public string JenisProperty { get; set; }
        public string Lokasi { get; set; }
        public string JenisLelang { get; set; }
        public string NoPerjanjian { get; set; }
        public System.DateTime TanggalPerjanjian { get; set; }
        public long NilaiLimitLelang { get; set; }
        public Nullable<long> NilaiTransaksi { get; set; }
        public Nullable<long> FeeImbalanJasa { get; set; }
        public Nullable<System.DateTime> TanggalMulai { get; set; }
        public System.DateTime TanggalSelesaiPekerjaan { get; set; }
        public string UnitKerjaBank { get; set; }
        public string PICBank { get; set; }
        public string Keterangan { get; set; }
        public string CreatedUser { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public IEnumerable<mstSubRegion> SubRegionColls { get; set; }
        public IEnumerable<mstSegmentasi> TypeOfSegmentasi3Colls { get; set; }
        public IEnumerable<mstSegmentasi> TypeOfSegmentasi5Colls { get; set; }
    }
}
