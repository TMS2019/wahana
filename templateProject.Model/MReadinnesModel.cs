using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace templateProject.Model
{
    
    public partial class MReadinnesModel
    {
       
        public int ReadinessID { get; set; }
        public string Itemname { get; set; }
      //  public string ResultName { get;set;}
        public string Problem { get;set;}
        public string ActionReadiness { get; set; }
        public string Checker { get; set; }
        public string StartReadiness { get; set; }
        public string EndReadiness { get; set; }
        //join
        public int UserID { get; set; }
        public int ResultID { get; set; }
        public int DefactID { get; set; }
        public string UserName { get; set; }
        public string ResultName { get; set; }
        public string DefactName { get; set; }
        
        public bool IsDeleted { get; set; }
        public string UserCreated { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string UserModified { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
       // public int TotalRows { get; set; }
    }

    public enum ResultStatus
    {
        OK=0,
        NOTOK=1,
        OFF=2
    }

  

     
}
