namespace Mecanica.Models.Dtos.Requests.Cliente
{
    public class AtualizarClienteDtoRequest
    {
        public string Nome {  get; set; } = string.Empty;
        public string Telefone { get; set;  } = string.Empty;
        public string Email { get; set;  } = string.Empty;
        public string CpfCnpj { get; set;  } = string.Empty;
    }
}
