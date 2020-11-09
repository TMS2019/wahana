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
    public class PlanningRepository : GenericRepository<MPlanningModel>
    {
        public PlanningRepository(DbContext context)
        {
            Db = context;
        }
        public List<MMaterialModel> Lookup_MPlanningMaterial(
            Nullable<int> MaterialID, Nullable<int> VesselID, Nullable<int> VoyageID, Nullable<int> TotalVoyage, string VesselName,
             string MaterialName, string MaterialDescription, Nullable<bool> _IsDeleted)
        {
            SqlParameter[] sqlParams =
            {
                new SqlParameter("MaterialID", MaterialID == null ? (Object) DBNull.Value : MaterialID),
                new SqlParameter("VesselID", VesselID == null ? (Object) DBNull.Value : VesselID),
                  new SqlParameter("VesselName", VesselName == null ? (Object) DBNull.Value : VesselName),
                new SqlParameter("VoyageID", VoyageID == null ? (Object) DBNull.Value : VoyageID),
                new SqlParameter("TotalVoyage", TotalVoyage == null ? (Object) DBNull.Value : TotalVoyage),
               // new SqlParameter("UomID", UomID == null ? (Object) DBNull.Value : UomID),
              //    new SqlParameter("tmp", SqlDbType.DateTime),
                new SqlParameter("MaterialName", string.IsNullOrEmpty(MaterialName) ? (Object) DBNull.Value : MaterialName),
                new SqlParameter("MaterialDescription", string.IsNullOrEmpty(MaterialDescription) ? (Object) DBNull.Value : MaterialDescription),
                //new SqlParameter("UomName", string.IsNullOrEmpty(UomName) ? (Object) DBNull.Value : UomName),
                //new SqlParameter("UomDescription", string.IsNullOrEmpty(UomDescription) ? (Object) DBNull.Value : UomDescription),
                 new SqlParameter("IsDeleted", _IsDeleted == null ? (Object) DBNull.Value : _IsDeleted),
                 };
            List<MMaterialModel> result = Db.Database.SqlQuery<MMaterialModel>(
                                                "exec sp_Lookup_MMaterial @MaterialID,@VesselID,@VesselName,@VoyageID,@VesselName,@MaterialName,@MaterialDescription,@TotalVoyage,@IsDeleted"
                                                , sqlParams).ToList();
            return result;
        }


        public List<MPlanningModel> Lookup_MPlanningList(
            Nullable<int> BlID,
            string BlDate, string BLQty, string PoNo, string PoDate, string PoQty, string MaterialCode,
            string MaterialDescription, string Uom, string BatchCode, string PortOfOrigin, string PortOfDischarge,
            string Wage,
            Nullable<bool> _IsDeleted, 
            //int? PageSize,
            int? PageNumber,
            string OrderBy,
            string Sort

            )
        {
            SqlParameter[] sqlParams =
            {
                new SqlParameter("BlID", BlID == null ? (Object) DBNull.Value : BlID),
               new SqlParameter("BLDate", string.IsNullOrEmpty(BlDate) ? (Object) DBNull.Value : BlDate),
                new SqlParameter("BLQty", string.IsNullOrEmpty(BLQty) ? (Object) DBNull.Value : BLQty),
                new SqlParameter("PoNo", string.IsNullOrEmpty(PoNo) ? (Object) DBNull.Value : PoNo),
                new SqlParameter("PoDate", string.IsNullOrEmpty(PoDate) ? (Object) DBNull.Value : PoDate),
                new SqlParameter("PoQty", string.IsNullOrEmpty(PoQty) ? (Object) DBNull.Value : PoQty),
                new SqlParameter("MaterialCode", string.IsNullOrEmpty(MaterialCode) ? (Object) DBNull.Value : MaterialCode),
                new SqlParameter("MaterialDescription", string.IsNullOrEmpty(MaterialDescription) ? (Object) DBNull.Value : MaterialDescription),
                new SqlParameter("Uom", string.IsNullOrEmpty(Uom) ? (Object) DBNull.Value : Uom),
                new SqlParameter("BatchCode", string.IsNullOrEmpty(BatchCode) ? (Object) DBNull.Value : BatchCode),
                new SqlParameter("PortOfOrigin", string.IsNullOrEmpty(PortOfOrigin) ? (Object) DBNull.Value : PortOfOrigin),
                new SqlParameter("PortOfDischarge", string.IsNullOrEmpty(PortOfDischarge) ? (Object) DBNull.Value : PortOfDischarge),
                new SqlParameter("Wage", string.IsNullOrEmpty(Wage) ? (Object) DBNull.Value : Wage),
                new SqlParameter("IsDeleted", _IsDeleted == null ? (Object) DBNull.Value : _IsDeleted),
               // new SqlParameter("PageSize", PageSize == null ? (Object) DBNull.Value : PageSize),
                new SqlParameter("PageNumber", PageNumber == null ? (Object) DBNull.Value : PageNumber),
                new SqlParameter("OrderBy", OrderBy == null ? (Object) DBNull.Value : OrderBy),
                new SqlParameter("Sort", Sort == null ? (Object) DBNull.Value : Sort)
            };//,@BLDate,@BLQty,@PoNo,@PoQty,@MaterialCode,@MaterialDescription,@Uom,@BatchCode,@PortOfOrigin,@PortOfDischarge,@Wage,
            List<MPlanningModel> result = Db.Database.SqlQuery<MPlanningModel>(
                                                "exec sp_Lookup_MPlanning @BlID,@BLDate,@BLQty,@PoNo,@PoDate,@PoQty,@MaterialCode,@MaterialDescription,@Uom,@BatchCode,@PortOfOrigin,@PortOfDischarge,@Wage,@IsDeleted" +
                                                  ",@PageNumber, @OrderBy, @Sort"
                                                , sqlParams).ToList();

            return result;
        }

        public ResultStatusModel CUD_Planning(MPlanningModel item, string mode, out string ID)
        {
            SqlParameter id_out = new SqlParameter("id_out", 0) { Direction = ParameterDirection.Output };
            SqlParameter[] sqlParams =
            {
                 new SqlParameter("BlID", SqlDbType.Int) { Value = item.BlID },
                 new SqlParameter("BLDate", string.IsNullOrEmpty(item.BLDate) ? (object) DBNull.Value : item.BLDate),
                 new SqlParameter("BLQty", string.IsNullOrEmpty(item.BLQty) ? (object) DBNull.Value : item.BLQty),
                 new SqlParameter("PoNo", string.IsNullOrEmpty(item.PoNo) ? (object) DBNull.Value : item.PoNo),
                 new SqlParameter("PoDate", string.IsNullOrEmpty(item.PoDate) ? (object) DBNull.Value : item.PoDate),
                 new SqlParameter("PoQty", string.IsNullOrEmpty(item.PoQty) ? (object) DBNull.Value : item.PoQty),
                 new SqlParameter("MaterialCode", string.IsNullOrEmpty(item.MaterialCode) ? (object) DBNull.Value : item.MaterialCode),
                 new SqlParameter("MaterialDescription", string.IsNullOrEmpty(item.MaterialDescription) ? (object) DBNull.Value : item.MaterialDescription),
                 new SqlParameter("Uom", string.IsNullOrEmpty(item.Uom) ? (object) DBNull.Value : item.Uom),
                 new SqlParameter("BatchCode", string.IsNullOrEmpty(item.BatchCode) ? (object) DBNull.Value : item.BatchCode),
                 new SqlParameter("PortOfOrigin", string.IsNullOrEmpty(item.PortOfOrigin) ? (object) DBNull.Value : item.PortOfOrigin),
                 new SqlParameter("PortOfDischarge", string.IsNullOrEmpty(item.PortOfDischarge) ? (object) DBNull.Value : item.PortOfDischarge),
                 new SqlParameter("Wage", string.IsNullOrEmpty(item.Wage) ? (object) DBNull.Value : item.Wage),
                 new SqlParameter("IsDeleted", item.IsDeleted),
                 new SqlParameter("UserCreated", string.IsNullOrEmpty(item.UserCreated) ? (object)DBNull.Value : item.UserCreated),
                 new SqlParameter("UserModified", string.IsNullOrEmpty(item.UserModified) ? (object) DBNull.Value : item.UserModified),
                 new SqlParameter("DateCreated", item.DateCreated == null ? (object) DBNull.Value : item.DateCreated),
                 new SqlParameter("DateModified", item.DateModified == null ? (object) DBNull.Value : item.DateModified),
                 new SqlParameter("Mode", mode),
                 id_out
            };
            //,@BLQty,@PoNo,@PoDate,@PoQty,@MaterialCode,@MaterialDescription,@Uom,@BatchCode,@PortOfOrigin,@PortOfDischarge,@Wage
            List<ResultStatusModel> result =
                Db.Database.SqlQuery<ResultStatusModel>(
                                                "exec sp_CUD_MPlanning " +
                                                "@BlID,@BLDate,@BLQty,@PoNo,@PoDate,@PoQty,@MaterialCode,@MaterialDescription,@Uom,@BatchCode,@PortOfOrigin,@PortOfDischarge,@Wage,@IsDeleted" +
                                                ", @UserCreated, @UserModified, @DateCreated, @DateModified, @Mode, @id_out output"
                                            , sqlParams).ToList();
            ID = id_out.Value.ToString();
            return result.FirstOrDefault();
        }

    }
}
