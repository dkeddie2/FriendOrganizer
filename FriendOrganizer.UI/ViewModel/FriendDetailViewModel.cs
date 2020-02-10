using System.Threading.Tasks;
using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;

namespace FriendOrganizer.UI.ViewModel
{
    public class FriendDetailViewModel : ViewModelBase, IFriendDetailViewModel
    {
        private IFriendDataService dataService;
        private Friend friend;

        public FriendDetailViewModel(IFriendDataService dataService)
        {
            this.dataService = dataService;
        }

        public async Task LoadAsync(int friendId)
        {
            Friend = await dataService.GetByIdAsync(friendId);
        }

        public Friend Friend
        {
            get { return friend; }
            private set
            {
                friend = value;
                OnPropertyChanged();
            }
        }
    }
}
