namespace FriendOrganizer.UI.Data.Repositories
{
    using System.Threading.Tasks;
    using FriendOrganizer.Model;

    public interface IFriendRepository
    {
        Task<Friend> GetByIdAsync(int friendId);

        Task SaveAsync();

        bool HasChanges();

        void Add(Friend friend);

        void Remove(Friend model);
    }
}