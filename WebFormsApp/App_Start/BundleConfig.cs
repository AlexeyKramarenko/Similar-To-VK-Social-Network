using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace WebFormsApp.App_Start
{
    class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/SignalR").Include(
                                                                "~/Scripts/jquery.signalR-2.2.0.min.js",
                                                                "~/Scripts/hubs.js"));
        }
    }
}
