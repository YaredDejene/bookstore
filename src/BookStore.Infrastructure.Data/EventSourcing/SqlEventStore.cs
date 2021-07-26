using System;
using NetDevPack.Messaging;
using Newtonsoft.Json;
using BookStore.Domain.Core.Events;
using BookStore.Infrastructure.Data.Repository.EventSourcing;

namespace BookStore.Infrastructure.Data.EventSourcing
{
    public class SqlEventStore : IEventStore
    {
        private readonly IEventStoreSqlRepository _eventStoreSqlRepository;

        public SqlEventStore(IEventStoreSqlRepository eventStoreSqlRepository)
        {
            _eventStoreSqlRepository = eventStoreSqlRepository ?? throw new ArgumentNullException(nameof(eventStoreSqlRepository));
        }

        public void Save<T> (T theEvent) where T : Event 
        {
            var serializedData = JsonConvert.SerializeObject(theEvent);

            var storedEvent = new StoredEvent(
                theEvent,
                serializedData,
                Guid.NewGuid().ToString()); // Random Id for user

            _eventStoreSqlRepository.Store(storedEvent);
        }
    }
}