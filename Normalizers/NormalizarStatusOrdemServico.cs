using Mecanica.Models.Enums;

namespace Mecanica.Normalizers
{
    public class NormalizarStatusOrdemServico
    {
        public static EnumStatusOrdemServico ObterStatus(string status)
        {
            status = StatusNormalizado.Normalizar(status);
            return status switch
            {
                "ABERTA" => EnumStatusOrdemServico.Aberto,
                "EM ANDAMENTO" => EnumStatusOrdemServico.EmAndamento,
                "AGUARDANDO PEÇA" => EnumStatusOrdemServico.AguardandoPeca,
                "FINALIZADA" => EnumStatusOrdemServico.Finalizado,
                "CANCELADA" => EnumStatusOrdemServico.Cancelado,
                _ => throw new ArgumentException("Status inválido.")
            };
        }
    }
}
