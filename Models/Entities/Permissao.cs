using Mecanica.Models.Entities.Bases;

namespace Mecanica.Models.Entities
{
    public class Permissao : EntityBase
    {
        public int Id { get; set; }
        public string Codigo{ get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
    
        public ICollection<GrupoPermissaoItem> GruposPermissao { get; set; } = new List<GrupoPermissaoItem>();
    }
}
