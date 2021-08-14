using Autofac;

namespace CQRSApiTemplate.DI
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<MediatorModule>();
        }
    }
}
