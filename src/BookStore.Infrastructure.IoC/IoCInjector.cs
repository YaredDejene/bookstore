using BookStore.Application.Interfaces;
using BookStore.Application.Services;
using BookStore.Domain.Commands;
using BookStore.Domain.Core.Events;
using BookStore.Domain.Events;
using BookStore.Domain.Interfaces;
using BookStore.Infrastructure.Bus;
using BookStore.Infrastructure.Data.Context;
using BookStore.Infrastructure.Data.EventSourcing;
using BookStore.Infrastructure.Data.Repository;
using BookStore.Infrastructure.Data.Repository.EventSourcing;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Mediator;

namespace BookStore.Infrastructure.IoC
{
    public static class IoCInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Application
            services.AddScoped<IBookService, BookService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<BookRegisteredEvent>, BookEventHandler>();
            services.AddScoped<INotificationHandler<BookUpdatedEvent>, BookEventHandler>();
            services.AddScoped<INotificationHandler<BookRemovedEvent>, BookEventHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<RegisterNewBookCommand, ValidationResult>, BookCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateBookCommand, ValidationResult>, BookCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveBookCommand, ValidationResult>, BookCommandHandler>();

            // Infra - Data
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<BookStoreContext>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreSqlRepository, EventStoreSqlRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSqlContext>();
        }
    }
}
