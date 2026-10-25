namespace Mecanica.Blazor.Models.Auth;

public class LoginResponse
{
    public bool Sucesso { get; set; }

    public string Mensagem { get; set; } = string.Empty;

    public string Token { get; set; } = string.Empty;
}