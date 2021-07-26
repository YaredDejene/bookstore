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
            return await GetQuery(s => s.AggregateId == aggregateId, null).ToListAsync();
        }

        public async Task<IEnumerable<StoredEvent>> All(int start, int pageSize, Expression<Func<StoredEvent, bool>> filter = null, 
                                                    Func<IQueryable<StoredEvent>, IOrderedQueryable<StoredEvent>> orderBy = null)
        {
            return await GetQuery(filter, orderBy).Skip(start).Take(pageSize).ToListAsync();
        }

        public async Task<int> Count(Expression<Func<StoredEvent, bool>> filter = null)
        {
            return await GetQuery(filter, null).CountAsync();
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

        private IQueryable<StoredEvent> GetQuery(Expression<Func<StoredEvent, bool>> filter, Func<IQueryable<StoredEvent>, IOrderedQueryable<StoredEvent>> orderBy)
        {
            var query = _context.StoredEvents.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }
    }
}