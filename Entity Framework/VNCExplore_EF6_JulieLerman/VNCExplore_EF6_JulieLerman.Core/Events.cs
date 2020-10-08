using Prism.Events;

namespace VNCExplore_EF6_JulieLerman.Core
{
    // Removed in Prism6
    //public class PersonUpdatedEvent : CompositePresentationEvent<string> { }
    public class PersonUpdatedEvent : PubSubEvent<string> { }
}
