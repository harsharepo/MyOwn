using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Vedica
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Complete jQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-2.2.3.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/bootstrap-datetimepicker.min.js",
                 "~/Scripts/jquery.geolocation.edit.min",
                 "~/Scripts/jquery.themepunch.tools.min.js",
                "~/Scripts/jquery.themepunch.revolution.min.js",
                "~/Scripts/revolution.extension.layeranimation.min.js",
                "~/Scripts/revolution.extension.navigation.min.js",
                "~/Scripts/revolution.extension.parallax.min.js",
                "~/Scripts/revolution.extension.slideanims.min.js",
                "~/Scripts/revolution.extension.video.min.js",
                "~/Scripts/slider.js",
                "~/Scripts/owl.carousel.min.js",
                "~/Scripts/jquery.parallax-1.1.3.js",
                "~/Scripts/parallax.js",
                "~/Scripts/jquery.mixitup.min.js",
                "~/Scripts/jquery.fancybox.js",
                "~/Scripts/functions.js",
                "~/Scripts/fancy/helpers/jquery.fancybox-buttons.js",
                "~/Scripts/fancy/helpers/jquery.fancybox-media.js",
                "~/Scripts/fancy/helpers/jquery.fancybox-thumbs.js"));
            #endregion

            #region Style Bundle
            bundles.Add(new StyleBundle("~/bundles/css").Include("~/Content/bootstrap.css",
                "~/Content/font-awesome.css",
                "~/Content/font-awesome.min.css",
                "~/Content/medical-guide-icons.css",
                "~/Content/animate.min.css",
                "~/Content/settings.css",
                "~/Content/navigation.css",
                "~/Content/owl.carousel.css",
                "~/Content/owl.transitions.css",
                "~/Content/jquery.fancybox.css",
                "~/Content/zerogrid.css",
                "~/Content/style.css",
                "~/Content/loader.css",
                "~/Content/jquery.fancybox-buttons.css",
                "~/Content/jquery.fancybox-thumbs.css"));

            #endregion

            BundleTable.EnableOptimizations = true;
        }
    }
}