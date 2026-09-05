namespace Mecanica.Repositories.Interfaces
{
    public interface IFuncionarioRepository
    {
        Task<bool> ExisteFuncionarioPorCargo(int cargoId);
    }
}
