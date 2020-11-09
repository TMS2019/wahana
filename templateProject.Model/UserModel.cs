using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace templateProject.Model
{
    public partial class UserModel
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

    public partial class MUserVM
    {
        public int UserID { get; set; }
        [Display(Name = "Official Name")]
        [Required(ErrorMessage = "Official Name is required!")]
        public string OfficialName { get; set; }
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User Name is required!")]
        public string UserName { get; set; }
        public string Password { get; set; }
        [Display(Name = "NIK")]
        [Required(ErrorMessage = "NIK is required!")]
        public string Nik { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Group user is required!")]
        public string GroupUserID { get; set; }
        [Display(Name = "Group User")]
        public string GroupUserName { get; set; }
        [Display(Name = "Group User")]
        public string GroupUserIDDum { get; set; }
        public bool IsDeleted { get; set; }
        public string UserCreated { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string UserModified { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public int TotalRows { get; set; }
    }

    public partial class MGroupUserModel
    {
        public int GroupUserID { get; set; }

        [Display(Name = "Group Name")]
        [Required(ErrorMessage = "Group user is required!")]
        public string GroupUserName { get; set; }

        [Display(Name = "Superadmin")]
        public bool IsSuperAdmin { get; set; }

        [Display(Name = "Group Code")]
        [Required(ErrorMessage = "Group user is required!")]
        public string GroupCode { get; set; }
        public bool IsDeleted { get; set; }
        public string UserCreated { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string UserModified { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
    }

    public partial class MUserGroupVM
    {
        public int UserGroupID { get; set; }
        public int GroupUserID { get; set; }
        public string GroupUserName { get; set; }
        public bool IsDeleted { get; set; }
        public string UserCreated { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string UserModified { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
    }

    public partial class MUserGroupMemberModel
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Nik { get; set; }
        public string OfficialName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<int> GroupUserID { get; set; }
        public string GroupUserName { get; set; }
        public bool IsSuperAdmin { get; set; }
        public string GroupCode { get; set; }
    }

    public partial class MGroupUserMenuModel
    {
        public int GroupUserMenuID { get; set; }
        [Display(Name = "Group User")]
        [Required(ErrorMessage = "Group User is required!")]
        public int GroupID { get; set; }
        [Display(Name = "Group User")]
        public string GroupUserName { get; set; }
        [Display(Name = "Menu")]
        [Required(ErrorMessage = "Menu is required!")]
        public int MenuID { get; set; }
        [Display(Name = "Menu")]
        public string MenuName { get; set; }
        public Nullable<int> OrderPage { get; set; }
        public bool IsMenu { get; set; }
        public string PageUrl { get; set; }
        public Nullable<int> OrderPageParent { get; set; }
        public Nullable<int> ParentMenuID { get; set; }
        public string ParentMenuName { get; set; }
        [Display(Name = "Create")]
        public bool AllowCreate { get; set; }
        [Display(Name = "Read")]
        public bool AllowRead { get; set; }
        [Display(Name = "Update")]
        public bool AllowUpdate { get; set; }
        [Display(Name = "Delete")]
        public bool AllowDelete { get; set; }
        public bool IsDeleted { get; set; }
        public string UserCreated { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string UserModified { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public int TotalRows { get; set; }
    }

    public partial class MGroupUserToMapModel
    {
        public int ID { get; set; }
        public int GroupUserID { get; set; }
        public bool IsSuperAdmin { get; set; }

        [Display(Name = "Bussiness Group")]
        public int BusinessGroupID { get; set; }
        [Display(Name = "Bussiness Group")]
        public string BusinessGroupName { get; set; }

        [Display(Name = "Division")]
        public int? DivisionID { get; set; }
        [Display(Name = "Division")]
        public string DivisionName { get; set; }

        [Display(Name = "Company")]
        public string CompanyID { get; set; }
        [Display(Name = "Company")]
        public string CompanyName { get; set; }

        [Display(Name = "Pers. Area")]
        public int? PersAreaID { get; set; }
        [Display(Name = "Pers. Area")]
        public string PersAreaName { get; set; }

        [Display(Name = "Pers. SubArea")]
        public int? PersSubAreaID { get; set; }
        [Display(Name = "Pers. SubArea")]
        public string PersSubAreaName { get; set; }
    }
}
