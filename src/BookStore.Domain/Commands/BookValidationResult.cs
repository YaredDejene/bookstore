using FluentValidation.Results;

namespace BookStore.Domain.Commands
{
    public class BookValidationResult : ValidationResult
    {
        public object Data { get; set; }
    }
}