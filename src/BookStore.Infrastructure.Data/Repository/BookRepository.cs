using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            return await _dbSet.ToListAsync();
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
    }
}