using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace templateProject.Model
{
    public partial class MDefactModel
    {
        public int DefactID { get; set; }
        [Required(ErrorMessage = "Defact name is required!")]
        [Display(Name = "Defact Name")]
        public string DefactName { get; set; }
        [Required(ErrorMessage = "Defact Desc is required!")]
        [Display(Name = "Defact Desc")]
        public string DefactDesc { get; set; }
        public bool IsDeleted { get; set; }
        public string UserCreated { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string UserModified { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public int TotalRows { get; set; }
    }
}
