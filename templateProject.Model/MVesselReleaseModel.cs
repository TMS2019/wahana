using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace templateProject.Model
{
     public class MVesselReleaseModel
    {
        public int ID_VesselRelease { get; set; }
        [Display(Name = "Depature title")]
        [Required(ErrorMessage = "Depature title is required!")]
        public string Departuretitle { get; set; }
        [Display(Name = "Depature Content")]
        [Required(ErrorMessage = "Depature Content is required!")]
        public string DepartureContent { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public bool IsDeleted { get; set; }
        public string UserCreated { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string UserModified { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public int TotalRows { get; set; }
    }
}

