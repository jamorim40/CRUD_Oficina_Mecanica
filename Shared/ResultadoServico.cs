namespace Mecanica.Shared
{
    public class ResultadoServico<T>
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public T? Conteudo {  get; set; }
    } 
}
