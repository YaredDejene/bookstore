using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Application.History;
using BookStore.Application.Interfaces;
using BookStore.Application.Models;
using BookStore.Domain.Commands;
using BookStore.Domain.Interfaces;
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

        public async Task<IList<BookHistoryData>> GetAllHistory(Guid id)
        {
            return BookHistory.ToBookHistory(await _eventStoreRepository.All(id));
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}