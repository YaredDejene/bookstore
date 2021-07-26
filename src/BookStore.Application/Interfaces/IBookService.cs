using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Application.History;
using BookStore.Application.Models;
using BookStore.Application.Wrappers;
using FluentValidation.Results;

namespace BookStore.Application.Interfaces
{
    public interface IBookService : IDisposable
    {
        Task<IEnumerable<BookModel>> GetAll();
        Task<DataTableResponse<BookModel>> GetAll(DataTableRequest request);
        Task<BookModel> GetById(Guid id);
        
        Task<ValidationResult> Register(BookModel book);
        Task<ValidationResult> Update(BookModel book);
        Task<ValidationResult> Remove(Guid id);

        Task<IEnumerable<BookHistoryData>> GetAllHistory(Guid id);
        Task<DataTableResponse<BookHistoryData>> GetAllHistory(DataTableRequest request);
    }
}