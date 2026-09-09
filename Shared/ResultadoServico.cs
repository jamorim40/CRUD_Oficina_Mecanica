using System.Security.Cryptography.Pkcs;

namespace Mecanica.Shared
{
    public class ResultadoServico<T>
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public T? Conteudo {  get; set; }
        public int? StatusCode { get; set; }

        public static ResultadoServico<T> Ok(T? conteudo, string mensagem, int? statusCode = 200)
        {
            return new ResultadoServico<T>
            {
                Sucesso = true,
                Mensagem = mensagem,
                Conteudo = conteudo,
                StatusCode = statusCode
            };

        }

        public static ResultadoServico<T> Falha(string mensagem, int? statusCode = 400)
        {
            return new ResultadoServico<T>
            {
                Sucesso = false,
                Mensagem = mensagem,
                StatusCode = statusCode
            };
        }
    } 
}
