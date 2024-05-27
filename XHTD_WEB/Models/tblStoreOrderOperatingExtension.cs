using HMXHTD.Data.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XHTD_WEB.Models
{
    public class tblStoreOrderOperatingExtension : tblStoreOrderOperating
    {
        public bool? IsSentTrough { get; set; }
        public DateTime? TimeSendTrough { get; set; }
        public string LogCall { get; set; }
    }
}