using System;
using NetDevPack.Messaging;

namespace BookStore.Domain.Commands
{
    public class BookCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public DateTime PublishDate { get; protected set; }
        public string Authors { get; protected set; }
    }
}