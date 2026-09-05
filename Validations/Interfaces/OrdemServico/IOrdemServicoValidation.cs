using Mecanica.Models.Dtos.Requests.OrdemServico;

namespace Mecanica.Validations.Interfaces.OrdemServico
{
    public interface IOrdemServicoValidation
    {
        List<string> validador(CriarOrdemServicoDtoRequest dto);
    }
}
