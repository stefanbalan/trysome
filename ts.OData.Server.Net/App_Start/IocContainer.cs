using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Castle.MicroKernel;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace ts.OData.Server.Net
{
    public static class IocContainer
    {
        private static IWindsorContainer _container;

        public static void Setup(HttpConfiguration config)
        {
            _container = new WindsorContainer().Install(FromAssembly.This());

            //var controllerFactory = new WindsorControllerFactory(_container.Kernel);
            //ControllerBuilder.Current.SetControllerFactory(controllerFactory);

            var controllerAtivator = new WindsorControllerActivator(_container.Kernel);

            config.Services.Replace(typeof(IHttpControllerActivator), controllerAtivator);
        }
    }

    public class WindsorControllerActivator : IHttpControllerActivator
    {
        private readonly IKernel _kernel;

        public WindsorControllerActivator(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controller = _kernel.Resolve(controllerType);
            request.RegisterForDispose(new DisposableWrapper(() => _kernel.ReleaseComponent(controller)));
            return (IHttpController)controller;
        }
    }

    public class DisposableWrapper : IDisposable
    {
        private readonly Action _disposeAction;
        public DisposableWrapper(Action disposeAction)
        {
            this._disposeAction = disposeAction;
        }

        public void Dispose()
        {
            _disposeAction();
        }
    }

}