using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace templateProject.Model
{
  public class MResultModel
    {
        public int ResultID { get; set; }
        [Display(Name = "Shift Name")]
        [Required(ErrorMessage = "Shift Name is required!")]
        public string ResultName { get; set; }

        public bool IsDeleted { get; set; }
        public string UserCreated { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string UserModified { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public int TotalRows { get; set; }
    }
}
