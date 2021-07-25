using System;
using NetDevPack.Messaging;


namespace BookStore.Domain.Events
{
    public class BookUpdatedEvent : Event
    {
        public BookUpdatedEvent(Guid id, string title, string description, DateTime publishDate, string authors)
        {
            Id = id;
            Title = title;
            Description = description;
            PublishDate = publishDate;
            Authors = authors;
            AggregateId = id;
        }

        public Guid Id { get; set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime PublishDate { get; private set; }
        public string Authors { get; private set; }
    }
}