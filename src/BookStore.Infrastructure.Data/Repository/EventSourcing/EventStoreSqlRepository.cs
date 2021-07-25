using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Domain.Core.Events;
using BookStore.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Data.Repository.EventSourcing
{
    public class EventStoreSqlRepository : IEventStoreSqlRepository
    {
        private readonly EventStoreSqlContext _context;

        public EventStoreSqlRepository(EventStoreSqlContext context)
        {
            _context = context;
        }
        public async Task<IList<StoredEvent>> All(Guid aggregateId)
        {
            return await (from e in _context.StoredEvents where e.AggregateId == aggregateId select e).ToListAsync();
        }

        public void Store(StoredEvent theEvent)
        {
            _context.StoredEvents.Add(theEvent);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}