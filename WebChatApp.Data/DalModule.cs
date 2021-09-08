using Autofac;
using WebChatApp.Core.Session;

namespace WebChatApp.Data
{
    public class DalModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Assembly types
            builder.RegisterAssemblyTypes(ThisAssembly).AsImplementedInterfaces();

            // DB context
            builder.RegisterType<ApplicationContext>().AsSelf();

            // Session and its provider
            builder.RegisterType<SessionProvider>().As<ISessionProvider>().InstancePerLifetimeScope();
            builder.Register(ctx => ctx.Resolve<ISessionProvider>().CurrentSession).As<ISession>();

        }
    }
}
