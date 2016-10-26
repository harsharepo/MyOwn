using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace VedicaAdmin
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Complete jQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-ui.min.js",
                "~/Scripts/raphael-min.js",
                "~/plugins/sparkline/jquery.sparkline.min.js",
                "~/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                "~/plugins/jvectormap/jquery-jvectormap-world-mill-en.js",
                "~/plugins/knob/jquery.knob.js",
                "~/Scripts/moment.min.js",
                "~/plugins/daterangepicker/daterangepicker.js",
                "~/plugins/datepicker/bootstrap-datepicker.js",
                "~/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js",
                "~/plugins/slimScroll/jquery.slimscroll.min.js",
                "~/plugins/fastclick/fastclick.min.js",
                "~/dist/js/app.min.js",
                "~/Scripts/pageScripts.js",
                "~/Scripts/FileUpload/UploadFile.js"));
            #endregion

            #region Style Bundle
            bundles.Add(new StyleBundle("~/bundles/css").Include("~/Content/bootstrap.css",
                "~/Content/font-awesome.css",
                "~/Content/font-awesome.min.css",
                "~/Content/ionicons.min.css",
                "~/Content/AdminLTE.min.css",
                "~/dist/css/skins/_all-skins.min.css",
                "~/plugins/iCheck/flat/blue.css",
                "~/plugins/morris/morris.css",
                "~/plugins/jvectormap/jquery-jvectormap-1.2.2.css",
                "~/plugins/datepicker/datepicker3.css",
                "~/plugins/daterangepicker/daterangepicker.css",
                "~/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css",
                "~/Content/Site.css",
                "~/Content/bootstrap.min.css",
                "~/Content/DataTables/css/dataTables.bootstrap.css",
                "~/plugins/select2/select2.min.css"));

            #endregion

            BundleTable.EnableOptimizations = true;
        }
    }
}