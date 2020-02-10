namespace FriendOrganizer.UI.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using FriendOrganizer.DataAccess;
    using FriendOrganizer.Model;

    public class FriendDataService : IFriendDataService
    {
        private Func<FriendOrganizerDbContext> contextCreator;

        public FriendDataService(Func<FriendOrganizerDbContext> contextCreator)
        {
            this.contextCreator = contextCreator;
        }

        public async Task<List<Friend>> GetAllAsync()
        {
            using (var ctx = new FriendOrganizerDbContext())
            {
                var friends = await ctx.Friends.AsNoTracking().ToListAsync();
                await Task.Delay(5000);
                return friends;
            }
        }
    }
}
