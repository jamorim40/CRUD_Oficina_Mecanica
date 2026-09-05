using Mecanica.Models.Dtos.Requests.Cliente;

namespace Mecanica.Validations.Interfaces.Cliente
{
    public interface IClienteValidation
    {
        List<string> validador(CriaClienteDtoRequest dto);
        
    }
}
