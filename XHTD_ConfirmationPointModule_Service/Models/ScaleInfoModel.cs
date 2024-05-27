using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XHTD_ConfirmationPointModule_Service.Models
{
    public class ScaleInfoModel
    {
        public DateTime CreatedOn { get; set; }
        public int WeightCurrent { get; set; }
        public int AmplitudeOfOscillation { get; set; }
    }
}
