using Autofac;
using ElecronicsStore.Core;
using ElecronicsStore.DB.Storages;
using ElectronicsStore.Core.ConfigurationOptions;
using ElectronicsStore.Repository;

namespace ElectronicsStore.API.Configuration
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ReportStorage>().As<IReportStorage>();
            builder.RegisterType<ReportRepository>().As<IReportRepository>();

            builder.RegisterType<ProductStorage>().As<IProductStorage>();
            builder.RegisterType<ProductRepository>().As<IProductRepository>();

            builder.RegisterType<OrderStorage>().As<IOrderStorage>();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();

            builder.RegisterType<UserStorage>().As<IUserStorage>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();

            builder.RegisterType<StorageOptions>().As<IStorageOptions>();
            builder.RegisterType<UrlOptions>().As<IUrlOptions>();

            builder.RegisterType<CurrencyConverter>().As<ICurrencyConverter>();
        }
    }
}
