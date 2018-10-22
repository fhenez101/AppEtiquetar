using System.Web;
using System.Web.Optimization;

namespace App.Etiketarte.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Etiketarte
            bundles.Add(new StyleBundle("~/Bundles/Style/Etiketas").Include(
                "~/Content/Style/Etiketas/bootstrap.min.css",
                "~/Content/Style/Etiketas/animate.css",
                "~/Content/Style/Etiketas/font-awesome.min.css",
                "~/Content/Style/Etiketas/simple-line-icons.css",
                "~/Content/Style/Etiketas/owl.carousel.css",
                "~/Content/Style/Etiketas/owl.transitions.css",
                "~/Content/Style/Etiketas/flexslider.css",
                "~/Content/Style/Etiketas/revolution-slider.css",
                "~/Content/Style/Etiketas/jquery-ui.css",
                "~/Content/Style/Etiketas/jquery.fancybox.css",
                "~/Content/Style/Etiketas/jtv-mobile-menu.css",
                "~/Content/Style/Etiketas/style.css",
                "~/Content/Style/Etiketas/side.css",
                "~/Content/Style/Etiketas/responsive.css"
            ));

            bundles.Add(new ScriptBundle("~/Bundles/Script/Etiketas").Include(
                "~/Scripts/jquery-3.2.1.min.js",
                "~/Scripts/AjaxSettings/Settings.js",
                "~/Scripts/Etiketas/jquery-ui.js",
                "~/Scripts/Etiketas/bootstrap.min.js",
                "~/Scripts/Etiketas/owl.carousel.min.js",
                "~/Scripts/Etiketas/jtv-mobile-menu.js",
                "~/Scripts/Etiketas/main.js",
                "~/Scripts/Etiketas/rev-slider.js",
                "~/Scripts/Etiketas/slider-index.js"
            ));
            #endregion

            #region Pymes
            bundles.Add(new StyleBundle("~/Bundles/Style/Pymes").Include(
                "~/Content/bootstrap.css",
                "~/Content/Style/Pymes/soon.css"
            ));

            bundles.Add(new ScriptBundle("~/Bundles/Script/Pymes").Include(
                "~/Scripts/jquery-3.2.1.js",
                "~/Scripts/modernizr-2.8.3.js",
                "~/Scripts/Pymes/plugins.js",
                "~/Scripts/Pymes/jquery.themepunch.revolution.min.js",
                "~/Scripts/Pymes/custom.js"
            ));
            #endregion

            BundleTable.EnableOptimizations = true;
        }
    }
}
