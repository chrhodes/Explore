using FriendOrganizer.Model04;
using System.Collections.Generic;

namespace FriendOrganizer.UI04.Data
{
  public class FriendDataService : IFriendDataService
  {
    public IEnumerable<Friend> GetAll()
    {
      // TODO: Load data from real database
      yield return new Friend { FirstName = "Thomas", LastName = "Huber" };
      yield return new Friend { FirstName = "Andreas", LastName = "Boehler" };
      yield return new Friend { FirstName = "Julia", LastName = "Huber" };
      yield return new Friend { FirstName = "Chrissi", LastName = "Egin" };
    }
  }
}
