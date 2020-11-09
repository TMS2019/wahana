using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

using templateProject.Security;
using templateProject.Repository.Common;
using templateProject.Model;
using templateProject.Helper;

namespace templateProject.Controllers
{
    public class MTTDController : Controller
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

        [CustomAuthorize(Users = "TandaTangan", Roles = "read")]
        public ActionResult Index()
        {
            return View();
        }

        [CustomAuthorize(Users = "accessmenu", Roles = "create")]
        public ActionResult Manage(int id = 0)
        {
            MGroupUserMenuModel data = new MGroupUserMenuModel();

            List<MGroupUserModel> group = new List<MGroupUserModel>();
            List<MMenuModel> menu = new List<MMenuModel>();

            group = uow.GroupUserRepository.LookUp_MGroupUser(null, null, null);
            menu = uow.MenuRepository.Lookup_MMenu(null, null, null, null, null);

            if (id != 0)
            {
                data = uow.GroupUserMenuRepository.Lookup_MGroupUserMenu(id, null, null, null, null).FirstOrDefault();

                if (data == null)
                {
                    data = new MGroupUserMenuModel();
                    data.GroupUserMenuID = -1;
                }
            }

            ViewData["GroupList"] = group;
            ViewData["MenuList"] = menu;

            return View(data);
        }

        [CustomAuthorize(Users = "accessmenu", Roles = "create")]
        [HttpPost]
        public JsonResult Manage(MGroupUserMenuModel item)
        {
            UserInfoModel userInfo = (UserInfoModel)GeneralFunctions.GetSession(Configs.session);
            ResultStatusModel result = new ResultStatusModel();
            item.UserCreated = userInfo.UserName;
            item.UserModified = userInfo.UserName;

            if (ModelState.IsValid)
            {
                try
                {
                    string id_out = "";
                    if (item.GroupUserMenuID == 0)
                    {
                        result = uow.GroupUserMenuRepository.CUD_GroupUserMenu(item, "c", out id_out);
                    }
                    else
                    {
                        result = uow.GroupUserMenuRepository.CUD_GroupUserMenu(item, "u", out id_out);
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

        [CustomAuthorize(Users = "accessmenu", Roles = "delete")]
        [HttpPost]
        public JsonResult Delete(int id = 0)
        {
            UserInfoModel userInfo = (UserInfoModel)GeneralFunctions.GetSession(Configs.session);
            MGroupUserMenuModel item = new MGroupUserMenuModel();
            item.GroupUserMenuID = id;
            item.UserCreated = userInfo.UserName;
            item.UserModified = userInfo.UserName;

            ResultStatusModel result = new ResultStatusModel();

            ModelState.Clear();

            try
            {
                string id_out = "";
                result = uow.GroupUserMenuRepository.CUD_GroupUserMenu(item, "d", out id_out);
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

        #endregion View

        #region Grid
        [CustomAuthorize(Users = "accessmenu", Roles = "read")]
        public JsonResult GridRead(DataTableRequest dt)
        {
            //Init Output
            DataTableModel<List<MGroupUserMenuModel>> data = new DataTableModel<List<MGroupUserMenuModel>>();

            //Init Get Data
            List<MGroupUserMenuModel> list = new List<MGroupUserMenuModel>();


            //Init Search 
            string searchByGroupUserName = !string.IsNullOrEmpty(Request.QueryString["searchByGroupUserName"]) ? Request.QueryString["searchByGroupUserName"] : null;
            string searchByMenuName = !string.IsNullOrEmpty(Request.QueryString["searchByMenuName"]) ? Request.QueryString["searchByMenuName"] : null;

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
                    sortBy = "GroupUserName";
                    break;
                case 2:
                    sortBy = "ParentMenuName";
                    break;
                case 3:
                    sortBy = "MenuName";
                    break;
                case 4:
                    sortBy = "AllowCreate";
                    break;
                case 5:
                    sortBy = "AllowRead";
                    break;
                case 6:
                    sortBy = "AllowUpdate";
                    break;
                case 7:
                    sortBy = "AllowDelete";
                    break;
                default:
                    break;
            }

            int pageNo = (int)Math.Floor((double)(dt.Start / dt.Length)) + 1;
            list = uow.GroupUserMenuRepository.Lookup_MGroupUserMenuPaging(null, null, searchByGroupUserName, null, searchByMenuName, dt.Length, pageNo, sortBy, sortDirection);

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