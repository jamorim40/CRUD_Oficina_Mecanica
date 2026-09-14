namespace Mecanica.Models.Dtos.Responses.Usuario
{
    public class UsuarioDtoResponse
    {
        public string NomeFuncionario { get; set; } = string.Empty;
        public string NomeCargo { get; set; } = string.Empty;
        public int Matricula {  get; set; }
        public string? Login {  get; set; }
        public bool Bloqueado { get; set; }

    }
}
