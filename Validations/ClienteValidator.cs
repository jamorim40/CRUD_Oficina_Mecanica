using Mecanica.Models.Dtos;
using Mecanica.Validations.Interfaces;

namespace Mecanica.Validations
{
    public class ClienteValidator : IClienteValidator
    {
        public List<string> validador(ClienteCreateDto dto)
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
