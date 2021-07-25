using System;
using NetDevPack.Messaging;

namespace BookStore.Domain.Core.Events
{
    public class StoredEvent : Event
    {
        public StoredEvent(Event theEvent, string data, string user)
        {
            Id = Guid.NewGuid();
            AggregateId = theEvent.AggregateId;
            MessageType = theEvent.MessageType;
            Data = data;
            User = user;
        }

        protected StoredEvent() {}

        public Guid Id { get; private set; }
        public string Data { get; set; }
        public string User { get; set; }
    }
}