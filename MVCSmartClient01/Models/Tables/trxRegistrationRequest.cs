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
    
    public partial class trxRegistrationRequest
    {
        public int IdRegRequest { get; set; }
        public string NamaLengkap { get; set; }
        public string NomorKTP { get; set; }
        public string NomorNPWP { get; set; }
        public string AlamatEmail { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int IsFollowedUp { get; set; }
        public System.DateTime FollowedUpDate { get; set; }
    }
}
