namespace FriendOrganizer.UI.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FriendOrganizer.Model;

    public interface IFriendDataService
    {
        Task<Friend> GetByIdAsync(int friendId);
    }
}