//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCSmartAPI01.Models
{
    using System;
    using System.Collections.Generic;

    public partial class mstRekananMap
    {
        public mstRekananMap(Guid myIdRekanan, string myName, decimal? myLatitude, decimal? myLongitude, string KelasColor)
        {
            IdRekanan = myIdRekanan;
            Name = myName;
            Latitude = myLatitude;
            Longitude = myLongitude;
            Kelas = KelasColor;
        }
        public System.Guid IdRekanan { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public string Kelas { get; set; }       
    }
}
