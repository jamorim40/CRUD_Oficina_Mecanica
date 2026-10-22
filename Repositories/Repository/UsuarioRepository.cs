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

        public async Task<List<string>> ObterPermissoesAsync(int usuarioId)
        {
            return await _appDbContext.UsuarioGrupoPermissoes
                .Where(ugp => ugp.UsuarioId == usuarioId)
                .SelectMany(ugp => ugp.GrupoPermissao.Permissoes)
                .Select(ugp => ugp.Permissao.Codigo)
                .Distinct()
                .ToListAsync();
        }

        public async Task<bool> UsuarioTemPermissaoAsync(int usuarioId, string permissao)
        {
            return await _appDbContext.UsuarioGrupoPermissoes
                .Where(ugp => ugp.UsuarioId == usuarioId)
                .SelectMany(ugp => ugp.GrupoPermissao.Permissoes)
                .AnyAsync(p => p.Permissao.Codigo == permissao);
        }

        public async Task SoftDelete(int matricula)
        {
            var usuario = await _appDbContext.Usuarios.FirstOrDefaultAsync(u => u.Funcionario!.Matricula == matricula);
            if (usuario is null)
            {
                return;
            } 
            usuario.Ativo = false;
            await _appDbContext.SaveChangesAsync();
        }
    }
}
