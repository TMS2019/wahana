using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace templateProject.Model
{
    public partial class MPlantModel
    {
        public int PlantID { get; set; }
        [Display(Name = "Plant Name")]
        public string PlantName { get; set; }
        [Display(Name = "plant Desc")]
        public string PlantDescription { get; set; }
        public bool IsDeleted { get; set; }
        public string UserCreated { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string UserModified { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public int TotalRows { get; set; }
    }
}
