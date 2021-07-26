using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using BookStore.Domain.Core.Events;

namespace BookStore.Infrastructure.Data.Repository.EventSourcing
{
    public interface IEventStoreSqlRepository : IDisposable
    {
        void Store(StoredEvent theEvent);
        Task<IEnumerable<StoredEvent>> All(Guid aggregateId);

        Task<IEnumerable<StoredEvent>> All(int start, int pageSize, Expression<Func<StoredEvent, bool>> filter = null, 
                                                    Func<IQueryable<StoredEvent>, IOrderedQueryable<StoredEvent>> orderBy = null);

        Task<int> Count(Expression<Func<StoredEvent, bool>> filter = null);

    }
}