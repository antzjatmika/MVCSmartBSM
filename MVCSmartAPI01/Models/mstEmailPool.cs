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
    
    public partial class mstEmailPool
    {
        public int PoolId { get; set; }
        public System.Guid IdRekanan { get; set; }
        public string JudulEmail { get; set; }
        public string IsiEmail { get; set; }
        public string EmailTo { get; set; }
        public string EmailFrom { get; set; }
        public bool SentStatus { get; set; }
        public Nullable<System.DateTime> SentDate { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
    }
}
