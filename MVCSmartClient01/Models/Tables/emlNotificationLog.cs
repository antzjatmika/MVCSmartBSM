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
    
    public partial class emlNotificationLog
    {
        public int IdNotificationLog { get; set; }
        public int IdNotification { get; set; }
        public Nullable<int> SendAttemptSeq { get; set; }
        public Nullable<System.DateTime> SendAttemptDate { get; set; }
        public string LogMessage { get; set; }
        public string CreatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }
}