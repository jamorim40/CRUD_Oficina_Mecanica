using System.Security.Cryptography.Pkcs;

namespace Mecanica.Shared
{
    public class ResultadoServico<T>
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public T? Conteudo {  get; set; }

        public static ResultadoServico<T> Ok(T? conteudo, string mensagem)
        {
            return new ResultadoServico<T>
            {
                Sucesso = true,
                Mensagem = mensagem,
                Conteudo = conteudo
            };

        }

        public static ResultadoServico<T> Falha(string mensagem)
        {
            return new ResultadoServico<T>
            {
                Sucesso = false,
                Mensagem = mensagem,
            };
        }
    } 
}
