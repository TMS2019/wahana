using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace templateProject.Model
{
    public partial class MMenuModel
    {
        public int MenuID { get; set; }
        public Nullable<int> ParentMenuID { get; set; }
        public string MenuName { get; set; }
        public string Modul { get; set; }
        public string IconFA { get; set; }
        public bool IsMenu { get; set; }
        public string PageUrl { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> OrderPage { get; set; }
        public string UserCreated { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string UserModified { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
    }
}
