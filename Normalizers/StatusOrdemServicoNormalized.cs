using Mecanica.Models.Enums;

namespace Mecanica.Normalizers
{
    public class StatusOrdemServicoNormalized
    {
        public static StatusOrdemServicoEnums ObterStatus(string status)
        {
            status = StatusNormalizado.Normalizar(status);
            return status switch
            {
                "ABERTA" => StatusOrdemServicoEnums.Aberto,
                "EM ANDAMENTO" => StatusOrdemServicoEnums.EmAndamento,
                "AGUARDANDO PEÇA" => StatusOrdemServicoEnums.AguardandoPeca,
                "FINALIZADA" => StatusOrdemServicoEnums.Finalizado,
                "CANCELADA" => StatusOrdemServicoEnums.Cancelado,
                _ => throw new ArgumentException("Status inválido.")
            };
        }
    }
}
