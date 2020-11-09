using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace templateProject.Model
{
    public partial class MVoyageModel
    {
        [Display(Name = "Voyage No")]
        public int VoyageID { get; set; }
        public int VesselID { get; set; }
        [Display(Name = "Vessel Name")]
        public string VesselName { get; set; }
        [Display(Name = "Voyage Name")]
        public string NameVoyage { get; set; }
        [Display(Name = "Voyage Desc")]
        public string VoyageDescription { get; set; }
        public bool VoyageStatus { get; set; }
        public bool IsDeleted { get; set; }
        public string UserCreated { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string UserModified { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
    }
}
