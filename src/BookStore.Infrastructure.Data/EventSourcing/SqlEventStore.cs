using System;
using NetDevPack.Identity.User;
using NetDevPack.Messaging;
using Newtonsoft.Json;
using BookStore.Domain.Core.Events;
using BookStore.Infrastructure.Data.Repository.EventSourcing;

namespace BookStore.Infrastructure.Data.EventSourcing
{
    public class SqlEventStore : IEventStore
    {
        private readonly IEventStoreSqlRepository _eventStoreSqlRepository;
        private readonly IAspNetUser _user;

        public SqlEventStore(IEventStoreSqlRepository eventStoreSqlRepository, IAspNetUser user)
        {
            _eventStoreSqlRepository = eventStoreSqlRepository ?? throw new ArgumentNullException(nameof(eventStoreSqlRepository));
            _user = user;
        }

        public void Save<T> (T theEvent) where T : Event 
        {
            var serializedData = JsonConvert.SerializeObject(theEvent);

            var storedEvent = new StoredEvent(
                theEvent,
                serializedData,
                _user.Name ?? _user.GetUserEmail());

            _eventStoreSqlRepository.Store(storedEvent);
        }
    }
}