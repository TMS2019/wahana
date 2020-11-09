using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace templateProject.Model
{
    public partial class MActivityModel
    {
        public int IDActivity { get; set; }
        public string HatchNo { get; set; }
        public string LoaderUnitStart { get; set; }
        public string LoaderUnitEnd { get; set; }
        public string DetailActivity { get; set; }
        public string ProblemIdentified { get; set; }
        public string Action { get; set; }
        public string LoadQtyPrevious { get; set; }
        public string LoadQtyToday { get; set; }
        public string LoadQtyCummulative { get; set; }
        public string LoadQtyRemaining { get; set; }

        public bool IsDeleted { get; set; }
        public string UserCreated { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string UserModified { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public int TotalRows { get; set; }
    }

    public partial class MActivityGroupLoader
    {
        public int ID_ActivityLoader { get; set; }
        public string HatchNo { get; set; }
        public string LoaderUnitStart { get; set; }
        public string LoaderUnitEnd { get; set; }
        public string DetailActivity { get; set; }
        public string ProblemIdentified { get; set; }
        public string Action { get; set; }
        public string LoadQtyPrevious { get; set; }
        public string LoadQtyToday { get; set; }
        public string LoadQtyCummulative { get; set; }
        public string LoadQtyRemaining { get; set; }

        public bool IsDeleted { get; set; }
        public string UserCreated { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string UserModified { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public int TotalRows { get; set; }
    }

    public partial class MActivityGroupUnloader
    {
        public int IDActivity { get; set; }
        public string HatchNo { get; set; }
        public string LoaderUnitStart { get; set; }
        public string LoaderUnitEnd { get; set; }
        public string DetailActivity { get; set; }
        public string ProblemIdentified { get; set; }
        public string Action { get; set; }
        public string LoadQtyPrevious { get; set; }
        public string LoadQtyToday { get; set; }
        public string LoadQtyCummulative { get; set; }
        public string LoadQtyRemaining { get; set; }

        public bool IsDeleted { get; set; }
        public string UserCreated { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string UserModified { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public int TotalRows { get; set; }
    }

    public partial class MActivityGroupUnloader2
    {
        public int ID_ActivityUnloader { get; set; }
        public string HatchNo { get; set; }
        public string LoaderUnitStart { get; set; }
        public string LoaderUnitEnd { get; set; }
        public string DetailActivity { get; set; }
        public string ProblemIdentified { get; set; }
        public string Action { get; set; }
        public string LoadQtyPrevious { get; set; }
        public string LoadQtyToday { get; set; }
        public string LoadQtyCummulative { get; set; }
        public string LoadQtyRemaining { get; set; }

        public bool IsDeleted { get; set; }
        public string UserCreated { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string UserModified { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public int TotalRows { get; set; }
    }
   

   
}
