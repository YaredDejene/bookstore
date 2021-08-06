using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Domain.Commands;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;

namespace BookStore.Domain.Queries
{
    public class BookQueryHandler : CommandHandler,
        IRequestHandler<GetAllBooksQuery, ValidationResult>,
        IRequestHandler<GetBookByIdQuery, ValidationResult>
    {
        private readonly IBookRepository _bookRepository;
        public BookQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }
        public async Task<ValidationResult> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            return new BookValidationResult { Data = await _bookRepository.GetAll() };
        }

        public async Task<ValidationResult> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            return new BookValidationResult { Data = await _bookRepository.GetById(request.Id) };
        }
    }
}