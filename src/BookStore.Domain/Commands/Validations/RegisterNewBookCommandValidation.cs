namespace BookStore.Domain.Commands.Validations
{
    public class RegisterNewBookCommandValidation : BookValidation<RegisterNewBookCommand>
    {
        public RegisterNewBookCommandValidation()
        {   
            ValidateTitle();
            ValidateDescription();
            ValidateAuthors();
        }
    }
}