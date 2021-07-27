using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using BookStore.Domain.Events;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;

namespace BookStore.Domain.Commands
{
    public class BookCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewBookCommand, ValidationResult>,
        IRequestHandler<UpdateBookCommand, ValidationResult>,
        IRequestHandler<RemoveBookCommand, ValidationResult>
    {
        private readonly IBookRepository _bookRepository;
        public BookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public async Task<ValidationResult> Handle(RegisterNewBookCommand  message, CancellationToken cancellationToken)
        {
            if(!message.IsValid()) return message.ValidationResult;

            var book = new Book(Guid.NewGuid(), message.Title, message.Description, message.PublishDate, message.Authors);

            book.AddDomainEvent(new BookRegisteredEvent(book.Id, book.Title, book.Description, book.PublishDate, book.Authors));

            _bookRepository.Add(book);

            await Commit(_bookRepository.UnitOfWork);

            BookValidationResult result = new BookValidationResult();
            result.Data = book;

            return result;
        }

        public async Task<ValidationResult> Handle(UpdateBookCommand  message, CancellationToken cancellationToken)
        {
            if(!message.IsValid()) return message.ValidationResult;

            var book = new Book(message.Id, message.Title, message.Description, message.PublishDate, message.Authors);

            book.AddDomainEvent(new BookUpdatedEvent(book.Id, book.Title, book.Description, book.PublishDate, book.Authors));

            _bookRepository.Update(book);

            await Commit(_bookRepository.UnitOfWork);

            BookValidationResult result = new BookValidationResult();
            result.Data = book;

            return result;
        }

        public async Task<ValidationResult> Handle(RemoveBookCommand  message, CancellationToken cancellationToken)
        {
            if(!message.IsValid()) return message.ValidationResult;

            var book = await _bookRepository.GetById(message.Id);

            if(book == null){
                AddError("The provided book doesn't exist.");
                return ValidationResult;
            }

            book.AddDomainEvent(new BookRemovedEvent(book.Id));

            _bookRepository.Remove(book);

            await Commit(_bookRepository.UnitOfWork);

            BookValidationResult result = new BookValidationResult();
            result.Data = book;

            return result;
        }
    }
}