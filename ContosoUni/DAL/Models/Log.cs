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
    
    public partial class Log
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Log()
        {
            this.LogPropertyChanges = new HashSet<LogPropertyChanx>();
        }
    
        public System.Guid LogId { get; set; }
        public Nullable<int> ObjectId { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string TypeKey { get; set; }
        public string OperationKey { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Message { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public bool Processed { get; set; }
        public Nullable<System.DateTime> ProcessedDate { get; set; }
        public bool SendEmail { get; set; }
        public bool WriteAsFile { get; set; }
        public bool Representative { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LogPropertyChanx> LogPropertyChanges { get; set; }
    }
}
