using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace templateProject.Model
{
    public partial class MMaterialModel
    {
        //MaterialID MaterialName    MaterialDescription IsDeleted   UserCreated DateCreated UserModified DateModified
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public int UomID { get; set; }
        public string UomName {get;set;}
        public int VesselID { get; set; }
        public string VesselName { get; set; }
        public int VoyageID { get; set; }
        public string VoyageName { get; set; }
        public string MaterialDescription { get; set; }
        public int TotalVoyage { get; set; } 
        public bool IsDeleted { get; set; }
        public Nullable<int> OrderPage { get; set; }
        public string UserCreated { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string UserModified { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
    }
}
