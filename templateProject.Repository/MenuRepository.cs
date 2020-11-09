using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data.Entity;

using templateProject.Repository.Common;
using templateProject.Model;

namespace templateProject.Repository
{
    public class MenuRepository : GenericRepository<MMenuModel>
    {
        public MenuRepository(DbContext context)
        {
            Db = context;
        }

        public List<MMenuModel> Lookup_MMenu(
            Nullable<int> MenuID,
            Nullable<int> ParentMenuID,
            string MenuName,
            string Modul,
            Nullable<bool> IsMenu
        )
        {
            SqlParameter[] sqlParams = 
            {
                new SqlParameter("MenuID", MenuID == null ? (object)DBNull.Value : MenuID),
                new SqlParameter("ParentMenuID", ParentMenuID == null ? (object)DBNull.Value : ParentMenuID),
                new SqlParameter("MenuName", string.IsNullOrEmpty(MenuName) ? (object)DBNull.Value : MenuName),
                new SqlParameter("Modul", string.IsNullOrEmpty(Modul) ? (object)DBNull.Value : Modul),
                new SqlParameter("IsMenu", IsMenu == null ? (object)DBNull.Value : IsMenu),
            };
            List<MMenuModel> result = Db.Database.SqlQuery<MMenuModel>(
                                                "exec sp_Lookup_MMenu @MenuID, @ParentMenuID, @MenuName, @Modul, @IsMenu "
                                            , sqlParams).ToList();

            return result;
        }

        public List<MMenuModel> Lookup_MenuByUserID(
            int _UserID
        )
        {
            SqlParameter[] sqlParams = 
            {
                new SqlParameter("UserID", _UserID)
            };
            List<MMenuModel> result = Db.Database.SqlQuery<MMenuModel>(
                                                "exec sp_Lookup_MenuByUserID @UserID "
                                            , sqlParams).ToList();

            return result;
        }

        public List<MMenuModel> GetMenuByGroupList(int userid)
        {
            List<MMenuModel> items = Lookup_MenuByUserID(userid);
            return items;
        }
    }
}
