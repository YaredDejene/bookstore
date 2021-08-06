using System;
using NetDevPack.Messaging;

namespace BookStore.Domain.Queries
{
    public class GetBookByIdQuery : Command
    {
        public Guid Id { get; set; }
    }
}