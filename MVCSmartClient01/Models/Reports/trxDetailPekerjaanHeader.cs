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

    public partial class trxDetailPekerjaanHeader
    {
        public int IdPekerjaanHeader { get; set; }
        public System.Guid GuidHeader { get; set; }
        public int IdTipePekerjaan { get; set; }
        public System.Guid IdRekanan { get; set; }
        public string JudulDokumen { get; set; }
        public string CreatedUser { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public bool IsNotaris { get; set; }
        public bool IsAsuransi { get; set; }
        public bool IsGrupNonNotaris { get; set; }
        public IEnumerable<fPekerjaanByTypeOfRekanan_Result> DetailPekerjaanColls { get; set; }
        public IEnumerable<fPekerjaanAS_1MByTypeOfRekanan_Result> DetailPekerjaanAS_1MColls { get; set; }
        public IEnumerable<fPekerjaanAS_3MByTypeOfRekanan_Result> DetailPekerjaanAS_3MColls { get; set; }
        public IEnumerable<fGetPekerjaan3MByIdTypeOfRekanan_Result> DetailPekerjaanAS_3MPCPColls { get; set; }
        public IEnumerable<fPekerjaanBLGByTypeOfRekanan_Result> DetailPekerjaanBLGColls { get; set; }
        public IEnumerable<mstReference> TypeTotalAsetColls { get; set; }
        public IEnumerable<mstReference> TriwulanColls { get; set; }
        public IEnumerable<mstReference> TingkatResikoColls { get; set; }
        public IEnumerable<mstSubRegion> SubRegionColls { get; set; }
        public IEnumerable<mstSegmentasi> TypeOfSegmentasi3Colls { get; set; }
        public IEnumerable<mstSegmentasi> TypeOfSegmentasi5Colls { get; set; }
    }
}