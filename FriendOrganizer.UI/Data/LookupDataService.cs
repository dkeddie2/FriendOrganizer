namespace FriendOrganizer.UI.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using FriendOrganizer.DataAccess;
    using FriendOrganizer.Model;
    public class LookupDataService : IFriendLookupDataService
    {
        private Func<FriendOrganizerDbContext> contextCreator;

        public LookupDataService(Func<FriendOrganizerDbContext> contextCreator)
        {
            this.contextCreator = contextCreator;
        }

        public async Task<IEnumerable<LookupItem>> GetFriendLookupAsync()
        {
            using (var ctx = contextCreator())
            {
                return await ctx.Friends
                    .AsNoTracking()
                    .Select(f => new LookupItem
                    {
                        Id = f.Id,
                        DisplayMember = f.FirstName + " " + f.LastName,
                    })
                    .ToListAsync();
            }
        }
    }
}
