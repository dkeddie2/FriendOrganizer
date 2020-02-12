namespace FriendOrganizer.UI.Data.Repositories
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using FriendOrganizer.DataAccess;
    using FriendOrganizer.Model;

    public class FriendRepository : IFriendRepository
    {
        private FriendOrganizerDbContext context;

        public FriendRepository(FriendOrganizerDbContext context)
        {
            this.context = context;
        }

        public async Task<Friend> GetByIdAsync(int friendId)
        {
            return await context.Friends.SingleAsync(f => f.Id == friendId);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public bool HasChanges()
        {
            return context.ChangeTracker.HasChanges();
        }
    }
}