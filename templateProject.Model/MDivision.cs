using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace templateProject.Model
{
   public class MDivision
    {

        public int CompanyID { get; set; }
        [Display(Name = "Company ID")]
        [Required(ErrorMessage = "Wage Name is required!")]
        public string Division { get; set; }
        [Display(Name = "Division")]
        [Required(ErrorMessage = "Wage Desc is required!")]
        //public string WageDescription { get; set; }
        //public bool IsDeleted { get; set; }
        //public string UserCreated { get; set; }
        //public Nullable<System.DateTime> DateCreated { get; set; }
        //public string UserModified { get; set; }
        //public Nullable<System.DateTime> DateModified { get; set; }
        public int TotalRows { get; set; }

        //public int TotalRows { get; set; }

    }
}
