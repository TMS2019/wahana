using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Data;

using templateProject.Repository.Common;
using templateProject.Model;

namespace templateProject.Repository
{
    public class GroupUserMenuRepository : GenericRepository<MGroupUserMenuModel>
    {
        public GroupUserMenuRepository(DbContext context)
        {
            Db = context;
        }

        #region Create, Update, Delete

        public ResultStatusModel CUD_GroupUserMenu(MGroupUserMenuModel item, string mode, out string ID)
        {
            SqlParameter id_out = new SqlParameter("id_out", 0) { Direction = ParameterDirection.Output };
            SqlParameter[] sqlParams = 
            {
                new SqlParameter("GroupUserMenuID", SqlDbType.Int) { Value = item.GroupUserMenuID },
                new SqlParameter("GroupID", item.GroupID),
                new SqlParameter("MenuID", item.MenuID),
                new SqlParameter("AllowCreate", item.AllowCreate),
                new SqlParameter("AllowRead", item.AllowRead),
                new SqlParameter("AllowUpdate", item.AllowUpdate),
                new SqlParameter("AllowDelete", item.AllowDelete),
                new SqlParameter("IsDeleted", item.IsDeleted),
                new SqlParameter("UserCreated", string.IsNullOrEmpty(item.UserCreated) ? (object)DBNull.Value : item.UserCreated),
                new SqlParameter("UserModified", string.IsNullOrEmpty(item.UserModified) ? (object) DBNull.Value : item.UserModified),
                new SqlParameter("DateCreated", item.DateCreated == null ? (object) DBNull.Value : item.DateCreated),
                new SqlParameter("DateModified", item.DateModified == null ? (object) DBNull.Value : item.DateModified),

                new SqlParameter("Mode", mode),
                id_out
            };

            List<ResultStatusModel> result =
                Db.Database.SqlQuery<ResultStatusModel>(
                                                "exec sp_CUD_MGroupUserMenu " +
                                                "@GroupUserMenuID, @GroupID, @MenuID, @AllowCreate, @AllowRead, @AllowUpdate, " +
                                                "@AllowDelete, @IsDeleted, @UserCreated, @DateCreated, @UserModified, @DateModified, " +
                                                "@Mode, @id_out output"
                                            , sqlParams).ToList();
            ID = id_out.Value.ToString();
            return result.FirstOrDefault();
        }
        #endregion

        #region Read

        public List<MGroupUserMenuModel> Lookup_MGroupUserMenu(
            Nullable<int> GroupUserMenuID,
            Nullable<int> GroupID,
            string GroupUserName,
            Nullable<int> MenuID,
            string MenuName
        )
        {
            SqlParameter[] sqlParams = 
            {
                new SqlParameter("GroupUserMenuID", GroupUserMenuID == null ? (Object) DBNull.Value : GroupUserMenuID),
                new SqlParameter("GroupID", GroupID == null ? (Object) DBNull.Value : GroupID),
                new SqlParameter("GroupUserName", string.IsNullOrEmpty(GroupUserName) ? (Object) DBNull.Value : GroupUserName),
                new SqlParameter("MenuID", MenuID == null ? (Object) DBNull.Value : MenuID),
                new SqlParameter("MenuName", string.IsNullOrEmpty(MenuName) ? (Object) DBNull.Value : MenuName)
            };

            List<MGroupUserMenuModel> result =
                Db.Database.SqlQuery<MGroupUserMenuModel>(
                                                "exec sp_Lookup_MGroupUserMenu " +
                                                "@GroupUserMenuID, @GroupID, @GroupUserName, @MenuID, @MenuName"
                                            , sqlParams).ToList();

            return result;
        }

        public List<MGroupUserMenuModel> Lookup_MGroupUserMenuPaging(
            Nullable<int> GroupUserMenuID,
            Nullable<int> GroupID,
            string GroupUserName,
            Nullable<int> MenuID,
            string MenuName,
            int? PageSize,
            int? PageNumber,
            string OrderBy,
            string Sort
        )
        {
            SqlParameter[] sqlParams = 
            {
                new SqlParameter("GroupUserMenuID", GroupUserMenuID == null ? (Object) DBNull.Value : GroupUserMenuID),
                new SqlParameter("GroupID", GroupID == null ? (Object) DBNull.Value : GroupID),
                new SqlParameter("GroupUserName", string.IsNullOrEmpty(GroupUserName) ? (Object) DBNull.Value : GroupUserName),
                new SqlParameter("MenuID", MenuID == null ? (Object) DBNull.Value : MenuID),
                new SqlParameter("MenuName", string.IsNullOrEmpty(MenuName) ? (Object) DBNull.Value : MenuName),
                new SqlParameter("PageSize", PageSize == null ? (Object) DBNull.Value : PageSize),
                new SqlParameter("PageNumber", PageNumber == null ? (Object) DBNull.Value : PageNumber),
                new SqlParameter("OrderBy", OrderBy == null ? (Object) DBNull.Value : OrderBy),
                new SqlParameter("Sort", Sort == null ? (Object) DBNull.Value : Sort)
            };

            List<MGroupUserMenuModel> result =
                Db.Database.SqlQuery<MGroupUserMenuModel>(
                                                "exec sp_Lookup_MGroupUserMenuPaging " +
                                                "@GroupUserMenuID, @GroupID, @GroupUserName, @MenuID, @MenuName" +
                                                ", @PageSize, @PageNumber, @OrderBy, @Sort"
                                            , sqlParams).ToList();

            return result;
        }

        public List<MGroupUserMenuModel> Lookup_AccessByModulUserID(
            string _Modul,
            int _UserID,
            string _ListGroup
        )
        {
            SqlParameter[] sqlParams = 
            {
                new SqlParameter("Modul", string.IsNullOrEmpty(_Modul) ? (object)DBNull.Value : _Modul),
                new SqlParameter("UserID", _UserID),
                new SqlParameter("ListGroup", string.IsNullOrEmpty(_ListGroup) ? (object) DBNull.Value : _ListGroup)
            };

            List<MGroupUserMenuModel> result =
                Db.Database.SqlQuery<MGroupUserMenuModel>(
                                                "exec sp_Lookup_AccessByModulUserID @Modul, @UserID, @ListGroup "
                                            , sqlParams).ToList();

            return result;
        }

        public UserInfoAccessModel GetPageAccessByGroupNModul(UserInfoModel userInfo, string Modul = "")
        {
            List<MGroupUserMenuModel> ListAccess = null;

            string listGroup = "";

            if (userInfo.GroupUser.Any())
            {
                listGroup = string.Join(",", userInfo.GroupUser.Select(x => x.GroupUserID));
            }

            ListAccess = this.Lookup_AccessByModulUserID(Modul, userInfo.UserID, listGroup);



            UserInfoAccessModel output = new UserInfoAccessModel();
            output.AllowCreate = false;
            output.AllowRead = false;
            output.AllowUpdate = false;
            output.AllowDelete = false;

            if (ListAccess != null)
            {
                foreach (MGroupUserMenuModel item in ListAccess)
                {
                    if (item.AllowCreate) { output.AllowCreate = true; }
                    if (item.AllowRead) { output.AllowRead = true; }
                    if (item.AllowUpdate) { output.AllowUpdate = true; }
                    if (item.AllowDelete) { output.AllowDelete = true; }
                }
            }

            return output;
        }
        #endregion
    }
}
