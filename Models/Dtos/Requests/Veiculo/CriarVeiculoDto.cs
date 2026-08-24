namespace Mecanica.Models.Dtos.Requests.Veiculo
{
    public class CriarVeiculoDto
    {
        public int ClienteId { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Placa { get; set; } = string.Empty; 
    }
}
