namespace FriendOrganizer.UI.ViewModel
{
    using System.Threading.Tasks;

    public interface IFriendDetailViewModel
    {
        Task LoadAsync(int friendId);
    }
}
