using Mecanica.Models.Enums;

namespace Mecanica.Models.Dtos.Requests.OrdemServico
{
    public class AtualizarOrdemServicoDtoRequest
    {
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string Observacao { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
