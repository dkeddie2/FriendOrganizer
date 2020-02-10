namespace FriendOrganizer.UI.Startup
{
    using Autofac;
    using FriendOrganizer.DataAccess;
    using FriendOrganizer.UI.Data;
    using FriendOrganizer.UI.ViewModel;

    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            // setting up Autofac stuff
            var builder = new ContainerBuilder();

            builder.RegisterType<FriendOrganizerDbContext>().AsSelf();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();

            builder.RegisterType<LookupDataService>().AsImplementedInterfaces();
            builder.RegisterType<FriendDataService>().As<IFriendDataService>();

            return builder.Build();
        }
    }
}
