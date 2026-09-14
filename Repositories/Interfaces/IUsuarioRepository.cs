using Mecanica.Models.Entities;

namespace Mecanica.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObterPorLoginAsync(string login);
        Task<Usuario?> ObterPorMatriculaAsync(int matricula);
        Task CriarAsync(Usuario usuario);
        Task AtualizarAsync(Usuario usuario);
        Task ResetarUsuarioAsync(Usuario usuario);
    }
}
