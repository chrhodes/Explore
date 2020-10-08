using System.Collections.Generic;
using FriendOrganizer.Domain;
using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.DomainServices
{
    public class FriendDataServiceMock04 : IFriendDataService04
    {
        public IEnumerable<IFriend> GetAll()
        {
            // TODO: Load data from real database
            yield return new Friend04 { FirstName = "Thomas", LastName = "Huber" };
            yield return new Friend04 { FirstName = "Andreas", LastName = "Boehler" };
            yield return new Friend04 { FirstName = "Julia", LastName = "Huber" };
            yield return new Friend04 { FirstName = "Chrissi", LastName = "Egin" };
        }
    }
}
