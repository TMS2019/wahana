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
    public class PlantRepository : GenericRepository<MPlantModel>
    {
        public PlantRepository(DbContext context)
        {
            Db = context;
        }


        #region Create/Update/Delete
        public ResultStatusModel CUD_Plant(MPlantModel item, string mode, out string ID)
        {
            SqlParameter id_out = new SqlParameter("id_out", 0) { Direction = ParameterDirection.Output };
            SqlParameter[] sqlParams =
            {
                new SqlParameter("PlantID", SqlDbType.Int) { Value = item.PlantID },
                new SqlParameter("PlantName", string.IsNullOrEmpty(item.PlantName) ? (object) DBNull.Value : item.PlantName),
                new SqlParameter("PlantDescription", string.IsNullOrEmpty(item.PlantDescription) ? (object) DBNull.Value : item.PlantDescription ),
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
                                                "exec sp_CUD_Plant @PlantID, @PlantName, @PlantDescription, @IsDeleted, @UserCreated, @UserModified, @DateCreated, @DateModified,@Mode, @id_out output"
                                            , sqlParams).ToList();
            ID = id_out.Value.ToString();
            return result.FirstOrDefault();
        }
        #endregion

        public List<MPlantModel> Lookup_MPlantList(
           Nullable<int> _PlantId,
           string _PlantName,
           string _PlantDesc
          
       )
        {
            SqlParameter[] sqlParams =
            {
               new SqlParameter("PlantID", _PlantId == null ? (Object) DBNull.Value : _PlantId),
               new SqlParameter("PlantName", string.IsNullOrEmpty(_PlantName) ? (Object) DBNull.Value : _PlantName),
               new SqlParameter("PlantDescription", string.IsNullOrEmpty(_PlantDesc) ? (Object) DBNull.Value : _PlantDesc),
               // new SqlParameter("IsDeleted", _IsDeleted == null ? (Object) DBNull.Value : _IsDeleted),
                //new SqlParameter("PageSize", PageSize == null ? (Object) DBNull.Value : PageSize),
                //new SqlParameter("PageNumber", PageNumber == null ? (Object) DBNull.Value : PageNumber),
                //new SqlParameter("OrderBy", OrderBy == null ? (Object) DBNull.Value : OrderBy),
                //new SqlParameter("Sort", Sort == null ? (Object) DBNull.Value : Sort)
            };

            List<MPlantModel> result =
                Db.Database.SqlQuery<MPlantModel>(
                                                "exec sp_listPlant @PlantID,@PlantName,@PlantDescription"
                                            , sqlParams).ToList();

            return result;
        }
    }
}
