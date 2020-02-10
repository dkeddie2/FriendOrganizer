namespace FriendOrganizer.UI.Data
{
    using System.Threading.Tasks;
    using FriendOrganizer.Model;

    public interface IFriendDataService
    {
        Task<Friend> GetByIdAsync(int friendId);

        Task SaveAsync(Friend friend);
    }
}