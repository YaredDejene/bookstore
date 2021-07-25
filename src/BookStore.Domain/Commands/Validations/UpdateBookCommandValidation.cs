namespace BookStore.Domain.Commands.Validations
{
    public class UpdateBookCommandValidation : BookValidation<UpdateBookCommand>
    {
        public UpdateBookCommandValidation()
        {   
            ValidateId();
            ValidateTitle();
            ValidateDescription();
            ValidateAuthors();
        }
    }
}