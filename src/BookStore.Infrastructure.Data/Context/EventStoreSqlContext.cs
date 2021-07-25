using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using NetDevPack.Domain;
using NetDevPack.Mediator;
using NetDevPack.Messaging;
using BookStore.Domain.Models;
using BookStore.Domain.Core.Events;

namespace BookStore.Infrastructure.Data.Context
{
    public class EventStoreSqlContext : DbContext
    {

        public EventStoreSqlContext(DbContextOptions<EventStoreSqlContext> options) : base(options) 
        {
                                  
        }

        public DbSet<StoredEvent> StoredEvents { get; set; }

    }
}