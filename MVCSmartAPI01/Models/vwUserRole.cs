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
    
    public partial class vwUserRole
    {
        public string id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public int IdSupervisor { get; set; }
        public string RoleId { get; set; }
        public string Name { get; set; }
        public System.Guid IdRekananContact { get; set; }
        public int IdNotaris { get; set; }
        public int IdOrganisasi { get; set; }
        public int IdTypeOfRekanan { get; set; }
    }
}