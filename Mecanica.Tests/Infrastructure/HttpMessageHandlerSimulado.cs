using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;

namespace Mecanica.Tests.Infrastructure
{
    public class HttpMessageHandlerSimulado : HttpMessageHandler
    {
        private readonly HttpResponseMessage _respostaSimulada;

        public HttpMessageHandlerSimulado(HttpResponseMessage respostaSimulada)
        {
            _respostaSimulada = respostaSimulada;
        }
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_respostaSimulada);
        }
    }
}
