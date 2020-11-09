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
    public class VesselRepository : GenericRepository<MVesselModel>
    {
        public VesselRepository(DbContext context)
        {
            Db = context;
        }

        public List<MVesselModel> LookUp_MVessel(
            Nullable<int> IDVessel,
            string NameVessel, string NameVessel1
           )
        {
            SqlParameter[] sqlParams =
            {
             
                new SqlParameter("IDVessel", IDVessel == null ? (object)DBNull.Value :IDVessel),
                new SqlParameter("NameVessel", string.IsNullOrEmpty(NameVessel) ? (object)DBNull.Value : NameVessel),
                   new SqlParameter("NameVessel", string.IsNullOrEmpty(NameVessel) ? (object)DBNull.Value : NameVessel),

            };

            List<MVesselModel> result =
                Db.Database.SqlQuery<MVesselModel>(
                                                "exec sp_Lookup_Vessel @NameVessel,@NameVessel,@NameVessel"
                                            , sqlParams).ToList();

            return result;
        }

        #region Create/Update/Delete
        public ResultStatusModel CUD_Vessel(MVesselModel item)
        {
            SqlParameter id_out = new SqlParameter("id_out", 0) { Direction = ParameterDirection.Output };
            SqlParameter[] sqlParams =
            {
                //new SqlParameter("GroupUserID", SqlDbType.Int) { Value = item.GroupUserID },
                new SqlParameter("NameVoyage", string.IsNullOrEmpty(item.NameVessel) ? (object)DBNull.Value : item.NameVessel),
                new SqlParameter("IsDeleted", item.IsDeleted),
                new SqlParameter("isActive", item.IsActive),
                //new SqlParameter("GroupUserName", string.IsNullOrEmpty(item.GroupUserName) ? (object)DBNull.Value : item.GroupUserName),
                //new SqlParameter("UserCreated", string.IsNullOrEmpty(item.UserCreated) ? (object)DBNull.Value : item.UserCreated),
                //new SqlParameter("UserModified", string.IsNullOrEmpty(item.UserModified) ? (object)DBNull.Value : item.UserModified),
                //new SqlParameter("Mode", string.IsNullOrEmpty(mode) ? (object)DBNull.Value : mode),
                //id_out
            };

            List<ResultStatusModel> result =
                Db.Database.SqlQuery<ResultStatusModel>(
                                                @"exec sp_CUD_MVoyage
                                                    @NameVoyage, @GroupCode, @GroupUserName, @IsDeleted, @isActive"
                                            , sqlParams).ToList();
            //ID = id_out.Value.ToString();
            return result.FirstOrDefault();
        }
        #endregion
    }
}
