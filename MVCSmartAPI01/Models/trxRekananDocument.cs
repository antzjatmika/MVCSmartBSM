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
    
    public partial class trxRekananDocument
    {
        public int IdDocument { get; set; }
        public System.Guid IdRekanan { get; set; }
        public int IdTypeOfDocument { get; set; }
        public string IssuedBy { get; set; }
        public Nullable<System.DateTime> IssuedDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string Notaris { get; set; }
        public Nullable<System.Guid> ImageBaseName { get; set; }
        public byte[] BlobDocument { get; set; }
        public string CreatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }
}
