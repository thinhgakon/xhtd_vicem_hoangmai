//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HMXHTD.Data.DataEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class LogStoreOrderOperating
    {
        public int Id { get; set; }
        public string Vehicle { get; set; }
        public string CardNo { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> Step { get; set; }
        public Nullable<bool> InSystem { get; set; }
    }
}