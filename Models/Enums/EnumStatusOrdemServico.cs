using System.ComponentModel;

namespace Mecanica.Models.Enums
{
    public enum EnumStatusOrdemServico
    {
        [Description("Aberta")]
        Aberto = 0,
        [Description("Em Andamento")]
        EmAndamento = 1,
        [Description("Aguardando Peça")]
        AguardandoPeca = 2,
        [Description("Finalizado")]
        Finalizado = 3,
        [Description("Cancelado")]
        Cancelado = 4
    }
}
