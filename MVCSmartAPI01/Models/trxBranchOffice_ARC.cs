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
    
    public partial class trxBranchOffice_ARC
    {
        public int IdArc { get; set; }
        public int IdAction { get; set; }
        public int IdCabang { get; set; }
        public int IdOrganisasi { get; set; }
        public int BranchType { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Nullable<int> IdWilayah { get; set; }
        public Nullable<int> IdKecamatan { get; set; }
        public string ZipCode { get; set; }
        public string CreatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
