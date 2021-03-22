using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace templateProject.Model
{
    public partial class TransaksiModel
    {
        public int UserID { get; set; }
        [Display(Name = "Official Name")]
        [Required(ErrorMessage = "Official Name is required!")]
        public string OfficialName { get; set; }
        [Display(Name = "User Name ")]
        [Required(ErrorMessage = "User Name is required!")]
        public string UserName { get; set; }
        public string Password { get; set; }
        [Display(Name = "NIK")]
        [Required(ErrorMessage = "NIK is required!")]
        public string Nik { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }
        [Display(Name = "Group User")]
        [Required(ErrorMessage = "Group user is required!")]
        public int? GroupUserID { get; set; }
        [Display(Name = "Group User")]
        public string GroupUserName { get; set; }
        public bool IsDeleted { get; set; }
        public string UserCreated { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string UserModified { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public int TotalRows { get; set; }
    }


}
