using System.Collections.Generic;
using FriendOrganizer.Domain;

namespace VNCExplore_FriendOrganizer.Core.DomainServices
{
  public interface IFriendDataService04
  {
    IEnumerable<IFriend> GetAll();
  }
}