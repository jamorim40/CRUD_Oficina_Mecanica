using Mecanica.Models.Dtos.Requests.Cliente;

namespace Mecanica.Validations.Interfaces.Cliente
{
    public interface IClienteValidator
    {
        List<string> validador(CriaClienteDto dto);
    }
}
