namespace FriendOrganizer.UI.Data
{
    using System;
    using System.Data.Entity;
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

        public async Task<Friend> GetByIdAsync(int friendId)
        {
            using (var ctx = contextCreator())
            {
                var friends = await ctx.Friends.AsNoTracking().SingleAsync(f => f.Id == friendId);
                return friends;
            }
        }
    }
}