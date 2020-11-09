using System.Web;
using System.Web.Optimization;

namespace templateProject
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Themes/Core").Include(
                "~/Content/css/bootstrap.min.css",
                "~/Content/css/atlantis.min.css",
                "~/Content/js/plugin/bootstrap-datepicker/css/bootstrap-datepicker.min.css"
                ));

            bundles.Add(new ScriptBundle("~/Script/bootstrap").Include(
                        "~/Content/js/core/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Themes/Custom").Include(
                "~/Content/css/custom.css"
                ));

            bundles.Add(new ScriptBundle("~/Script/FormUnobtrusive").Include(
                        "~/Content/js/jquery.validate.min.js",
                        "~/Content/js/jquery.validate.unobtrusive.min.js",
                        "~/Content/js/jquery.unobtrusive-ajax.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/Script/Font").Include(
                        "~/Content/js/webfont.min.js"));

            bundles.Add(new ScriptBundle("~/Script/DataTable").Include(
                        "~/Content/js/plugin/datatables/datatables.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/Script/jquery").Include(
                        "~/Content/js/core/jquery.3.2.1.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/Script/Core").Include(
                        "~/Content/js/plugin/jquery-ui-1.12.1.custom/jquery-ui.min.js",
                        "~/Content/js/plugin/moment/moment.min.js",
                        "~/Content/js/plugin/bootstrap-datepicker/js/bootstrap-datepicker.min.js",
                        "~/Content/js/core/popper.min.js",
                        "~/Content/js/core/bootstrap.min.js",
                        "~/Content/js/plugin/jquery-ui-touch-punch/jquery.ui.touch-punch.min.js",
                        "~/Content/js/plugin/bootstrap-toggle/bootstrap-toggle.min.js",
                        "~/Content/js/plugin/jquery-scrollbar/jquery.scrollbar.min.js",
                        "~/Content/js/atlantis.min.js",
                        "~/Content/js/setting-demo.js",
                        "~/Content/js/plugin/bootstrap-notify/bootstrap-notify.min.js",
                        "~/Content/js/plugin/select2/select2.full.min.js",
                        "~/Content/js/plugin/bootstrap-datepicker/js/bootstrap-datetimepicker.min.js",
                        "~/Content/js/plugin/bootstrap-tagsinput/bootstrap-tagsinput.min.js",
                        "~/Content/js/moment.min.js",
                        "~/Content/js/HelperScripts.js"));

            bundles.IgnoreList.Clear();
            BundleTable.EnableOptimizations = false;
        }
    }
}
