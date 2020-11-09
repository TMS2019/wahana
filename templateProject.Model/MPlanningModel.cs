using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace templateProject.Model
{
    public partial class MPlanningModel
    {
        public int BlID { get; set; }
        [Display(Name = "BL Date")]
        [Required(ErrorMessage = "BL Date is required!")]
        [DataType(DataType.Date)]
        public string BLDate { get; set; }
        [Display(Name = "BLQty")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "BL Qty must be a natural number")]
        public string BLQty { get; set; }
        [Display(Name = "PONo")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Po No must be a natural number")]
        public string PoNo { get; set; }
        [DataType(DataType.Date)]
        public string PoDate { get; set; }
        [Display(Name = "POQty")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Po Qty must be a natural number")]
        public string PoQty { get; set; }
        [Display(Name = "Material Code")]
        [Required(ErrorMessage = "Material Code is required!")]
        public string MaterialCode { get; set; }
        [Display(Name = "Material Desc")]
        [Required(ErrorMessage = "Material Desc is required!")]
        public string MaterialDescription { get; set; }
        [Display(Name = "Uom")]
        [Required(ErrorMessage = "Uom is required!")]
        public string Uom { get; set; }
        [Display(Name = "BatchCode")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Batch Code must be a natural number")]
        public string BatchCode { get; set; }
        [Display(Name = "Port Of Origin")]
        [Required(ErrorMessage = "Port Of Origin is required!")]
        public string PortOfOrigin { get; set; }
        [Display(Name = "Port Of Discharge")]
        [Required(ErrorMessage = "Port Of Discharge is required!")]
        public string PortOfDischarge { get; set; }
        [Display(Name = "Wage No")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Wage Number must be a natural number")]
        public string Wage { get; set; }

        public bool IsDeleted { get; set; }
        public Nullable<int> OrderPage { get; set; }
        public string UserCreated { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string UserModified { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public int TotalRows { get; set; }
    }

 }
