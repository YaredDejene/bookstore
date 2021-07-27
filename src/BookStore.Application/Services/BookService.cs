using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Application.History;
using BookStore.Application.Interfaces;
using BookStore.Application.Models;
using BookStore.Application.Wrappers;
using BookStore.Application.Helpers;
using BookStore.Domain.Models;
using BookStore.Domain.Commands;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Core.Events;
using BookStore.Infrastructure.Data.Repository.EventSourcing;
using FluentValidation.Results;
using NetDevPack.Mediator;

namespace BookStore.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;
        private readonly IEventStoreSqlRepository _eventStoreRepository;
        private readonly IMediatorHandler _mediator;

        public BookService(IMapper mapper,
                                  IBookRepository bookRepository,
                                  IMediatorHandler mediator,
                                  IEventStoreSqlRepository eventStoreRepository)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
            _mediator = mediator;
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task<IEnumerable<BookModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<BookModel>>(await _bookRepository.GetAll());
        }

        public async Task<DataTableResponse<BookModel>> GetAll(DataTableRequest request)
        {
            string search = request.Search?.Value?.ToLower();
            Expression<Func<Book, bool>> expression = (m => (m.Title.ToLower().Contains(search) || m.Authors.ToLower().Contains(search)));
            Func<IQueryable<Book>, IOrderedQueryable<Book>> orderBy = string.IsNullOrEmpty(request.Sort) ? null :
                 OrderBy<Book>.GetOrderBy(request.Sort, request.IsDescending);

            var totalRecords = string.IsNullOrEmpty(search) ? await _bookRepository.Count() : await _bookRepository.Count(expression);
            var books = await _bookRepository.GetAll(request.Start, request.Length, expression, orderBy);
            var bookModels = _mapper.Map<IEnumerable<BookModel>>(books);

            var response = new DataTableResponse<BookModel>(request.Draw, totalRecords, totalRecords, bookModels.ToList(), null);
            return response;
        }

        public async Task<BookModel> GetById(Guid id)
        {
            return _mapper.Map<BookModel>(await _bookRepository.GetById(id));
        }

        public async Task<ValidationResult> Register(BookModel bookViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewBookCommand>(bookViewModel);
            return await _mediator.SendCommand(registerCommand);
        }

        public async Task<ValidationResult> Update(BookModel bookViewModel)
        {
            var updateCommand = _mapper.Map<UpdateBookCommand>(bookViewModel);
            return await _mediator.SendCommand(updateCommand);
        }

        public async Task<ValidationResult> Remove(Guid id)
        {
            var removeCommand = new RemoveBookCommand(id);
            return await _mediator.SendCommand(removeCommand);
        }

        public async Task<IEnumerable<BookHistoryData>> GetAllHistory(Guid id)
        {
            return BookHistory.ToBookHistory(await _eventStoreRepository.All(id));
        }

        public async Task<DataTableResponse<BookHistoryData>> GetAllHistory(DataTableRequest request)
        {
            if(request.Data == null ) return null;

            var aggregateId = new System.Guid(request.Data);
            string search = request.Search?.Value?.ToLower();         
        
            var historyData = BookHistory.ToBookHistory(await _eventStoreRepository.All(aggregateId));
            IQueryable<BookHistoryData> filteredData = historyData.AsQueryable();
            
            if(!string.IsNullOrEmpty(search)) {
                Expression<Func<BookHistoryData, bool>> expression = ( m => (m.Action.ToLower().Contains(search) || m.ChangeText.ToLower().Contains(search)));
                filteredData = filteredData.Where(expression);
            }

            if(!string.IsNullOrEmpty(request.Sort)) {
                Func<IQueryable<BookHistoryData>, IOrderedQueryable<BookHistoryData>> orderBy = OrderBy<BookHistoryData>.GetOrderBy(request.Sort, request.IsDescending);
                filteredData = orderBy(filteredData);            
            }
 
            var totalRecords = string.IsNullOrEmpty(search) ? historyData.Count() : filteredData.Count();

            var response = new DataTableResponse<BookHistoryData>(request.Draw, totalRecords, totalRecords, filteredData.Skip(request.Start).Take(request.Length).ToList(), null);
            return response;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}