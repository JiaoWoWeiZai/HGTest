using System;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace CrHgWcfService
{

    public class CustomServiceHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(
           Type serviceType, Uri[] baseAddresses)
        {
            var customServiceHost =
               new CustomServiceHost(serviceType, baseAddresses);
            return customServiceHost;
        }
    }

    public class CustomServiceHost : ServiceHost
    {
        public CustomServiceHost(Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            log4net.Config.XmlConfigurator.Configure();
        }
        protected override void ApplyConfiguration()
        {
            base.ApplyConfiguration();
        }
    }
}