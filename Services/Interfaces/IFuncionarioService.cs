using Mecanica.Models.Dtos.Requests.Cliente;
using Mecanica.Models.Dtos.Requests.Funcionario;
using Mecanica.Models.Dtos.Responses.Cliente;
using Mecanica.Models.Dtos.Responses.Funcionario;
using Mecanica.Models.Entities;
using Mecanica.Shared;

namespace Mecanica.Services.Interfaces
{
    public interface IFuncionarioService
    {
        Task<List<FuncionarioDtoResponse>> ObterTodos();
        Task<ResultadoServico<FuncionarioDtoResponse>> ObterPorMatricula(int matricula);
        Task<ResultadoServico<FuncionarioDtoResponse>> CriarAsync(CriarFuncionarioDtoRequest dto);
        Task<ResultadoServico<FuncionarioDtoResponse>> AtualizarAsync(int matricula, AtualizarFuncionarioDtoRequest dto);
        Task<ResultadoServico<string>> SoftDeleteAsync(int matricula);
    }
}
