using Autofac;
using ElecronicsStore.Core;
using ElecronicsStore.DB.Storages;
using ElectronicsStore.API.Controllers;
using ElectronicsStore.Core.ConfigurationOptions;
using ElectronicsStore.Repository;

namespace ElectronicsStore.API.Configuration
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ReportStorage>().As<IReportStorage>();
            builder.RegisterType<ReportController>().As<IReportController>();
            builder.RegisterType<ReportRepository>().As<IReportRepository>();

            builder.RegisterType<ProductStorage>().As<IProductStorage>();

            builder.RegisterType<OrderStorage>().As<IOrderStorage>();
            builder.RegisterType<OrderController>().As<IOrderController>();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();

            builder.RegisterType<StorageOptions>().As<IStorageOptions>();
            builder.RegisterType<UrlOptions>().As<IUrlOptions>();

            builder.RegisterType<CurrencyConverter>().As<ICurrencyConverter>();
        }
    }
}
