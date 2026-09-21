using Mecanica.Models.Entities;

namespace Mecanica.Security.Interfaces
{
    public interface IJwtService
    {
        string GerarToken(Usuario usuario);
    }
}
