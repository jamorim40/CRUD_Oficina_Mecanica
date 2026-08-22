using Mecanica.Models.Dtos.Requests;

namespace Mecanica.Validations.Interfaces
{
    public interface IClienteValidator
    {
        List<string> validador(CriaClienteDto dto);
    }
}
