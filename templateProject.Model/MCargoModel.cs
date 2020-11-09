using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace templateProject.Model
{
    public partial class MCargoModel
    {
        public int CargoID { get; set; }
        [Display(Name = "Cargo Name")]
        [Required(ErrorMessage = "Cargo Name is required!")]
        public string CargoName { get; set; }
        [Display(Name = "Cargo Deskripsi")]
        [Required(ErrorMessage = "Cargo Deskripsi is required!")]
        public string CargoDescription { get; set; }
        public bool IsDeleted { get; set; }
        public string UserCreated { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string UserModified { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public int TotalRows { get; set; }
    }

    
}
