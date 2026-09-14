namespace Mecanica.Models.Dtos.Requests.Usuario
{
    public class LoginUsuarioDtoRequest
    {
        public string Login { get; set; } = null!;
        public string Senha { get; set; } = null!;
    }
}
