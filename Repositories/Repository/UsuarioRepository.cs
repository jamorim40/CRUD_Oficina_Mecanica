using Mecanica.Datas;
using Mecanica.Models.Entities;
using Mecanica.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mecanica.Repositories.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _appDbContext;

        public UsuarioRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Usuario?> ObterPorLoginAsync(string login)
        {
            return await _appDbContext.Usuarios
                .Include(u => u.Funcionario)
                .ThenInclude(f => f.Cargo)
                .FirstOrDefaultAsync(u => u.Login == login);
        }
        public async Task<Usuario?> ObterPorMatriculaAsync(int matricula)
        {
            return await _appDbContext.Usuarios
                .Include(u => u.Funcionario)
                .ThenInclude(f => f.Cargo)
                .FirstOrDefaultAsync(u => u.Funcionario!.Matricula == matricula);
        }
        public async Task CriarAsync(Usuario usuario)
        {
            await _appDbContext.Usuarios.AddAsync(usuario);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Usuario usuario)
        {
            _appDbContext.Usuarios.Update(usuario);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task ResetarUsuarioAsync(Usuario usuario)
        {
            _appDbContext.Usuarios.Update(usuario);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
