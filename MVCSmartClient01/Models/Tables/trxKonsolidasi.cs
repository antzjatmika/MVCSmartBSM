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

    public partial class trxKonsolidasi
    {
        public int IdTrxKonsolidasi { get; set; }
        public int IdMstKonsolidasi { get; set; }
        public System.Guid IdRekanan { get; set; }
        public int TahunBulan { get; set; }
        public decimal NilaiRp { get; set; }
        public Nullable<decimal> NilaiPercent { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
    }
}
