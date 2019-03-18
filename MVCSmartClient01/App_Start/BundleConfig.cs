﻿using System.Web;
using System.Web.Optimization;

namespace MVCSmartClient01
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.globalize/globalize.js",
                         "~/Scripts/knockout-3.0.0.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"
                        ));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                       "~/Content/bootstrap-custom.css",
                      "~/Content/Site.css",
                      "~/Content/Command.css",
                       "~/Content/SmartApps.css"));

            bundles.Add(new StyleBundle("~/Content/font").Include(
                      "~/Content/font/FuturaStd-Book.ttf",
                      "~/Content/font/FuturaStd-Heavy.ttf",
                      "~/Content/font/FuturaStd-Light.ttf",
                      "~/Content/font/FuturaStd-Medium.ttf"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                      "~/Scripts/knockout-{version}.js",
                      "~/Scripts/app.js",
                      "~/Scripts/SmartApps.js"));


            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            //BundleTable.EnableOptimizations = true; //use web.config compilation

        }
    }
}
