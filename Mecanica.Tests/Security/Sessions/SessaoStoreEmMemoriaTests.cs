using Mecanica.Blazor.Security.Sessions;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mecanica.Tests.Security.Sessions
{
    public class SessaoStoreEmMemoriaTests
    {
        [Fact]
        public async Task SalvarAsync_DevePermitirRecuperarToken()
        {
            // Arrange: preparar o ambiente de teste
            using var cache = new MemoryCache(new MemoryCacheOptions());
            var store = new SessaoStoreEmMemoria(cache);

            var sessaoId = "sessao123";
            var token = "token123";
            var expiracao = DateTimeOffset.UtcNow.AddMinutes(30);

            // Act: salvar o token e depois tentar recuperá-lo
            await store.SalvarAsync(sessaoId, token, expiracao);
            var tokenRecuperado = await store.ObterTokenAsync(sessaoId);

            // Assert: verificar se o token recuperado é igual ao token salvo
            Assert.Equal(token, tokenRecuperado);
        }

        [Fact]
        public async Task ObterTokenAsync_SessaoInexistente_DeveRetornarNull()
        {
            // Arrange
            using var cache = new MemoryCache(new MemoryCacheOptions());
            var store = new SessaoStoreEmMemoria(cache);

            // Act
            var tokenRecuperado = await store.ObterTokenAsync("sessaoInexistente");

            // Assert
            Assert.Null(tokenRecuperado);
        }


        [Fact]

        public async Task RemoverAsync_DeveRemoverSessaoExistente()
        {
            // Arrange
            using var cache = new MemoryCache(new MemoryCacheOptions());
            var store = new SessaoStoreEmMemoria(cache);

            var sessaoId = "sessao123";
            var token = "token123";
            var expiracao = DateTimeOffset.UtcNow.AddMinutes(30);

            await store.SalvarAsync(sessaoId, token, expiracao);

            // Act
            await store.RemoverAsync(sessaoId);

            // Assert
            var tokenRecuperado = await store.ObterTokenAsync(sessaoId);
            Assert.Null(tokenRecuperado);
        }

        [Fact]
        public async Task ObterTokenAsync_SessaoExpirada_DeveRetornarNull()
        {
            // Arrange
            using var cache = new MemoryCache(new MemoryCacheOptions());
            var store = new SessaoStoreEmMemoria(cache);

            var sessaoId = "sessao123";
            var token = "token123";

            var expiracao = DateTimeOffset.UtcNow.AddMilliseconds(100); // Expira em 100ms
            await store.SalvarAsync(sessaoId, token, expiracao);
            Assert.Equal(token, await store.ObterTokenAsync(sessaoId));

            // Act
            await Task.Delay(300); // Espera 200ms para garantir que a sessão expire
            var tokenRecuperado = await store.ObterTokenAsync(sessaoId);

            // Assert
            Assert.Null(tokenRecuperado);
        }
    }
}
