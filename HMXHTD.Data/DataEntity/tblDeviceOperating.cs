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
    
    public partial class tblDeviceOperating
    {
        public int Id { get; set; }
        public Nullable<int> GroupDevice { get; set; }
        public string GroupDeviceCode { get; set; }
        public string GroupDeviceName { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public Nullable<int> PortNumber { get; set; }
        public Nullable<bool> State { get; set; }
        public Nullable<System.DateTime> DayCreate { get; set; }
        public string LogHistory { get; set; }
        public Nullable<int> Flag { get; set; }
    }
}
