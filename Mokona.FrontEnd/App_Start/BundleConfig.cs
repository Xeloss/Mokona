namespace Mokona.FrontEnd
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterStyleBundles(bundles);
            RegisterScriptsBundles(bundles);
        }

        public static void RegisterStyleBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/layout")
                   .Include(
                            "~/Content/Styles/bootstrap.css"
                          , "~/Content/Styles/font-awesome.css"
                          , "~/Content/Styles/_buttons.css"
                          , "~/Content/Styles/_forms.css"
                          , "~/Content/Styles/_layout.css"
                          , "~/Content/Styles/_modal.css"
                          , "~/Content/Styles/_navigation.css"
                          , "~/Content/Styles/_scrollbars.css"
                          , "~/Content/Styles/_tables.css"
                          , "~/Content/Styles/site.css"
                   ));
        }

        public static void RegisterScriptsBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/script/externals").Include(
                          "~/Scripts/bootstrap.min.js"
                        
                        , "~/Scripts/angular.min.js"
                        , "~/Scripts/angular-cookies.min.js"
                        , "~/Scripts/angular-route.js"
                        , "~/Scripts/angular-animate.min.js"
                        , "~/Scripts/angular-ui/ui-bootstrap.min.js"
                        , "~/Scripts/loading-bar.min.js"
            ));

            bundles.Add(new ScriptBundle("~/script/common").Include(
                  "~/Scripts/JSExtensions.js"
                , "~/Scripts/Common/common.js"
                , "~/Scripts/Common/routeConfig.js"
                , "~/Scripts/Common/language.js"
            ));

            bundles.Add(new ScriptBundle("~/script/services").Include(
                  "~/Scripts/Angular-extras/Services/BaseResourceService.js"
                , "~/Scripts/Angular-extras/Services/UserService.js"
                , "~/Scripts/Angular-extras/Services/CompanyService.js"
            ));

            bundles.Add(new ScriptBundle("~/script/directives").Include(
                  "~/Scripts/Angular-extras/Directives/PagerDirective.js"
                , "~/Scripts/Angular-extras/Directives/ExpenseDetailDirective.js"
            ));

            bundles.Add(new ScriptBundle("~/script/controllers").Include(

                  "~/Scripts/Angular-extras/BaseController.js"
            ));

        }
    }
}