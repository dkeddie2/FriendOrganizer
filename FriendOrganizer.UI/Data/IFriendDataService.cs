namespace FriendOrganizer.UI.Data
{
    using System.Collections.Generic;
    using FriendOrganizer.Model;

    public interface IFriendDataService
    {
        IEnumerable<Friend> GetAll();
    }
}