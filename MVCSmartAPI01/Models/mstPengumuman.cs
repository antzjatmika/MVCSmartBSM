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
    
    public partial class mstPengumuman
    {
        public int Id { get; set; }
        public int IdTypeOfRekanan { get; set; }
        public string JudulPengumuman { get; set; }
        public string IsiPengumuman { get; set; }
        public Nullable<System.DateTime> MulaiAktif { get; set; }
        public Nullable<System.DateTime> SelesaiAktif { get; set; }
        public Nullable<System.Guid> ImageName { get; set; }
        public string FileExt { get; set; }
        public string CreatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
