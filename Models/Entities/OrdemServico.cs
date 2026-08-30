 using Mecanica.Models.Entities.Bases;
using Mecanica.Models.Enums;

namespace Mecanica.Models.Entities
{
    public class OrdemServico : EntityBase
    {
        public int Id { get; set; }
        public int Romaneio { get; set; }
        public string Descricao {  get; set; } = string.Empty;
        public DateTime? DataInicio {  get; set; }
        public DateTime? DataFim {  get; set; }
        public int VeiculoId { get; set; }
        public Veiculo? Veiculo { get; set; }
        public string? Observacao { get; set; }
        public EnumStatusOrdemServico Status { get; set;}


    }
}
