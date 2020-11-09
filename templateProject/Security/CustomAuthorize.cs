using System;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;

using templateProject.Model;
using templateProject.Repository.Common;
using templateProject.Helper;

namespace templateProject.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    //[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class CustomAuthorize : AuthorizeAttribute
    {
        //menjadi modul
        //public string Users { get; set; }
        // u/ roles (create, read, update, delete)
        //public string Roles { get; set; }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {

                var redirectToUrl = "/Account/Login?ReturnUrl=" + filterContext.HttpContext.Request.UrlReferrer.PathAndQuery;
                filterContext.Result = new JavaScriptResult() { Script = "window.location = '" + redirectToUrl + "'" };
            }
            else
            {
                //base.HandleUnauthorizedRequest(filterContext);
                string q = filterContext.HttpContext.Request.Url.PathAndQuery == null ? "" : filterContext.HttpContext.Request.Url.PathAndQuery;
                filterContext.Result = new RedirectResult("~/Account/Login?ReturnUrl=" + q);
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string Module = Users;
            using (UnitOfWork uow = new UnitOfWork())
            {
                string UserName = httpContext.User.Identity.Name;
                string PageUrl = httpContext.Request.Path;

                if (PageUrl.ToLower().Contains("/index"))
                {
                    PageUrl = PageUrl.ToLower().Replace("/index", "/");
                }

                //check session tersedia atau enggak
                UserInfoModel userInfo = (UserInfoModel)System.Web.HttpContext.Current.Session[Configs.session];

                if (!string.IsNullOrEmpty(UserName))
                {
                    if (userInfo == null)
                    {
                        userInfo = uow.UserRepository.SelectUserInfo(UserName);
                        if (userInfo.UserID != 0)
                        {
                            System.Web.HttpContext.Current.Session[Configs.session] = userInfo;
                        }
                        else
                        {
                            FormsAuthentication.SignOut();
                            return false;
                        }
                    }

                    //Check Permission
                    UserInfoAccessModel ugs = uow.GroupUserMenuRepository.GetPageAccessByGroupNModul(userInfo, Module);
                    UserInfoAccessModel access = new UserInfoAccessModel();
                    access.AllowCreate = false;
                    access.AllowRead = false;
                    access.AllowUpdate = false;
                    access.AllowDelete = false;

                    if (ugs != null && !string.IsNullOrEmpty(Roles))
                    {
                        access.AllowCreate = ugs.AllowCreate;
                        access.AllowRead = ugs.AllowRead;
                        access.AllowUpdate = ugs.AllowUpdate;
                        access.AllowDelete = ugs.AllowDelete;
                        userInfo.InfoAccess = access;
                        System.Web.HttpContext.Current.Session[Configs.session] = userInfo;



                        switch (Roles.ToLower())
                        {
                            case "create":
                                return ugs.AllowCreate;
                            case "read":
                                return ugs.AllowRead;
                            case "update":
                                return ugs.AllowUpdate;
                            case "delete":
                                return ugs.AllowDelete;
                            default:
                                return false;
                        }
                    }
                    else if (ugs == null && PageUrl != "" && PageUrl != "/")
                    {
                        userInfo.InfoAccess = access;
                        System.Web.HttpContext.Current.Session[Configs.session] = userInfo;

                        return false;
                    }

                    userInfo.InfoAccess = access;
                    System.Web.HttpContext.Current.Session[Configs.session] = userInfo;
                }
                else
                {
                    FormsAuthentication.SignOut();
                    return false;
                }

                return base.AuthorizeCore(httpContext);
            }
        }
    }
}