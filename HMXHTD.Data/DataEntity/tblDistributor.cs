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
    
    public partial class tblDistributor
    {
        public int IDDistributor { get; set; }
        public string CodeStore { get; set; }
        public Nullable<int> IDDistributorSyn { get; set; }
        public string CodeDistributor { get; set; }
        public string NameDistributor { get; set; }
        public string AddressDistributor { get; set; }
        public string PhoneDistributor { get; set; }
        public string EmailDistributor { get; set; }
        public string SMSDistributor { get; set; }
        public Nullable<int> IDProvince { get; set; }
        public string NameProvince { get; set; }
        public Nullable<int> IDDistrict { get; set; }
        public string NameDistrict { get; set; }
        public Nullable<int> IDWard { get; set; }
        public string NameWard { get; set; }
        public Nullable<int> TypeOrder { get; set; }
        public Nullable<bool> State { get; set; }
        public Nullable<bool> DisCount { get; set; }
        public Nullable<System.DateTime> DaySyn { get; set; }
        public Nullable<bool> ExportStoreDistributor { get; set; }
        public Nullable<bool> CreateOrderExport { get; set; }
        public Nullable<bool> LockInDbet { get; set; }
    }
}
