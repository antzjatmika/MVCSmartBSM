namespace MVCSmartClient01.Models
{
    using System;
    using System.Collections.Generic;

    public partial class trxNotarisDetail
    {
        public int IdNotarisDetail { get; set; }
        public System.Guid IdRekanan { get; set; }
        public Nullable<System.Guid> ImageBaseName { get; set; }
        public string FileNameKoperasi { get; set; }
        public string FileExtKoperasi { get; set; }
        public string FileNamePasarModal { get; set; }
        public string FileExtPasarModal { get; set; }
        public Nullable<bool> IsNotarisKoperasi { get; set; }
        public Nullable<bool> IsNotarisPasarModal { get; set; }
        public string Remark { get; set; }
        public string CreatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }
}
