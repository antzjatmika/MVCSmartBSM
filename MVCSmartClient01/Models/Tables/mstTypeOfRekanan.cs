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
    using ApiHelper.Model;

    public partial class mstTypeOfRekanan : ApiModel
    {
        public int IdTypeOfRekanan { get; set; }
        public string Kode { get; set; }
        public string Name { get; set; }
        public string CreatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
