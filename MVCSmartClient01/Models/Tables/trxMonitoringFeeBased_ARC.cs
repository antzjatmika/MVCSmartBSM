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
    
    public partial class trxMonitoringFeeBased_ARC
    {
        public int IdArc { get; set; }
        public int IdFeeBased { get; set; }
        public System.Guid IdRekanan { get; set; }
        public int IdRegion { get; set; }
        public int IdArea { get; set; }
        public int IdCabang { get; set; }
        public string NomorPolis { get; set; }
        public string Produk { get; set; }
        public decimal NilaiPertanggungan { get; set; }
        public int Periode { get; set; }
        public decimal NilaiPremi { get; set; }
        public decimal NilaiFeeBased { get; set; }
        public Nullable<decimal> NilaiPPNBasedNet { get; set; }
        public Nullable<decimal> NilaiPPNBased { get; set; }
        public Nullable<decimal> PercentFeeBased { get; set; }
        public int IdChannel { get; set; }
        public bool SudahBayar { get; set; }
    }
}
