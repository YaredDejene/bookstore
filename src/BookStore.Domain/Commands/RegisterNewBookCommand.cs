using System;
using BookStore.Domain.Commands.Validations;

namespace BookStore.Domain.Commands
{
    public class RegisterNewBookCommand: BookCommand
    {
        public RegisterNewBookCommand(string title, string description, DateTime publishDate, string authors)
        {
            Title = title;
            Description = description;
            PublishDate = publishDate;
            Authors = authors;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewBookCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}