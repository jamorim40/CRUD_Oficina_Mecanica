using Mecanica.Blazor.Security.Sessions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http.Json;
using Mecanica.Blazor.Models.Auth;
using System.Net;

namespace Mecanica.Tests.Infrastructure
{
    public class FabricaDeTesteMecanicaBlazor : WebApplicationFactory<Mecanica.Blazor.Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll<ISessaoStore>();
                SessaoStoreMock
                    .Setup(store => store.SalvarAsync(
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<DateTimeOffset>()))
                    .ThrowsAsync(
                        new InvalidOperationException(
                            "Falha simulada no armazenamento."));

                var resposatAPI = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = JsonContent.Create(new LoginResponse { Sucesso = true, Mensagem = "Login realizado com sucesso.", Token = FabricaTesteJwt.GerarTokenValido() })
                };

                services.AddHttpClient("MecanicaApi")
                    .ConfigurePrimaryHttpMessageHandler(() =>
                        new HttpMessageHandlerSimulado(resposatAPI));
                services.AddSingleton(SessaoStoreMock.Object);
            });
        }
        public Mock<ISessaoStore> SessaoStoreMock { get; } = new();
    }
}
