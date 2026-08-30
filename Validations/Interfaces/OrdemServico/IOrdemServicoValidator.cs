using Mecanica.Models.Dtos.Requests.OrdemServico;

namespace Mecanica.Validations.Interfaces.OrdemServico
{
    public interface IOrdemServicoValidator
    {
        List<string> validador(RequisicaoCriarOrdemServicoDto dto);
    }
}
