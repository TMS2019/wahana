using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

using templateProject.Security;
using templateProject.Repository.Common;
using templateProject.Model;
using templateProject.Helper;

namespace templateProject.Controllers
{
    public class GroupUserController : Controller
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
        [CustomAuthorize(Users = "groupuser", Roles = "read")]
        public ActionResult Index()
        {
            return View();
        }

        [CustomAuthorize(Users = "groupuser", Roles = "create")]
        public ActionResult Manage(int id = 0)
        {
            MGroupUserModel data = new MGroupUserModel();

            if (id != 0)
            {
                data = uow.GroupUserRepository.LookUp_MGroupUser(id, null, null).FirstOrDefault();

                if (data == null)
                {
                    data = new MGroupUserModel();
                    data.GroupUserID = -1;
                }
            }

            return View(data);
        }

        [CustomAuthorize(Users = "groupuser", Roles = "create")]
        [HttpPost]
        public JsonResult Manage(MGroupUserModel item)
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
                    if (item.GroupUserID == 0)
                    {
                        result = uow.GroupUserRepository.CUD_GroupUser(item, "c", out id_out);
                    }
                    else
                    {
                        result = uow.GroupUserRepository.CUD_GroupUser(item, "u", out id_out);
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

        [CustomAuthorize(Users = "groupuser", Roles = "delete")]
        [HttpPost]
        public JsonResult Delete(int id = 0)
        {
            UserInfoModel userInfo = (UserInfoModel)GeneralFunctions.GetSession(Configs.session);
            MGroupUserModel item = new MGroupUserModel();
            item.GroupUserID = id;
            item.UserCreated = userInfo.UserName;
            item.UserModified = userInfo.UserName;

            ResultStatusModel result = new ResultStatusModel();

            ModelState.Clear();

            try
            {
                string id_out = "";
                result = uow.GroupUserRepository.CUD_GroupUser(item, "d", out id_out);
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

        #endregion

        #region GRID
        [CustomAuthorize(Users = "groupuser", Roles = "read")]
        public JsonResult GridRead(DataTableRequest dt)
        {
            //Init Output
            DataTableModel<List<MGroupUserModel>> data = new DataTableModel<List<MGroupUserModel>>();

            //Init Get Data
            List<MGroupUserModel> list = new List<MGroupUserModel>();
            list = uow.GroupUserRepository.LookUp_MGroupUser(null, null, null);
            

            if (list.Any())
            {
                data.recordsFiltered = list.Count();
                data.recordsTotal = list.Count();
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