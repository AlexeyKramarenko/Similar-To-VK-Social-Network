using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace WebFormsApp
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                         "~/Scripts/_base/promise.js",
                         "~/Scripts/_base/ajax.js",
                         "~/Scripts/_main/service.js",
                         "~/Scripts/_main/view.js",
                         "~/Scripts/_main/controller.js"));

            bundles.Add(new ScriptBundle("~/bundles/messages").Include(
                         "~/Scripts/_base/promise.js",
                         "~/Scripts/_base/ajax.js",
                         "~/Scripts/_messages/service.js",
                         "~/Scripts/_messages/view.js",
                         "~/Scripts/_messages/controller.js"));

            bundles.Add(new ScriptBundle("~/bundles/signalr.messages").Include(
                         "~/Scripts/_signalr.messages/view.js",
                         "~/Scripts/_signalr.messages/controller.js"));

            bundles.Add(new ScriptBundle("~/bundles/people").Include(
                     "~/Scripts/_base/promise.js",
                     "~/Scripts/_base/ajax.js",
                     "~/Scripts/_people/service.js",
                     "~/Scripts/_people/view.js",
                     "~/Scripts/_people/controller.js"));

            bundles.Add(new ScriptBundle("~/bundles/photos").Include(
                         "~/Scripts/_base/promise.js",
                         "~/Scripts/_base/ajax.js",
                         "~/Scripts/_photos/service.js",
                         "~/Scripts/_photos/view.js",
                         "~/Scripts/_photos/controller.js"));

            bundles.Add(new ScriptBundle("~/bundles/profile").Include(
                         "~/Scripts/_base/promise.js",
                         "~/Scripts/_base/ajax.js",
                         "~/Scripts/_profile/service.js",
                         "~/Scripts/_profile/view.js",
                         "~/Scripts/_profile/controller.js"));

            bundles.Add(new ScriptBundle("~/bundles/registration").Include(
                         "~/Scripts/_base/promise.js",
                         "~/Scripts/_base/ajax.js",
                         "~/Scripts/_registration/service.js",
                         "~/Scripts/_registration/view.js",
                         "~/Scripts/_registration/controller.js"));

            bundles.Add(new ScriptBundle("~/bundles/settings").Include(
                         "~/Scripts/_base/promise.js",
                         "~/Scripts/_base/ajax.js",
                         "~/Scripts/_settings/service.js",
                         "~/Scripts/_settings/view.js",
                         "~/Scripts/_settings/controller.js"));
             
            bundles.Add(new ScriptBundle("~/bundles/modalPopLite").Include(
                         "~/Scripts/jquery-1.7.1.min.js",
                         "~/Scripts/modalPopLite.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js" 
                      ));




            bundles.Add(new ScriptBundle("~/bundles/WebFormsJs").Include(
                            "~/Scripts/WebForms/WebForms.js",
                            "~/Scripts/WebForms/WebUIValidation.js",
                            "~/Scripts/WebForms/MenuStandards.js",
                            "~/Scripts/WebForms/Focus.js",
                            "~/Scripts/WebForms/GridView.js",
                            "~/Scripts/WebForms/DetailsView.js",
                            "~/Scripts/WebForms/TreeView.js",
                            "~/Scripts/WebForms/WebParts.js"));

            // Order is very important for these files to work, they have explicit dependencies
            bundles.Add(new ScriptBundle("~/bundles/MsAjaxJs").Include(
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjax.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxTimer.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.js"));

            // Use the Development version of Modernizr to develop with and learn from. Then, when you’re
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                            "~/Scripts/modernizr-*"));

            ScriptManager.ScriptResourceMapping.AddDefinition(
                "respond",
                new ScriptResourceDefinition
                {
                    Path = "~/Scripts/respond.min.js",
                    DebugPath = "~/Scripts/respond.js",
                });
        }
    }
}