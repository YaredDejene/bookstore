using NetDevPack.Messaging; 

namespace BookStore.Domain.Core.Events
{
    public interface IEventStore
    {
        void Save<T>(T theEvent) where T: Event;
    }
}