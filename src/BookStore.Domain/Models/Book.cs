using System;
using NetDevPack.Domain;

namespace BookStore.Domain.Models
{
    public class Book: Entity, IAggregateRoot
    {

        public Book(Guid id, string title, string description, DateTime publishDate, string authors )
        {
            Id = id;
            Title = title;
            Description = description;
            PublishDate = publishDate;
            Authors = authors;
        }
        protected Book() {}

        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime PublishDate { get; private set; }
        public string Authors { get; private set; }
    }
}
