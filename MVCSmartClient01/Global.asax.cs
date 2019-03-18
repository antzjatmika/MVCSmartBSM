using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.Mvc;
using System.Web.Optimization;
using MVCSmartClient01.Models;
using MVCSmartClient01.Controllers;
using System.Web.SessionState;

using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
//using MVCSmartClient01.Components;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Log4Net.config", Watch = true)]
namespace MVCSmartClient01
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.DefaultBinder = new DevExpress.Web.Mvc.DevExpressEditorsBinder();
            //RegisterMef();
        }

        //private void RegisterCustomControllerFactory()
        //{
        //    IControllerFactory factory = new CustomControllerFactory();
        //    ControllerBuilder.Current.SetControllerFactory(factory);
        //}
        private void RegisterMef()
        {
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var composition = new CompositionContainer(catalog);
            //IControllerFactory mefControllerFactory = new MefControllerFactory(composition);
            //ControllerBuilder.Current.SetControllerFactory(mefControllerFactory);
        }
    }

    //public class CustomControllerFactory : IControllerFactory
    //{
    //    public IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
    //    {
    //        IUserSession userSession = new UserSession();
    //        var controller = new MstTypeOfRekananController(userSession);
    //        return controller;
    //    }
    //    public System.Web.SessionState.SessionStateBehavior GetControllerSessionBehavior(
    //       System.Web.Routing.RequestContext requestContext, string controllerName)
    //    {
    //        return SessionStateBehavior.Default;
    //    }
    //    public void ReleaseController(IController controller)
    //    {
    //        IDisposable disposable = controller as IDisposable;
    //        if (disposable != null)
    //            disposable.Dispose();
    //    }
    //}

    //public class CustomControllerFactory : DefaultControllerFactory
    //{
    //    protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
    //    {
    //        IUserSession userSession = new UserSession();
    //        IController controller = Activator.CreateInstance(controllerType, new[] { userSession }) as Controller;
    //        return controller;
    //    }
    //}

    //public class MefControllerFactory : DefaultControllerFactory
    //{
    //    private readonly CompositionContainer _container;
    //    public MefControllerFactory(CompositionContainer container)
    //    {
    //        _container = container;
    //    }
    //    protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
    //    {
    //        Lazy<object, object> export = _container.GetExports(controllerType, null, null).FirstOrDefault();

    //        return null == export
    //                            ? base.GetControllerInstance(requestContext, controllerType)
    //                            : (IController)export.Value;
    //    }
    //    public override void ReleaseController(IController controller)
    //    {
    //        ((IDisposable)controller).Dispose();
    //    }
    //}
}