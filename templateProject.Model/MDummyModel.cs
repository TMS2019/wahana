using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace templateProject.Model
{
    public partial class MDummyModel
    {
       
        public int DummyID { get; set; }
        [Display(Name = "Dummy Name")]
        [Required(ErrorMessage = "Dummy Description is required!")]
        public string DummyName { get; set; }
        [Display(Name = "Dummy Description ")]
        [Required(ErrorMessage = "User Name is required!")]
        public string DummyDescription { get; set; }
        public int VesselID { get; set; }
        public string VesselName { get; set; }
        public string VesselDescription { get; set; }
        public bool IsDeleted { get; set; }
        public string UserCreated { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string UserModified { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public int TotalRows { get; set; }
    }

  

}
