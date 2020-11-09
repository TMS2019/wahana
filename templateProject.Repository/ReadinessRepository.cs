using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data.Entity;
using templateProject.Repository.Common;
using templateProject.Model;
using System.Data;

namespace templateProject.Repository
{
    public class ReadinessRepository : GenericRepository<MReadinnesModel>
    {
        public ReadinessRepository(DbContext context)
        {
            Db = context;
        }


        //public ResultStatusModel CUD_Readiness(MReadinnesModel item, string mode, out string ID)
        //{
        //    SqlParameter id_out = new SqlParameter("id_out", 0) { Direction = ParameterDirection.Output };
        //    SqlParameter[] sqlParams =
        //    {
        //        new SqlParameter("ReadinessID", SqlDbType.Int) { Value = item.ReadinessID },
        //        new SqlParameter("Category", string.IsNullOrEmpty(item.Category) ? (object) DBNull.Value : item.Category),
        //        new SqlParameter("IsDeleted", item.IsDeleted),
        //        new SqlParameter("Mode", mode),
        //        id_out
        //    };

        //    List<ResultStatusModel> result =
        //        Db.Database.SqlQuery<ResultStatusModel>(
        //                                        "exec sp_CUD_MReadiness " +
        //                                        "@ReadinessID, @Category, @IsDeleted,@Mode,@id_out output"
        //                                    , sqlParams).ToList();
        //    ID = id_out.Value.ToString();
        //    return result.FirstOrDefault();
        //}

        public List<MReadinnesModel> Lookup_MReadinessBody(
        Nullable<int> UserID,
        Nullable<int> ResultID,
        Nullable<int> DefactID,
        Nullable<int> ReadinessID,
        string UserName,
        string ResultName,
        string DefactName,
        string ResultStatus,
        Nullable<bool> _IsDeleted
        )
        {
 
            SqlParameter[] sqlParams =
            {
                new SqlParameter("UserID", UserID == null ? (object)DBNull.Value : UserID),
                new SqlParameter("ResultID", ResultID == null ? (object)DBNull.Value : ResultID),
                new SqlParameter("DefactID", DefactID == null ? (object)DBNull.Value : DefactID),
                new SqlParameter("ReadinessID", ReadinessID == null ? (object)DBNull.Value : ReadinessID),
                new SqlParameter("UserName", UserName == null ? (object)DBNull.Value : UserName),
                new SqlParameter("ResultName", string.IsNullOrEmpty(ResultName) ? (Object) DBNull.Value : ResultName),
                new SqlParameter("DefactName", string.IsNullOrEmpty(DefactName) ? (Object) DBNull.Value : DefactName),
                new SqlParameter("ResultStatus", string.IsNullOrEmpty(ResultStatus) ? (Object) DBNull.Value : ResultStatus),
                new SqlParameter("IsDeleted", _IsDeleted == null ? (Object) DBNull.Value : _IsDeleted)
                //@UserID,@ResultID,@DefactID,@ReadinessID,@UserName,@ResultName,@DefactName,@ResultStatus,@IsDeleted,
            };
            List<MReadinnesModel> result = Db.Database.SqlQuery<MReadinnesModel>(
                                                "exec sp_Lookup_MReadinessBody @ResultStatus"
                                            , sqlParams).ToList();

            return result;
        }


    }
}
