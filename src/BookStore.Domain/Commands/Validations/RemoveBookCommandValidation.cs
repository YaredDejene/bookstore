namespace BookStore.Domain.Commands.Validations
{
    public class RemoveBookCommandValidation : BookValidation<RemoveBookCommand>
    {
        public RemoveBookCommandValidation()
        {   
            ValidateId();
        }
    }
}