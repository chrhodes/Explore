using Prism.Events;

namespace VNCExplore_LearnPrism_BrianLagunas.Infrastructure
{
    // Removed in Prism6
    //public class PersonUpdatedEvent : CompositePresentationEvent<string> { }
    public class PersonUpdatedEvent : PubSubEvent<string> { }
}
