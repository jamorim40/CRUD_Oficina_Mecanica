using Mecanica.Models.Dtos.Requests.OrdemServico;
using Microsoft.JSInterop.Infrastructure;

namespace Mecanica.Validations.Interfaces.OrdemServico
{
    public class OrdemServicoValidation : IOrdemServicoValidation
    {
        public List<string> validador(CriarOrdemServicoDtoRequest dto)
        {
            List<string> erros = new();
            if (string.IsNullOrWhiteSpace(dto.Placa))
                erros.Add("Placa é obrigatória. ");
            if (dto.Placa.Length > 8)
                erros.Add("Placa não pode ter mais de 8 digitos. ");
            if (string.IsNullOrWhiteSpace(dto.Descricao))
                erros.Add("Modelo é obrigatório.");
            if (dto.Descricao.Length > 500)
                erros.Add("Limite máximo de 500 caracteres. ");
            if (dto.Observacao?.Length > 500)
                erros.Add("Limite máximo de 500 caracteres. ");
            return erros;
        }
    }
}
