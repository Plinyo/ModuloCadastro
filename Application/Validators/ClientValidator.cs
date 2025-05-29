using Application.Requests;

namespace Application.Validators
{
    public class ClientValidator
    {
        public bool Validate(ClientRequest request, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(request.FullName))
            {
                errorMessage = "Nome completo é obrigatório.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(request.Cpf))
            {
                errorMessage = "CPF é obrigatório.";
                return false;
            }

            if (request.DateOfBirth == default)
            {
                errorMessage = "Data de nascimento inválida.";
                return false;
            }

            if (request.Address == null)
            {
                errorMessage = "Endereço é obrigatório.";
                return false;
            }

            if (request.Phones == null || !request.Phones.Any())
            {
                errorMessage = "Pelo menos um telefone é obrigatório.";
                return false;
            }

            if (request.Emails == null || !request.Emails.Any())
            {
                errorMessage = "Pelo menos um email é obrigatório.";
                return false;
            }

            return true;
        }
    }
}