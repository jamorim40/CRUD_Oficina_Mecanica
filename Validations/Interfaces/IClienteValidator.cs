using Mecanica.Models.Dtos;

namespace Mecanica.Validations.Interfaces
{
    public interface IClienteValidator
    {
        List<string> validador(ClienteCreateDto dto);
    }
}
