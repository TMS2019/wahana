using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace templateProject.Model
{
    public partial class UserInfoModel
    {
        public int UserID { get; set; }
        public string OfficialName { get; set; }
        public string UserName { get; set; }
        public string Nik { get; set; }
        public string Email { get; set; }
        public string Client { get; set; }
        public List<MGroupUserModel> GroupUser { get; set; }
        public UserInfoAccessModel InfoAccess { get; set; }
        public List<ErrorModelState> ErrorModel { get; set; }
    }

    public partial class PlanningInfoModel
    {
        public int ID_Bl { get; set; }
        public DateTime BL_Date { get; set; }
        public Double BL_Qty { get; set; }
        public int PO_No { get; set; }
        public DateTime PO_Date { get; set; }
        public Double PO_Qty { get; set; }
        public int Material_Code { get; set; }
        public string Material_Desc { get; set; }
        public string Uom { get; set; }
        public int Batch_Code { get; set; }
        public string Port_of_Origin { get; set; }
        public string Port_of_discharge { get; set; }
        public int Wage_No { get; set; }
    }

    public partial class PlanningInfoAccessModel
    {
        public bool AllowCreate { get; set; }
        public bool AllowRead { get; set; }
        public bool AllowUpdate { get; set; }
        public bool AllowDelete { get; set; }
    }

    public partial class UserInfoAccessModel
    {
        public bool AllowCreate { get; set; }
        public bool AllowRead { get; set; }
        public bool AllowUpdate { get; set; }
        public bool AllowDelete { get; set; }
    }

    public partial class ErrorModelState
    {
        public string key { get; set; }
        public string errorMessage { get; set; }
    }

    public class StatusModel<T>
    {
        public bool IsSuccess { get; set; }
        public string Title { get; set; }
        public string Reason { get; set; }
        public T Data { get; set; }

        public StatusModel()
        {

        }

        public StatusModel(bool isSuccess, string title, string reason, T data)
        {
            this.IsSuccess = isSuccess;
            this.Title = title;
            this.Reason = reason;
            this.Data = data;
        }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class ListString{
        public string Code {get;set;}
    }

    public partial class DataTableModel<T>
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public T data { get; set; }
    }

    public class DataTableOrder
    {
        public int Column { get; set; }
        public string Dir { get; set; }
    }

    public class DataTableSearch
    {
        public string Value { get; set; }
        public bool Regex { get; set; }
    }

    public class DataTableColumn
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }

        public DataTableSearch Search { get; set; }
    }

    public class DataTableRequest
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }

        public DataTableOrder[] Order { get; set; }
        public DataTableColumn[] Columns { get; set; }
        public DataTableSearch Search { get; set; }
    }
}
