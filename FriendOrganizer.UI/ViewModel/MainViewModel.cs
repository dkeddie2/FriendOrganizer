namespace FriendOrganizer.UI.ViewModel
{
    using System.Collections.ObjectModel;
    using FriendOrganizer.Model;
    using FriendOrganizer.UI.Data;

    public class MainViewModel : ViewModelBase
    {
        private IFriendDataService friendDataService;
        private Friend selectedFriend;

        public MainViewModel(IFriendDataService friendDataService)
        {
            Friends = new ObservableCollection<Friend>();
            this.friendDataService = friendDataService;
        }

        public void Load()
        {
            var friends = friendDataService.GetAll();
            Friends.Clear();

            foreach (var friend in friends)
            {
                Friends.Add(friend);
            }
        }

        public ObservableCollection<Friend> Friends { get; set; }

        public Friend SelectedFriend
        {
            get { return selectedFriend; }
            set
            {
                selectedFriend = value;
                OnPropertyChanged();
            }
        }
    }
}
