namespace FriendOrganizer.UI.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using FriendOrganizer.Model;
    using FriendOrganizer.UI.Data;
    using FriendOrganizer.UI.Event;
    using Prism.Events;

    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private IFriendLookupDataService friendLookupService;
        private IEventAggregator eventAggregator;
        private LookupItem selectedFriend;

        public NavigationViewModel(IFriendLookupDataService friendLookupService, IEventAggregator eventAggregator)
        {
            this.friendLookupService = friendLookupService;
            this.eventAggregator = eventAggregator;
            Friends = new ObservableCollection<LookupItem>();
        }

        public ObservableCollection<LookupItem> Friends { get; }

        public LookupItem SelectedFriend
        {
            get { return selectedFriend; }
            set
            {
                selectedFriend = value;
                OnPropertyChanged();
                if (selectedFriend != null)
                {
                    eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Publish(selectedFriend.Id);
                }
            }
        }

        public async Task LoadAsync()
        {
            var lookup = await friendLookupService.GetFriendLookupAsync();
            Friends.Clear();

            foreach (var item in lookup)
            {
                Friends.Add(item);
            }
        }
    }
}
