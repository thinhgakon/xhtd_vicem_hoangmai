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
    
    public partial class tblVehicle
    {
        public int IDVehicle { get; set; }
        public Nullable<int> IDStore { get; set; }
        public string Vehicle { get; set; }
        public Nullable<double> Tonnage { get; set; }
        public Nullable<double> TonnageDefault { get; set; }
        public string NameDriver { get; set; }
        public string IdCardNumber { get; set; }
        public string HeightVehicle { get; set; }
        public string WidthVehicle { get; set; }
        public string LongVehicle { get; set; }
        public Nullable<System.DateTime> DayCreate { get; set; }
        public Nullable<System.DateTime> DayUpdate { get; set; }
        public string UserCreate { get; set; }
        public string UserUpdate { get; set; }
        public Nullable<int> UnladenWeight1 { get; set; }
        public Nullable<int> UnladenWeight2 { get; set; }
        public Nullable<int> UnladenWeight3 { get; set; }
        public Nullable<bool> IsSetMediumUnladenWeight { get; set; }
    }
}
