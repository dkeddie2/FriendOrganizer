namespace FriendOrganizer.DataAccess
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using FriendOrganizer.Model;

    public class FriendOrganizerDbContext : DbContext
    {
        public FriendOrganizerDbContext()
            : base("FriendOrganizerDb")
        {

        }

        public DbSet<Friend> Friends { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // to not pluralize table names (friend instead of friends)
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
