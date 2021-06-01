using System.ServiceProcess;
using Autofac;
using ScanEventWorker.Database.Context;
using ScanEventWorker.Logging;
using ScanEventWorker.Logging.Interfaces;
using ScanEventWorker.Repository;
using ScanEventWorker.Repository.Interfaces;
using ScanEventWorker.Services;
using ScanEventWorker.Services.Interfaces;

namespace ScanEventWorker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var container = SetupDiContainer(); 
            ServiceBase.Run(container.Resolve<ParcelEventsService>());
        }

        private static IContainer SetupDiContainer()
        {
            var containerBuilder = new ContainerBuilder();

            // windows service
            containerBuilder.RegisterType<ParcelEventsService>().AsSelf().InstancePerLifetimeScope();
            
            // service classes
            containerBuilder.RegisterType<ParcelService>().As<IParcelService>().InstancePerLifetimeScope();

            // repository classes
            containerBuilder.RegisterType<ParcelRepository>().As<IParcelRepository>().InstancePerLifetimeScope();

            // logging
            containerBuilder.RegisterType<Logger>().As<ILogger>().InstancePerLifetimeScope();
            
            // database context
            containerBuilder.RegisterType<DatabaseContext>().AsSelf().InstancePerLifetimeScope();

            return containerBuilder.Build();
        }
    }
}
