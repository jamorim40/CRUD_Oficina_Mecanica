using System.Text.Json.Serialization;

namespace Mecanica.Blazor.Models.Auth
{
    public class ErroApiResponse
    {
        [JsonPropertyName("message")]
        public string Mensagem { get; set; } = string.Empty;
    }
}
