using VNC.Core.DomainServices;

namespace FriendOrganizer.Domain
{
    public class LookupItem : ILookupItem<int>
    {
        public int Id { get; set; }

        public string DisplayMember { get; set; }
    }
}
