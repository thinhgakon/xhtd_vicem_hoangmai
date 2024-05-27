using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMXHTD.Data.Models
{
    public class ScaleInfoRealTimeModel
    {
        public bool IsScalling { get; set; } = false;
        public DateTime ModifiedOn { get; set; } = DateTime.Now;
    }
}
