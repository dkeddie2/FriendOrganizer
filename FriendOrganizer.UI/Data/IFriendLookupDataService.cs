namespace FriendOrganizer.UI.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FriendOrganizer.Model;

    public interface IFriendLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetFriendLookupAsync();
    }
}
