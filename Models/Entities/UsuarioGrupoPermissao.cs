using System.Security.Cryptography.Pkcs;

namespace Mecanica.Models.Entities
{
    public class UsuarioGrupoPermissao
    {
        public int UsuarioId { get; set; }
        public int GrupoPermissaoId { get; set; }

        public Usuario Usuario { get; set; } = null!;
        public GrupoPermissao GrupoPermissao { get; set; } = null!;
    }
}
