namespace Mecanica.Models.Entities
{
    public class GrupoPermissaoItem
    {
        public int GrupoPermissaoId { get; set; }
        public int PermissaoId { get; set; }

        public GrupoPermissao GrupoPermissao { get; set; } = null!;
        public Permissao Permissao { get; set; } = null!;
    }
}
