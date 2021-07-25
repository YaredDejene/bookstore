using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Domain.Core.Events;

namespace BookStore.Infrastructure.Data.Repository.EventSourcing
{
    public interface IEventStoreSqlRepository : IDisposable
    {
        void Store(StoredEvent theEvent);
        Task<IList<StoredEvent>> All(Guid aggregateId);
    }
}