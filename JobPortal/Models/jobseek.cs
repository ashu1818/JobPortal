//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JobPortal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class jobseek
    {
        public int JobseekId { get; set; }
        public Nullable<System.DateTime> JobCreatedDate { get; set; }
        public Nullable<int> RefUserId { get; set; }
        public Nullable<int> RefJobId { get; set; }
    
        public virtual ManageJob ManageJob { get; set; }
        public virtual UserMaster UserMaster { get; set; }
    }
}