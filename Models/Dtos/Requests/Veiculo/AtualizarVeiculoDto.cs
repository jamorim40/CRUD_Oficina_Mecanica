namespace Mecanica.Models.Dtos.Requests.Veiculo
{
    public class AtualizarVeiculoDto
    {
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Placa { get; set; } = string.Empty;
    }
}
