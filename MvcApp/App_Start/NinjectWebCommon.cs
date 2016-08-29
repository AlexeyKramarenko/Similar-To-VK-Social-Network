[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MvcApp.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(MvcApp.App_Start.NinjectWebCommon), "Stop")]

namespace MvcApp.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Core.DAL.Interfaces;
    using Core.BLL;
    using Core.BLL.Interfaces;
    using Core.DAL;
    using MvcApp;
    using System.Web.Http;
    using System.Collections.Generic;
    using Ninject.Syntax;
    using Microsoft.AspNet.SignalR;
    using System.Web.Http.Dependencies;
    using System.Linq;
    using Ninject.Parameters;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();


                RegisterWithWebApi(kernel);

                RegisterWithSignalr(kernel);

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        #region SignalR
        private class NinjectSignalRDependencyResolver : Microsoft.AspNet.SignalR.DefaultDependencyResolver
        {
            private readonly IKernel kernel;
            public NinjectSignalRDependencyResolver(IKernel kernel)
            {
                this.kernel = kernel;
            }

            public override object GetService(Type serviceType)
            {
                return kernel.TryGet(serviceType) ?? base.GetService(serviceType);
            }

            public override System.Collections.Generic.IEnumerable<object> GetServices(Type serviceType)
            {
                return kernel.GetAll(serviceType).Concat(base.GetServices(serviceType));
            }

        }
        private static void RegisterWithSignalr(IKernel kernel)
        {
            GlobalHost.DependencyResolver = new NinjectSignalRDependencyResolver(kernel);
        }
        #endregion
        #region WebAPi
        public class NinjectWebApiResolver : NinjectScope, System.Web.Http.Dependencies.IDependencyResolver
        {
            private readonly IKernel _kernel;
            public NinjectWebApiResolver(IKernel kernel)
                : base(kernel)
            {
                _kernel = kernel;
            }
            public IDependencyScope BeginScope()
            {
                return new NinjectScope(_kernel.BeginBlock());
            }
        }

        public class NinjectScope : System.Web.Http.Dependencies.IDependencyScope
        {
            protected IResolutionRoot resolutionRoot;
            public NinjectScope(IResolutionRoot kernel)
            {
                resolutionRoot = kernel;
            }
            public object GetService(Type serviceType)
            {
                Ninject.Activation.IRequest request = resolutionRoot.CreateRequest(serviceType, null, new Parameter[0], true, true);
                return resolutionRoot.Resolve(request).SingleOrDefault();
            }
            public IEnumerable<object> GetServices(Type serviceType)
            {
                Ninject.Activation.IRequest request = resolutionRoot.CreateRequest(serviceType, null, new Parameter[0], true, true);
                return resolutionRoot.Resolve(request).ToList();
            }
            public void Dispose()
            {
                IDisposable disposable = (IDisposable)resolutionRoot;
                if (disposable != null) disposable.Dispose();
                resolutionRoot = null;
            }
        }

        static void RegisterWithWebApi(IKernel kernel)
        {
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectWebApiResolver(kernel);
        }
        #endregion

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {

            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();

            kernel.Bind<ICountriesRepository>().To<CountriesRepository>().InRequestScope();
            kernel.Bind<IRelationshipsRepository>().To<RelationshipsRepository>().InRequestScope();
            kernel.Bind<IMessagesRepository>().To<MessagesRepository>().InRequestScope();
            kernel.Bind<IPhotoRepository>().To<PhotoRepository>().InRequestScope();
            kernel.Bind<IProfileRepository>().To<ProfileRepository>().InRequestScope();
            kernel.Bind<ISettingsRepository>().To<SettingsRepository>().InRequestScope();
            kernel.Bind<IUserRepository>().To<ApplicationUserManager>().InRequestScope();
            kernel.Bind<IWallStatusRepository>().To<WallStatusRepository>().InRequestScope();

            kernel.Bind<IMappingService>().To<MappingService>().InRequestScope();

            kernel.Bind<ICountriesService>().To<CountriesService>().InRequestScope();
            kernel.Bind<IEmailService>().To<EmailService>().InRequestScope();
            kernel.Bind<ILuceneService>().To<LuceneService>().InRequestScope();
            kernel.Bind<IPhotoService>().To<PhotoService>().InRequestScope();
            kernel.Bind<IProfileService>().To<ProfileService>().InRequestScope();
            kernel.Bind<IRelationshipsService>().To<RelationshipsService>().InRequestScope();
            kernel.Bind<ISettingsService>().To<SettingsService>().InRequestScope();
            kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            kernel.Bind<IWallStatusService>().To<WallStatusService>().InRequestScope();
            kernel.Bind<IMessagesService>().To<MessagesService>().InRequestScope();
            kernel.Bind<IChatHub>().To<ChatHub>().InRequestScope();

        }
    }
    //public static class NinjectWebCommon 
    //{
    //    private static readonly Bootstrapper bootstrapper = new Bootstrapper();

    //    /// <summary>
    //    /// Starts the application
    //    /// </summary>
    //    public static void Start() 
    //    {
    //        DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
    //        DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
    //        bootstrapper.Initialize(CreateKernel);
    //    }

    //    /// <summary>
    //    /// Stops the application.
    //    /// </summary>
    //    public static void Stop()
    //    {
    //        bootstrapper.ShutDown();
    //    }

    //    /// <summary>
    //    /// Creates the kernel that will manage your application.
    //    /// </summary>
    //    /// <returns>The created kernel.</returns>
    //    private static IKernel CreateKernel()
    //    {
    //        var kernel = new StandardKernel();

    //        //Instruct the Kernel to rebind the HttpConfiguration to the default config instance provided from the GlobalConfiguration
    //        kernel.Rebind<HttpConfiguration>().ToMethod(context => GlobalConfiguration.Configuration);

    //        try
    //        {
    //            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
    //            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

    //            RegisterServices(kernel);
    //            return kernel;
    //        }
    //        catch
    //        {
    //            kernel.Dispose();
    //            throw;
    //        }
    //    }

    //    /// <summary>
    //    /// Load your modules or register your services here!
    //    /// </summary>
    //    /// <param name="kernel">The kernel.</param>
    //    private static void RegisterServices(IKernel kernel)
    //    {
    //        kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();

    //        kernel.Bind<ICountriesRepository>().To<CountriesRepository>().InRequestScope();
    //        kernel.Bind<IRelationshipsRepository>().To<RelationshipsRepository>().InRequestScope();
    //        kernel.Bind<IMessagesRepository>().To<MessagesRepository>().InRequestScope();
    //        kernel.Bind<IPhotoRepository>().To<PhotoRepository>().InRequestScope();
    //        kernel.Bind<IProfileRepository>().To<ProfileRepository>().InRequestScope();
    //        kernel.Bind<ISettingsRepository>().To<SettingsRepository>().InRequestScope();
    //        kernel.Bind<IUserRepository>().To<ApplicationUserManager>().InRequestScope();
    //        kernel.Bind<IWallStatusRepository>().To<WallStatusRepository>().InRequestScope();

    //        kernel.Bind<IMappingService>().To<MappingService>().InRequestScope();

    //        kernel.Bind<ICountriesService>().To<CountriesService>().InRequestScope();
    //        kernel.Bind<IEmailService>().To<EmailService>().InRequestScope();
    //        kernel.Bind<ILuceneService>().To<LuceneService>().InRequestScope();
    //        kernel.Bind<IPhotoService>().To<PhotoService>().InRequestScope();
    //        kernel.Bind<IProfileService>().To<ProfileService>().InRequestScope();
    //        kernel.Bind<IRelationshipsService>().To<RelationshipsService>().InRequestScope();
    //        kernel.Bind<ISettingsService>().To<SettingsService>().InRequestScope();
    //        kernel.Bind<IUserService>().To<UserService>().InRequestScope();
    //        kernel.Bind<IWallStatusService>().To<WallStatusService>().InRequestScope();
    //        kernel.Bind<IMessagesService>().To<MessagesService>().InRequestScope();
    //        kernel.Bind<IChatHub>().To<ChatHub>().InRequestScope();

    //    }
    //}
}
