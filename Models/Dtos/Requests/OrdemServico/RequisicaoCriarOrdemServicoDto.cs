namespace Mecanica.Models.Dtos.Requests.OrdemServico
{
    public class RequisicaoCriarOrdemServicoDto
    {
        public string Placa { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string? Observacao {  get; set; } = string.Empty;


    }
}
