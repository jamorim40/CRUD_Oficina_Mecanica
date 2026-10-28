using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;

namespace Mecanica.Tests.Infrastructure
{
    public class HttpMessageHandlerSimulado : HttpMessageHandler
    {
        private readonly Func<HttpResponseMessage> _criarResposta;

        public HttpMessageHandlerSimulado(Func<HttpResponseMessage> criarResposta)
        {
            _criarResposta = criarResposta;
        }
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = _criarResposta();

            Console.WriteLine($"[HTTP SIMULADO] {request.Method} {request.RequestUri} => {(int)response.StatusCode}");
            return Task.FromResult(response);
        }
    }
}
