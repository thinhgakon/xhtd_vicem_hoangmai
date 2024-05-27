using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMXHTD_CallVoiceService.Models
{
    public class VehicleAudioModel
    {
        public int Id { get; set; }
        public string Vehicle { get; set; }
        public int IdVehicle { get; set; }
        public bool? IsCreatedAudio { get; set; }
    }
}
