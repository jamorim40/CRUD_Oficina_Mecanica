namespace Mecanica.Models.Dtos.Requests.Funcionario
{
    public class AtualizarFuncionarioDtoRequest
    {
        public string Nome { get; set; } =string.Empty;
        public string CpfCnpj { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int CargoId { get; set; } 
    }
}
