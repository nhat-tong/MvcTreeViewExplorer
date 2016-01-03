#region using
using System.Web.Optimization;
#endregion

namespace MvcTreeViewExplorer.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/Site.css",
                "~/Content/fancytree/ui.fancytree.min.css",
                "~/Content/jquery-ui/jquery-ui.min.css",
                "~/Content/jquery-ui/jquery-ui.structure.min.css",
                "~/Content/jquery-ui/jquery-ui.theme.min.css"));

            //jquery.unobtrusive-ajax.min: permit to enable ajax call
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                         "~/Scripts/jquery/jquery-1.10.2.min.js",
                         "~/Scripts/jquery-ajax/jquery.unobtrusive-ajax.min.js",
                         "~/Scripts/jquery-validate/jquery.validate.min.js",
                         "~/Scripts/jquery-validate/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/boostrap/bootstrap.min.js",
                        "~/Scripts/modernizr-2.6.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                      "~/Scripts/jquery-ui/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/fancytree").Include(
                       "~/Scripts/fancytree/jquery.fancytree-all.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/treeview").Include(
                     "~/Scripts/treeview.js"));

            // Code removed for clarity.
            //BundleTable.EnableOptimizations = true;
        }
    }
}