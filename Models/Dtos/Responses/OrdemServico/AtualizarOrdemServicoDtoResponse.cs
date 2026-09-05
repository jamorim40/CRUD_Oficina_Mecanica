using Mecanica.Models.Enums;

namespace Mecanica.Models.Dtos.Responses.OrdemServico
{
    public class AtualizarOrdemServicoDtoResponse
    {
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string Observacao { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
