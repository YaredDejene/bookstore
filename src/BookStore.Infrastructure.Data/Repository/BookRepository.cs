using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using BookStore.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;

namespace BookStore.Infrastructure.Data.Repository
{
    public class BookRepository : IBookRepository
    {
        protected readonly BookStoreContext _context;
        protected readonly DbSet<Book> _dbSet;
        public BookRepository(BookStoreContext context)
        {
            _context = context;
            _dbSet = _context.Set<Book>();
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Book> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await GetQuery(null, null).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAll(int start, int pageSize, Expression<Func<Book, bool>> filter = null, 
                                                    Func<IQueryable<Book>, IOrderedQueryable<Book>> orderBy = null)
        {
            var query = GetQuery(filter, orderBy);
            return await query.Skip(start).Take(pageSize).ToListAsync();
        }

        public async Task<int> Count(Expression<Func<Book, bool>> filter = null)
        {
            return await GetQuery(filter, null).CountAsync();
        }

        public async Task<Book> GetByTitle(string title)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Title == title);
        }

        public void Add(Book book)
        {
           _dbSet.Add(book);
        }

        public void Update(Book book)
        {
            _dbSet.Update(book);
        }

        public void Remove(Book book)
        {
            _dbSet.Remove(book);
        }
       
        public void Dispose()
        {
            _context.Dispose();
        }

        private IQueryable<Book> GetQuery(Expression<Func<Book, bool>> filter, Func<IQueryable<Book>, IOrderedQueryable<Book>> orderBy, params Expression<Func<Book, object>>[] includes)
        {
            var query = _dbSet.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }
    }
}