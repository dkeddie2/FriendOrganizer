namespace FriendOrganizer.UI.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FriendOrganizer.DataAccess;
    using FriendOrganizer.Model;

    public class FriendDataService : IFriendDataService
    {
        private Func<FriendOrganizerDbContext> contextCreator;

        public FriendDataService(Func<FriendOrganizerDbContext> contextCreator)
        {
            this.contextCreator = contextCreator;
        }

        public IEnumerable<Friend> GetAll()
        {
            using (var ctx = new FriendOrganizerDbContext())
            {
                return ctx.Friends.AsNoTracking().ToList();
            }
        }
    }
}
