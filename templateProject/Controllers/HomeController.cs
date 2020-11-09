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
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;

namespace templateProject.Controllers
{
    public class HomeController : Controller
    {
        #region Uow
        UnitOfWork uow = new UnitOfWork();
        protected override void Dispose(bool disposing)
        {
            uow.Dispose();
            base.Dispose(disposing);
        }
        #endregion

        [CustomAuthorize(Users = "home", Roles = "read")]
        public ActionResult Index()
        {
            UserInfoModel userInfo = (UserInfoModel)GeneralFunctions.GetSession(Configs.session);
            return View();
        }
    }
}