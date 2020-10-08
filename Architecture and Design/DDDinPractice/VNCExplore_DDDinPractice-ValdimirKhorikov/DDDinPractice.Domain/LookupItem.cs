using VNC.Core.Domain;

namespace DDDinPractice.Domain
{
    public class LookupItem : ILookupItem<int>
    {
        public int Id { get; set; }

        public string DisplayMember { get; set; }
    }
}
