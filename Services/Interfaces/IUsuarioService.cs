using Mecanica.Models.Dtos.Requests.Usuario;
using Mecanica.Models.Dtos.Responses.Usuario;
using Mecanica.Shared;

namespace Mecanica.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<ResultadoServico<UsuarioDtoResponse>> CriarAsync(CriarUsuarioDtoRequest dtoRequestCriar);
        Task<ResultadoServico<UsuarioDtoResponse>> ObterPorMatricula(int matricula);
        Task<ResultadoServico<string>> LoginAsync(LoginUsuarioDtoRequest dtoResquesteLogin);
        Task<ResultadoServico<bool>> AlterarSenhaPrimeiroAcessoAsync(AlterarSenhaPrimeiroAcessoUsuarioDtoRequest dtoRequestAlterarSenha);
        Task<ResultadoServico<bool>> ResetarUsuarioAsync(ResetarUsuarioDtoRequest dtoRequestResetar);
    }
}
