#region

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using WebUI.ServiceReferenceJoueur;
using WebUI.ServiceReferencePartie;

#endregion

namespace WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IGestionJoueur>().To<GestionJoueurClient>();
            kernel.Bind<IGestionPartie>().To<GestionPartieClient>();
        }
    }
}