using Microsoft.Extensions.Caching.Memory;

namespace Mecanica.Blazor.Security.Sessions
{
    public class ArmazenamentoSessaoSecurity : IArmazenamentoSessaoSecurity
    {
        private readonly IMemoryCache _cache;
        public ArmazenamentoSessaoSecurity(IMemoryCache cache)
        {
            _cache = cache;
        }

        public Task SalvarAsync(string sessaoId, string token, DateTimeOffset expiracao)
        {
            _cache.Set(sessaoId, token, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = expiracao
            });
            return Task.CompletedTask;
        }

        public Task<string?> ObterTokenAsync(string sessaoId)
        {
            _cache.TryGetValue(sessaoId, out string? token);
            return Task.FromResult(token);
        }

        public Task RemoverAsync(string sessaoId)
        {
            _cache.Remove(sessaoId);
            return Task.CompletedTask;
        }
    }
}
