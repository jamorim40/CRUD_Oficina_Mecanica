namespace Mecanica.Models.Dtos.Responses.Cliente
{
    public class ClienteDtoResponse
    {
        public string Nome { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set;  } = string.Empty;
        public string? CpfCnpj { get; set; } = string.Empty;
    }
}
