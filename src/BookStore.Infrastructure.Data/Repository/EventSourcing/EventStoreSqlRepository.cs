using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
        public async Task<IEnumerable<StoredEvent>> All(Guid aggregateId)
        {
            return await _context.StoredEvents.Where(s => s.AggregateId == aggregateId).ToListAsync();
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