using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using templateProject.Security;
using templateProject.Repository.Common;
using templateProject.Helper;
using templateProject.Model;

namespace templateProject.Controllers
{
    public class AccountController : Controller
    {
        #region Uow
        UnitOfWork uow = new UnitOfWork();
        protected override void Dispose(bool disposing)
        {
            uow.Dispose();
            base.Dispose(disposing);
        }
        #endregion

        #region VIEW
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToLocal(Url.Content("~/"));
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                string username = "";
                string domain = Configs.DefaultDomain;

                string[] DomUser = model.UserName.Split('\\');

                if (DomUser.Length == 1)
                {
                    username = model.UserName;
                }
                else if (DomUser.Length == 2)
                {
                    username = DomUser[1];
                    domain = DomUser[0];
                }
                else
                {
                    ModelState.AddModelError("Failed", "Domain and Username is incorrect!");
                }

                if (ModelState.IsValid)
                {
                    //bool isvalidLdap = false;
                    //try
                    //{
                    //    isvalidLdap = uow.UserRepository.IsLDAPAuthenticated(Configs.DirectoryPath, domain, username, model.Password);
                    //}
                    //catch (Exception)
                    //{

                    //}

                    //if (isvalidLdap)
                    //{
                    StatusModel<UserInfoModel> validateLogin = new StatusModel<UserInfoModel>();
                    try
                    {
                        validateLogin = uow.UserRepository.ValidateUser(model.UserName, Helper.Encryption.EncryptRegular(Configs.KeyEncrypt, model.Password));
                    }
                    catch (Exception e)
                    {
                        validateLogin.IsSuccess = false;
                        validateLogin.Reason = e.Message;
                    }
                    
                    if (validateLogin.IsSuccess)
                    {
                        UserInfoModel userInfo = validateLogin.Data;
                        FormsAuthentication.SetAuthCookie(model.UserName, true);

                        GeneralFunctions.SetSession(Configs.session, userInfo);

                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("error", validateLogin.Reason);
                    }
                    //}
                    //else
                    //{
                    //    ModelState.AddModelError("Failed", "Username or password is incorrect!");
                    //}
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            //GeneralFunctions.DestroySession("User");
            return RedirectToAction("Login", "Account");
        }
        #endregion VIEW


        #region Private
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion Private
    }
}