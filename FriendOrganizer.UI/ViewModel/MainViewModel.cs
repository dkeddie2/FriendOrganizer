namespace FriendOrganizer.UI.ViewModel
{
    using System;
    using System.Threading.Tasks;
    using FriendOrganizer.UI.Event;
    using Prism.Events;

    public class MainViewModel : ViewModelBase
    {
        private readonly IEventAggregator eventAggregator;
        private Func<IFriendDetailViewModel> friendDetailViewModelCreator;
        private IFriendDetailViewModel friendDetailViewModel;

        public MainViewModel(INavigationViewModel navigationViewModel, Func<IFriendDetailViewModel> friendDetailViewModelCreator, IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.NavigationViewModel = navigationViewModel;
            this.friendDetailViewModelCreator = friendDetailViewModelCreator;
            this.eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Subscribe(OnOpenFriendDetailView);
        }

        public INavigationViewModel NavigationViewModel { get; }

        public IFriendDetailViewModel FriendDetailViewModel
        {
            get { return friendDetailViewModel; }
            set
            {
                friendDetailViewModel = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        private async void OnOpenFriendDetailView(int friendId)
        {
            FriendDetailViewModel = friendDetailViewModelCreator();
            await FriendDetailViewModel.LoadAsync(friendId);
        }
    }
}
