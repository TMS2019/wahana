using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace templateProject.Model
{
    public partial class MBarangModel
    {
        public int BarangID { get; set; }
        [Display(Name = "Barang ID")]
        [Required(ErrorMessage = "Barang ID is required!")]
        public string NamaBarang { get; set; }
        [Display(Name = "Nama Barang")]
        [Required(ErrorMessage = " Nama Barang is required!")]

        public string JenisBarang { get; set; }
        [Display(Name = "Jenis Barang ")]
        [Required(ErrorMessage = "Jenis Barang is required!")]
       
        public bool IsDeleted { get; set; }
        public string UserCreated { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string UserModified { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public int TotalRows { get; set; }
    }
 
 
}
