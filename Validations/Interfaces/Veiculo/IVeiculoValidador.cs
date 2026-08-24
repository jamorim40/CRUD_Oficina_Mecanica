using Mecanica.Models.Dtos.Requests.Veiculo;

namespace Mecanica.Validations.Interfaces.Veiculo
{
    public interface IVeiculoValidador
    {
        List<string> validador(CriarVeiculoDto dto);
    }
}
