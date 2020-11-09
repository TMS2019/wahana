using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

using templateProject.Security;
using templateProject.Repository.Common;
using templateProject.Model;
using templateProject.Helper;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace templateProject.Controllers
{
    public class DivisionController : Controller
    {
        #region Uow
        UnitOfWork uow = new UnitOfWork();
        protected override void Dispose(bool disposing)
        {
            uow.Dispose();
            base.Dispose(disposing);
        }
        #endregion

        #region View

        [CustomAuthorize(Users = "Division", Roles = "read")]
        public ActionResult Index()
        {
            return View();
        }

        [CustomAuthorize(Users = "Division", Roles = "create")]
        public ActionResult Manage(int id = 0)
        {
            UserModel data = new UserModel();

            List<MGroupUserModel> group = new List<MGroupUserModel>();
            if (id != 0)
            {
                data = uow.UserRepository.Lookup_MUser(id, null, null, null, null, false).FirstOrDefault();

                if (data == null)
                {
                    data = new UserModel();
                    data.UserID = -1;
                }
                else
                {
                    group = uow.GroupUserRepository.LookUp_MGroupUser(null, null, null);
                }
            }
            else
            {
                group = uow.GroupUserRepository.LookUp_MGroupUser(null, null, null);
            }

            ViewData["GroupList"] = group;

            return View(data);
        }

        [CustomAuthorize(Users = "Division", Roles = "create")]
        [HttpPost]
        public async Task<JsonResult> Manage(UserModel item)
        {
            UserInfoModel userInfo = (UserInfoModel)GeneralFunctions.GetSession(Configs.session);
            ResultStatusModel result = new ResultStatusModel();
            item.UserCreated = userInfo.UserName;
            item.UserModified = userInfo.UserName;

            if (item.UserID == 0 && string.IsNullOrEmpty(item.Password))
            {
                ModelState.AddModelError("Password", "Password wajib diisi!");
            }

            try
            {
                //using (HttpClient client = new HttpClient())
                //{
                //    client.BaseAddress = new Uri("http://10.126.20.22/ws_NIKSAP/Service1.asmx/");
                //    HttpResponseMessage response = new HttpResponseMessage();
                //    response = await client.GetAsync("GetNIKSAP?employee_code=" + item.Nik + "&userparam=sap&passparam=JOYketC0rdA/F4MBzx5BEA==");
                //    var data = await response.Content.ReadAsStringAsync();
                //    XElement convertXml = XElement.Parse(data);
                //    if (string.IsNullOrEmpty(convertXml.Value))
                //    {
                //        ModelState.AddModelError("Nik", "Nik tidak ditemukan!");
                //    }
                //}
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("User", ex.Message);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    MUserVM user = new MUserVM();
                    user.UserID = item.UserID;
                    user.Email = item.Email;
                    user.GroupUserID = item.GroupUserID.ToString();
                    user.GroupUserName = item.GroupUserName;
                    user.IsDeleted = false;
                    user.Nik = item.Nik;
                    user.OfficialName = item.OfficialName;
                    user.UserName = item.UserName;
                    if (item.UserID == 0)
                    {
                        user.Password = Helper.Encryption.EncryptRegular(Configs.KeyEncrypt, item.Password);
                    }
                    user.UserCreated = userInfo.UserName;
                    user.UserModified = userInfo.UserName;

                    string id_out = "";
                    if (item.UserID == 0)
                    {
                        result = uow.UserRepository.CUD_User(user, "c", out id_out);
                    }
                    else
                    {
                        result = uow.UserRepository.CUD_User(user, "u", out id_out);
                    }

                    if (!result.issuccess)
                    {
                        ModelState.AddModelError("Failed", result.err_msg);
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Failed", e.Message);
                }
            }
            List<string> Error = (from m in ModelState
                                  where m.Value.Errors.Any()
                                  select m.Value.Errors[0].ErrorMessage).ToList();

            return Json(new { Error = Error, data = item }, JsonRequestBehavior.DenyGet);
        }

        [CustomAuthorize(Users = "Division", Roles = "delete")]
        [HttpPost]
        public JsonResult Delete(int id = 0)
        {
            UserInfoModel userInfo = (UserInfoModel)GeneralFunctions.GetSession(Configs.session);
            MUserVM user = new MUserVM();
            user.UserID = id;
            user.UserCreated = userInfo.UserName;
            user.UserModified = userInfo.UserName;

            ResultStatusModel result = new ResultStatusModel();

            ModelState.Clear();

            try
            {
                string id_out = "";
                result = uow.UserRepository.CUD_User(user, "d", out id_out);
                if (!result.issuccess)
                {
                    ModelState.AddModelError("Failed", result.msg);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Failed", e.Message);
            }

            List<string> Error = (from m in ModelState
                                  where m.Value.Errors.Any()
                                  select m.Value.Errors[0].ErrorMessage).ToList();

            return Json(new { Error = Error, data = id }, JsonRequestBehavior.DenyGet);
        }

        [CustomAuthorize(Users = "home", Roles = "read")]
        public ActionResult UserProfile()
        {
            return View();
        }

        #endregion View

        #region Grid
        [CustomAuthorize(Users = "Division", Roles = "read")]
        public JsonResult GridRead(DataTableRequest dt)
        {
            //Init Output
            DataTableModel<List<MDivision>> data = new DataTableModel<List<MDivision>>();

            //Init Get Data
            List<MDivision> list = new List<MDivision>();


            //Init Search 
            string searchByOfficialName = !string.IsNullOrEmpty(Request.QueryString["searchByOfficialName"]) ? Request.QueryString["searchByOfficialName"] : null;
            string searchByUsername = !string.IsNullOrEmpty(Request.QueryString["searchByUsername"]) ? Request.QueryString["searchByUsername"] : null;

            //Init Sort
            int sortColumn = 0;
            string sortDirection = "";
            string sortBy = "";
            if (Request.QueryString["order[0][column]"] != null)
            {
                sortColumn = int.Parse(Request.QueryString["order[0][column]"]);
            }
            if (Request.QueryString["order[0][dir]"] != null)
            {
                sortDirection = Request.QueryString["order[0][dir]"];
            }

            switch (sortColumn)
            {
                case 1:
                    sortBy = "CompanyID";
                    break;
                case 2:
                    sortBy = "Division";
                    break;
                case 3:
                    sortBy = "Nik";
                    break;
                case 4:
                    sortBy = "Email";
                    break;
                case 5:
                    sortBy = "GroupUserName";
                    break;
                default:
                    break;
            }

            int pageNo = (int)Math.Floor((double)(dt.Start / dt.Length)) + 1;
            
            list = uow.DivisionRepository.CUD_Division( searchByOfficialName, searchByUsername, null,null, dt.Length, pageNo, sortBy, sortDirection);

            if (list.Any())
            {
                data.recordsFiltered = list.FirstOrDefault().TotalRows;
                data.recordsTotal = list.FirstOrDefault().TotalRows;
            }
            else
            {
                data.recordsFiltered = 0;
                data.recordsTotal = 0;
            }
            //Init Optional
            data.draw = dt.Draw;
            data.data = list;

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}