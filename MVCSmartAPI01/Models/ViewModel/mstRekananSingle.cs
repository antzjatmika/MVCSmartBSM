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
    
    public partial class mstRekananSingle
    {
        public mstRekanan MstRekananSingle { get; set; }
        public IEnumerable<mstTypeOfRegion> TypeOfRegionColls { get; set; }
        public IEnumerable<mstTypeOfRekanan> TypeOfRekananColls { get; set; }
        public IEnumerable<mstTypeOfBadanUsaha> TypeOfBadanUsahaColls { get; set; }
        public IEnumerable<mstWilayah> WilayahColls { get; set; }
        public IEnumerable<mstKecamatan> KecamatanColls { get; set; }
    }
}
