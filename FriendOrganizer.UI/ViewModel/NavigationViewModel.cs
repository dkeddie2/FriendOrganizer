namespace FriendOrganizer.UI.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using FriendOrganizer.Model;
    using FriendOrganizer.UI.Data;
    public class NavigationViewModel : INavigationViewModel
    {
        private IFriendLookupDataService friendLookupService;

        public NavigationViewModel(IFriendLookupDataService friendLookupService)
        {
            this.friendLookupService = friendLookupService;
            Friends = new ObservableCollection<LookupItem>();
        }

        public ObservableCollection<LookupItem> Friends { get; }

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
