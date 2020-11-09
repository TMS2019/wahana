using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace templateProject.Model
{
    public partial class MWageModel
    {

        public int WageID { get; set; }
        [Display(Name = "Wage Name")]
        [Required(ErrorMessage = "Wage Name is required!")]
        public string WageName { get; set; }
        [Display(Name = "Wage Description")]
        [Required(ErrorMessage = "Wage Desc is required!")]
        public string WageDescription { get; set; }
        public bool IsDeleted { get; set; }        
        public string UserCreated { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string UserModified { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        //public int TotalRows { get; set; }

    }


}
