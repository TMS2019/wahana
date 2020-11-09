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

namespace templateProject.Controllers
{
    public class FormController : Controller
    {
        #region Uow
        UnitOfWork uow = new UnitOfWork();
        protected override void Dispose(bool disposing)
        {
            uow.Dispose();
            base.Dispose(disposing);
        }
        #endregion
        
        [CustomAuthorize(Users = "Form", Roles = "read")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Plugin()
        {
            return View();
        }
	}
}