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
    
    public partial class trxDetailPekerjaanHeader
    {
        public int IdPekerjaanHeader { get; set; }
        public System.Guid GuidHeader { get; set; }
        public int IdTipePekerjaan { get; set; }
        public System.Guid IdRekanan { get; set; }
        public string JudulDokumen { get; set; }
        public string CreatedUser { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}
