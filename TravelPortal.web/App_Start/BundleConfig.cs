using System.Web;
using System.Web.Optimization;

namespace TravelPortal.web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/admin/js").Include(
                    "~/Scripts/admin/js/feather.min.js",
                    "~/Scripts/admin/js/jquery.slimscroll.min.js",
                    "~/Scripts/admin/js/jquery.dataTables.min.js",
                    "~/Scripts/admin/js/dataTables.bootstrap5.min.js",
                    "~/Scripts/admin/js/bootstrap.bundle.min.js",
                    "~/Scripts/admin/js/moment.min.js",
                    "~/Scripts/admin/js/custom-select2.js",
                    "~/Scripts/admin/js/theme-colorpicker.js",
                    "~/Scripts/admin/js/script.js",
                    "~/Scripts/admin/js/rocket-loader.min.js"
          ));

            bundles.Add(new Bundle("~/bundles/admin/plugins").Include(
                "~/Content/admin/plugins/chartjs/chart.min.js",
                "~/Content/admin/plugins/chartjs/chart-data.js",
                "~/Content/admin/plugins/daterangepicker/daterangepicker.js",
                "~/Content/admin/plugins/select2/js/select2.min.js",
                "~/Content/admin/plugins/moment/moment.min.js",
                "~/Content/admin/plugins/flatpickr/flatpickr.js",
                "~/Content/admin/plugins/simonwep/pickr/pickr.es5.min.js",
                "~/Content/admin/plugins/toast-plugin/jquery.toast.min.js",
                "~/Content/admin/plugins/jquery-ui/jquery-ui.js"
            ));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // Admin Bundles
            bundles.Add(new StyleBundle("~/admin/admincss").Include(
                      "~/Content/admin/css/bootstrap.min.css",
                      "~/Content/admin/css/bootstrap-datetimepicker.min.css",
                      "~/Content/admin/css/animate.css",
                      "~/Content/admin/css/dataTables.bootstrap5.min.css",
                      "~/Content/admin/css/admin.css",
                      "~/Content/admin/css/loader.css"
                      ));
            bundles.Add(new StyleBundle("~/admin/css/plugins").Include(
                      "~/Content/admin/plugins/select2/css/select2.min.css",
                      "~/Content/admin/plugins/daterangepicker/daterangepicker.css",
                      "~/Content/admin/plugins/tabler-icons/tabler-icons.min.css",
                      "~/Content/admin/plugins/flatpickr/flatpickr.min.css",
                      "~/Content/admin/plugins/fontawesome/css/fontawesome.min.css",
                      "~/Content/admin/plugins/fontawesome/css/all.min.css",
                      "~/Content/admin/plugins/simonwep/pickr/themes/nano.min.css",
                      "~/Content/admin/plugins/toast-plugin/jquery.toast.min.css",
                      "~/Content/admin/plugins/jquery-ui/jquery-ui.css"
                      ));

          

        }
    }
}
