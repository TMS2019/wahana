using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;

using templateProject.Model;
using templateProject.Repository.Common;

namespace templateProject.Repository
{
    public class BarangRepository : GenericRepository<MBarangModel>
    {
        public BarangRepository(DbContext context)
        {
            Db = context;
        }

        public List<MGroupUserModel> LookUp_MBarang(
            Nullable<int> _id, string _name, string _groupCode
            )
        {
            SqlParameter[] sqlParams =
            {
                new SqlParameter("GroupUserID", _id == null ? (object)DBNull.Value :_id),
                new SqlParameter("GroupUserName", string.IsNullOrEmpty(_name) ? (object)DBNull.Value : _name),
                new SqlParameter("GroupCode", string.IsNullOrEmpty(_groupCode) ? (object)DBNull.Value : _groupCode)
            };

            List<MGroupUserModel> result =
                Db.Database.SqlQuery<MGroupUserModel>(
                                                "exec sp_Lookup_MGroupUser @GroupUserID, @GroupUserName, @GroupCode "
                                            , sqlParams).ToList();

            return result;
        }

        #region Create/Update/Delete
        public ResultStatusModel CUD_MBarang(MGroupUserModel item, string mode, out string ID)
        {
            SqlParameter id_out = new SqlParameter("id_out", 0) { Direction = ParameterDirection.Output };
            SqlParameter[] sqlParams =
            {
                new SqlParameter("BarangID", SqlDbType.Int) { Value = item.GroupUserID },
                new SqlParameter("NamaBarang", string.IsNullOrEmpty(item.GroupCode) ? (object)DBNull.Value : item.GroupCode),
                 new SqlParameter("JenisBarang", string.IsNullOrEmpty(item.GroupCode) ? (object)DBNull.Value : item.GroupCode),               
                new SqlParameter("UserCreated", string.IsNullOrEmpty(item.UserCreated) ? (object)DBNull.Value : item.UserCreated),
                new SqlParameter("UserModified", string.IsNullOrEmpty(item.UserModified) ? (object)DBNull.Value : item.UserModified),
                new SqlParameter("Mode", string.IsNullOrEmpty(mode) ? (object)DBNull.Value : mode),
                id_out
            };

            List<ResultStatusModel> result =
                Db.Database.SqlQuery<ResultStatusModel>(
                                                @"exec sp_CUD_MBarang
                                                    @BarangID, @NamaBarang, @JenisBarang, @UserCreated
	                                                , @UserModified, @Mode, @id_out output"
                                            , sqlParams).ToList();
            ID = id_out.Value.ToString();
            return result.FirstOrDefault();
        }
        #endregion
    }
}
