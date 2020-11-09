using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace templateProject.Model
{
    public partial class MShiftModel
    {
        public int ShiftID { get; set; }
        [Display(Name = "Shift Name")]
        [Required(ErrorMessage = "Shift Name is required!")]
        public string ShiftName { get; set; }
        [Display(Name = "Shift Deskripsi")]
        [Required(ErrorMessage = "Shift Deskripsi is required!")]
        public string ShiftDeskripsi { get; set; }
       
        public bool IsDeleted { get; set; }
        public string UserCreated { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string UserModified { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public int TotalRows { get; set; }
    }

    
}
