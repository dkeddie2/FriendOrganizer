namespace FriendOrganizer.UI.Startup
{
    using Autofac;
    using FriendOrganizer.UI.Data;
    using FriendOrganizer.UI.ViewModel;

    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            // setting up Autofac stuff
            var builder = new ContainerBuilder();
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<FriendDataService>().As<IFriendDataService>();

            return builder.Build();
        }
    }
}
