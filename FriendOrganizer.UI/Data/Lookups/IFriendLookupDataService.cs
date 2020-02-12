namespace FriendOrganizer.UI.Data.Lookups
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FriendOrganizer.Model;

    public interface IFriendLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetFriendLookupAsync();
    }
}
