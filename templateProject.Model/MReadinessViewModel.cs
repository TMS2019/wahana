using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace templateProject.Model
{
   public class MReadinessViewModel
    {
        public MReadinnesModel Readiness { get; set; }
        public MVesselModel Vessel { get; set; }
        public MVoyageModel Voyage { get; set; } 
        public MShiftModel Shift { get; set; }
        public MResultModel result { get; set; }
    }
}
