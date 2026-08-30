using Mecanica.Models.Enums;

namespace Mecanica.Models.Dtos.Responses.OrdemServico
{
    public class RespostaCriarOrdemServicoDto
    {
        public string Placa { get; set; } = string.Empty;
        public int? Romaneio { get; set; }
        public string Modelo { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string? Observacao { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
