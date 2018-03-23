//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LogPropertyChanx
    {
        public System.Guid LogPropertyChangeId { get; set; }
        public System.Guid LogId { get; set; }
        public string PropertyKey { get; set; }
        public string PreviousValue { get; set; }
        public string NewValue { get; set; }
        public string EncryptionKey { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual Log Log { get; set; }
    }
}
