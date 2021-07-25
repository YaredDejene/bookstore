using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Domain.Models;
using NetDevPack.Data;

namespace BookStore.Domain.Interfaces
{
    public interface IBookRepository: IRepository<Book>
    {
        Task<Book> GetById(Guid id);
        Task<Book> GetByTitle(string title);
        Task<IEnumerable<Book>> GetAll();

        void Add(Book book);
        void Update(Book book);
        void Remove(Book book);
    }
}