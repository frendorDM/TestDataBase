using Autofac;
namespace WebChatApp.ServicesApp
{
    public class BllModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(this.ThisAssembly).AsImplementedInterfaces();

        }
    }
}
