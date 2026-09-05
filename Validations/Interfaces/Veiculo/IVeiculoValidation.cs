using Mecanica.Models.Dtos.Requests.Veiculo;

namespace Mecanica.Validations.Interfaces.Veiculo
{
    public interface IVeiculoValidation
    {
        List<string> validador(CriarVeiculoDtoRequest dto);
    }
}
