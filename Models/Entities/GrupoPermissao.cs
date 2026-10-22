using Mecanica.Models.Entities.Bases;

namespace Mecanica.Models.Entities
{
    public class GrupoPermissao : EntityBase
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;

        public ICollection<GrupoPermissaoItem> Permissoes { get; set; } = new List<GrupoPermissaoItem>();
        public ICollection<UsuarioGrupoPermissao> Usuarios { get; set; } = new List<UsuarioGrupoPermissao>();
    }
}
