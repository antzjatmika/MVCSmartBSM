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
    using System.Collections.Generic;
    
    public partial class mstRekanan
    {
        public System.Guid IdRekanan { get; set; }
        public int IdRegion { get; set; }
        public string RegistrationNumber { get; set; }
        public int ClassOfRekanan { get; set; }
        public int IdTypeOfRekanan { get; set; }
        public int IdTypeOfBadanUsaha { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Nullable<int> IdWilayah { get; set; }
        public string Kelurahan { get; set; }
        public string Kecamatan { get; set; }
        public string Kota { get; set; }
        public string ZipCode { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string Fax1 { get; set; }
        public string Fax2 { get; set; }
        public string EmailAddress { get; set; }
        public string PenerbitRating { get; set; }
        public string NilaiRating { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public System.DateTime LMDate { get; set; }
        public string Note { get; set; }
        public string CreatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public byte IsActive { get; set; }
    }
}
