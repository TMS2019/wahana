using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using templateProject.Model;
using templateProject.Repository.Common;

namespace templateProject.Repository
{
   public class WageRepository: GenericRepository<MWageModel>
    {
        public WageRepository(DbContext context)
        {
            Db = context;
        }
        public ResultStatusModel CUD_Wage(MWageModel item, string mode, out string ID)
        {
            SqlParameter id_out = new SqlParameter("id_out", 0) { Direction = ParameterDirection.Output };
            SqlParameter[] sqlParams =
            {
                new SqlParameter("WageID", SqlDbType.Int) { Value = item.WageID },
                new SqlParameter("WageName", string.IsNullOrEmpty(item.WageName) ? (object) DBNull.Value : item.WageName),
                new SqlParameter("WageDescription", string.IsNullOrEmpty(item.WageDescription) ? (object) DBNull.Value : item.WageDescription),
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
                                                "exec sp_CUD_MWage " +
                                                "@WageID,@WageName,@WageDescription, @IsDeleted" +
                                                ", @UserCreated, @UserModified, @DateCreated, @DateModified, @Mode, @id_out output"
                                            , sqlParams).ToList();
            ID = id_out.Value.ToString();
            return result.FirstOrDefault();
        }

        public List<MWageModel> Lookup_WageList(
           Nullable<int> _WageNo,
           string _wageName,
           string _WageDescription,
           Nullable<bool> _IsDeleted
       )
        {
            SqlParameter[] sqlParams =
            {
                new SqlParameter("WageID", _WageNo == null ? (Object) DBNull.Value : _WageNo),
                new SqlParameter("WageName", string.IsNullOrEmpty(_wageName) ? (Object) DBNull.Value : _wageName),
                new SqlParameter("WageDescription", string.IsNullOrEmpty(_WageDescription) ? (Object) DBNull.Value : _WageDescription),
                new SqlParameter("IsDeleted", _IsDeleted == null ? (Object) DBNull.Value : _IsDeleted)
            };

            List<MWageModel> result =
                Db.Database.SqlQuery<MWageModel>(
                                                "exec sp_Lookup_MWage " +
                                                "@WageID, @WageName, @WageDescription,@IsDeleted"
                                            , sqlParams).ToList();

            return result;
        }
    }
}
