namespace Mecanica.Blazor.Security.Sessions
{
    public interface IArmazenamentoSessaoSecurity
    {
        Task SalvarAsync(string sessaoId, string token, DateTimeOffset expiracao);
        Task<string?> ObterTokenAsync(string sessaoId);
        Task RemoverAsync(string sessaoId);
    }
}
