using System.Collections.Generic;
using FriendOrganizer.Model04;

namespace FriendOrganizer.UI04.Data
{
  public interface IFriendDataService
  {
    IEnumerable<Friend> GetAll();
  }
}