using System;
using BookStore.Domain.Commands.Validations;

namespace BookStore.Domain.Commands
{
    public class UpdateBookCommand: BookCommand
    {
        public UpdateBookCommand(Guid id, string title, string description, DateTime publishDate, string authors)
        {
            Id = id;
            Title = title;
            Description = description;
            PublishDate = publishDate;
            Authors = authors;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateBookCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}