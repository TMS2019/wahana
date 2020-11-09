using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;
using System.DirectoryServices;
using System.Security.Principal;

using templateProject.Model;
using templateProject.Repository.Common;

namespace templateProject.Repository
{
    public class DefactRepository : GenericRepository<MDefactModel>
    {
        public DefactRepository(DbContext context)
        {
            Db = context;
        }
 

        #region Create/Update/Delete
        public ResultStatusModel CUD_Defact(MDefactModel item, string mode, out string ID)
        {
            SqlParameter id_out = new SqlParameter("id_out", 0) { Direction = ParameterDirection.Output };
            SqlParameter[] sqlParams =
            {
                new SqlParameter("DefactID", SqlDbType.Int) { Value = item.DefactID },
                new SqlParameter("DefactName", string.IsNullOrEmpty(item.DefactName) ? (object) DBNull.Value : item.DefactName),
                new SqlParameter("DefactDesc", string.IsNullOrEmpty(item.DefactDesc) ? (object) DBNull.Value : item.DefactDesc),
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
                                                "exec sp_CUD_MDefact " +
                                                "@DefactID, @DefactName, @DefactDesc,@IsDeleted" +
                                                ", @UserCreated, @UserModified, @DateCreated, @DateModified, @Mode, @id_out output"
                                            , sqlParams).ToList();
            ID = id_out.Value.ToString();
            return result.FirstOrDefault();
        }
        #endregion

      
        public List<MDefactModel> Lookup_MDefactPaging(
            Nullable<int> DefactID,
            string DefactName,
            string DefactDesc,
            Nullable<bool> _IsDeleted
        )
        {
            SqlParameter[] sqlParams =
            {
                new SqlParameter("DefactID", DefactID == null ? (Object) DBNull.Value : DefactID),
                new SqlParameter("DefactName", string.IsNullOrEmpty(DefactName) ? (Object) DBNull.Value : DefactName),
                new SqlParameter("DefactDesc", string.IsNullOrEmpty(DefactDesc) ? (Object) DBNull.Value : DefactDesc),
                new SqlParameter("IsDeleted", _IsDeleted == null ? (Object) DBNull.Value : _IsDeleted)
            };

            List<MDefactModel> result =
                Db.Database.SqlQuery<MDefactModel>(
                                                "exec sp_Lookup_MDefactList " +
                                                "@DefactID, @DefactName, @DefactDesc, @IsDeleted"
                                               // ", @UserCreated, @UserModified, @DateCreated, @DateModified, @Mode, @id_out output"
                                            , sqlParams).ToList();

            return result;
        }
    }
}


 

    

