using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using BookStore.Domain.Models;
using NetDevPack.Data;

namespace BookStore.Domain.Interfaces
{
    public interface IBookRepository: IRepository<Book>
    {
        Task<Book> GetById(Guid id);
        Task<Book> GetByTitle(string title);
        Task<IEnumerable<Book>> GetAll();        
        Task<IEnumerable<Book>> GetAll(int start, int pageSize, Expression<Func<Book, bool>> filter = null, Func<IQueryable<Book>, IOrderedQueryable<Book>> orderBy = null);
        Task<int> Count(Expression<Func<Book, bool>> filter = null);

        void Add(Book book);
        void Update(Book book);
        void Remove(Book book);
    }
}