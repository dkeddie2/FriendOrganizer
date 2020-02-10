namespace FriendOrganizer.UI.Data
{
    using System.Collections.Generic;
    using FriendOrganizer.Model;
    public class FriendDataService : IFriendDataService
    {
        public IEnumerable<Friend> GetAll()
        {
            // TODO: Load data from a real database.
            yield return new Friend { FirstName = "Thomas", LastName = "Huber" };
            yield return new Friend { FirstName = "Andreas", LastName = "Boehler" };
            yield return new Friend { FirstName = "Julia", LastName = "Huber" };
            yield return new Friend { FirstName = "Chrissi", LastName = "Egin" };
        }
    }
}
