using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace BookStore.Domain.Events
{
    public class BookEventHandler :
        INotificationHandler<BookRegisteredEvent>,
        INotificationHandler<BookUpdatedEvent>,
        INotificationHandler<BookRemovedEvent>
    {
        
        public Task Handle(BookRegisteredEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(BookRemovedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(BookUpdatedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }


}