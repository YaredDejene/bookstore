using System;
using BookStore.Domain.Commands.Validations;

namespace BookStore.Domain.Commands
{
    public class RemoveBookCommand: BookCommand
    {
        public RemoveBookCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveBookCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}