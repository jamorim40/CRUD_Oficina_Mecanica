using Mecanica.Models.Dtos.Requests.Cliente;
using Mecanica.Models.Dtos.Requests.Funcionario;
using Mecanica.Models.Dtos.Responses.Cliente;
using Mecanica.Models.Dtos.Responses.Funcionario;
using Mecanica.Models.Entities;

namespace Mecanica.Services.Interfaces
{
    public interface IFuncionarioService
    {
        Task<List<FuncionarioDtoResponse>> ObterTodos();
        Task<FuncionarioDtoResponse> ObterPorMatricula(int matricula);
        Task<Funcionario> CriarAsync(CriarFuncionarioDtoRequest dto);
        Task<Funcionario> AtualizarAsync(int matricula, AtualizarFuncionarioDtoRequest dto);
        Task SoftDeleteAsync(int matricula);
    }
}
