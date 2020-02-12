namespace FriendOrganizer.UI.ViewModel
{
    using System;
    using System.Threading.Tasks;
    using FriendOrganizer.UI.Event;
    using FriendOrganizer.UI.View.Services;
    using Prism.Events;

    public class MainViewModel : ViewModelBase
    {
        private readonly IEventAggregator eventAggregator;
        private Func<IFriendDetailViewModel> friendDetailViewModelCreator;
        private IFriendDetailViewModel friendDetailViewModel;
        private readonly IMessageDialogService messageDialogService;

        public MainViewModel(INavigationViewModel navigationViewModel, Func<IFriendDetailViewModel> friendDetailViewModelCreator, IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            this.messageDialogService = messageDialogService;
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
            if (FriendDetailViewModel != null && FriendDetailViewModel.HasChanges)
            {
                var result = messageDialogService.ShowOkCancelDialog("You've made changes. Navigate away?", "Question");

                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }

            FriendDetailViewModel = friendDetailViewModelCreator();
            await FriendDetailViewModel.LoadAsync(friendId);
        }
    }
}
