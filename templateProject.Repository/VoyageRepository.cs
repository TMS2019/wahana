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
    public class VoyageRepository : GenericRepository<MVoyageModel>
    {
        public VoyageRepository(DbContext context)
        {
            Db = context;
        }

        public List<MVoyageModel> Lookup_VoyageClose(
           Nullable<int> VoyageID
       //  Nullable<int> VesselID,
       //   string VesselName

       )
        {
            SqlParameter[] sqlParams =
            {
                new SqlParameter("VoyageID", VoyageID == null ? (object)DBNull.Value : VoyageID),
                //new SqlParameter("VesselID", VesselID == null ? (object)DBNull.Value : VesselID),
               // new SqlParameter("VesselName", string.IsNullOrEmpty(VesselName) ? (object)DBNull.Value : VesselName)
            };
            List<MVoyageModel> result = Db.Database.SqlQuery<MVoyageModel>(
                                                "exec sp_Lookup_MVoyageClose @VoyageID"
                                            , sqlParams).ToList();

            return result;
        }

    }
}


