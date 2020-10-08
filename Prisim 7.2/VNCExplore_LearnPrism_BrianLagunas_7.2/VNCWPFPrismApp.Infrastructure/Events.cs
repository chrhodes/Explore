using Prism.Events;

namespace VNCWPFPrismApp.Infrastructure
{
    // Removed in Prism6
    //public class PersonUpdatedEvent : CompositePresentationEvent<string> { }
    public class PersonUpdatedEvent : PubSubEvent<string> { }
}
