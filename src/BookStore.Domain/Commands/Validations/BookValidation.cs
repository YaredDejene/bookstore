using System;
using FluentValidation;

namespace BookStore.Domain.Commands.Validations
{
    public abstract class BookValidation<T> : AbstractValidator<T> where T : BookCommand
    {
        protected void ValidateTitle()
        {
            RuleFor(book => book.Title)
                .NotEmpty().WithMessage("Please make sure you have entered the Title")
                .Length(2, 250).WithMessage("The Title must have between 2 and 250 characters");
        }

        protected void ValidateDescription()
        {
            RuleFor(book => book.Title)
                .Length(10, 250).WithMessage("The Title must have between 10 and 1000 characters");
        }

        protected void ValidateId()
        {
            RuleFor(book => book.Id)
                .NotEqual(Guid.Empty);
        }
    }
}