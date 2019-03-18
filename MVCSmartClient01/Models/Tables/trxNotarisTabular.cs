namespace MVCSmartClient01.Models
{
    using System;
    using System.Collections.Generic;

    public partial class trxNotarisTabular
    {
        public int IdNotarisTabular { get; set; }
        public System.Guid IdRekanan { get; set; }
        public string SKNotarisNumber { get; set; }
        public Nullable<System.DateTime> SKNotarisDate { get; set; }
        public string SumpahNotarisNumber { get; set; }
        public Nullable<System.DateTime> SumpahNotarisDate { get; set; }
        public string WilayahKerjaNotaris { get; set; }
        public Nullable<System.DateTime> NotarisPensionDate { get; set; }
        public string PPATSKNumber { get; set; }
        public Nullable<System.DateTime> PPATSKDate { get; set; }
        public string PPATSumpahNumber { get; set; }
        public Nullable<System.DateTime> PPATSumpahDate { get; set; }
        public string WilayahKerjaPPAT { get; set; }
        public Nullable<System.DateTime> PPATPensionDate { get; set; }
        public string CreatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }
}