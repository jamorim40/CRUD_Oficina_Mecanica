namespace Mecanica.Models.Dtos.Requests.Usuario
{
    public class AlterarSenhaPrimeiroAcessoUsuarioDtoRequest
    {
        public string Login { get; set; } = null!;
        public string SenhaAtual { get; set; } = null!;
        public string NovaSenha { get; set; } = null!;
    }
}
