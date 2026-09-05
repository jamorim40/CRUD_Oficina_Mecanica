using Mecanica.Models.Dtos.Requests.Cliente;

namespace Mecanica.Validations.Interfaces.Cliente
{
    public class ClienteValidation : IClienteValidation
    {
        public List<string> validador(CriaClienteDtoRequest dto)
        {
            List<string> erros = new();
            if (string.IsNullOrWhiteSpace(dto.Nome))
                erros.Add("Nome é obrigatrio. ");
            if (dto.Nome?.Length > 150)
                erros.Add("Nome deve possuir no máximo 150 caracteres. ");
            if (string.IsNullOrWhiteSpace(dto.Email))
                erros.Add("Email é obrigatório.");
            if (!dto.Email.Contains("@"))
                erros.Add("Email invállido");
            return erros;
        }
    }
}
