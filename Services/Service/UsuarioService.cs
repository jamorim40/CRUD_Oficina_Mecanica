using Mecanica.Models.Dtos.Requests.Usuario;
using Mecanica.Models.Dtos.Responses.Funcionario;
using Mecanica.Models.Dtos.Responses.Usuario;
using Mecanica.Models.Entities;
using Mecanica.Normalizers;
using Mecanica.Repositories.Interfaces;
using Mecanica.Services.Interfaces;
using Mecanica.Shared;

namespace Mecanica.Services.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly IFuncionarioRepository _funcionarioRepository;


        public UsuarioService(IUsuarioRepository repository, IFuncionarioRepository funcionarioRepository)
        {
            _repository = repository;
            _funcionarioRepository = funcionarioRepository;
        }
        public async Task<ResultadoServico<UsuarioDtoResponse>> ObterPorMatricula(int matricula)
        {
            var usuario = await _repository.ObterPorMatriculaAsync(matricula);
            if (usuario is null)
                return ResultadoServico<UsuarioDtoResponse>.Falha($"Usuário de matricula: {matricula} não encontrado.", 404);
            if (!usuario.Funcionario.Ativo)
                return ResultadoServico<UsuarioDtoResponse>.Falha("Funcionário está inativo.", 400);
            var dto = new UsuarioDtoResponse
            {
                NomeFuncionario = usuario.Funcionario.Nome,
                NomeCargo = usuario.Funcionario.Cargo.Nome,
                Matricula = usuario.Funcionario.Matricula,
                Login = usuario.Login,
                Bloqueado = usuario.Bloqueado,
            };
            return ResultadoServico<UsuarioDtoResponse>.Ok(dto, "Ok", 200);
        }

        public async Task<ResultadoServico<UsuarioDtoResponse>> CriarAsync(CriarUsuarioDtoRequest dtoRequestCriar)
        {
            var funcionario = await _funcionarioRepository.ObterPorMatricula(dtoRequestCriar.Matricula);
            if (funcionario is null)
                return ResultadoServico<UsuarioDtoResponse>.Falha($"Funcionario de matricula: {dtoRequestCriar.Matricula} não encontrado.", 404);
            if (!funcionario.Ativo)
                return ResultadoServico<UsuarioDtoResponse>.Falha("Funcionário está inativo.", 400);
            if (funcionario.Usuario is not null)
                return ResultadoServico<UsuarioDtoResponse>.Falha("Funcionário já possui usuário. ", 409);
            var login = await GerarLoginAsync(funcionario.Nome);
            var senhaInicial = GerarSenhaInicial(funcionario.CpfCnpj, funcionario.Matricula);
            var senhaHash = Senhas.GerarSenhaHash(senhaInicial);
            var usuario = new Usuario
            {
                FuncionarioId = funcionario.Id,
                Login = login,
                SenhaHash = senhaHash,
                PrimeiroAcesso = true,
                TentativasLogin = 0,
                Bloqueado = false,
                DataPrimeiroAcesso = null,
                DataUltimoAcesso = null
            };
           await _repository.CriarAsync(usuario);
           var usuarioDto = new UsuarioDtoResponse
            {
                NomeFuncionario = funcionario.Nome,
                NomeCargo = funcionario.Cargo.Nome,
                Matricula = funcionario.Matricula,
                Login = login,
                Bloqueado = usuario.Bloqueado
            };
            return ResultadoServico<UsuarioDtoResponse>.Ok(usuarioDto, "Criado", 201); 
        }

        private const int maxTentativasLogin = 5;
        public async Task<ResultadoServico<string>> LoginAsync(LoginUsuarioDtoRequest dtoResquesteLogin)
        {
            
            var usuario = await _repository.ObterPorLoginAsync(dtoResquesteLogin.Login);
            if (usuario is null)
                return ResultadoServico<string>.Falha($"Funcionário de login: {dtoResquesteLogin.Login} não encontrado.", 409);
            if (!usuario.Ativo)
                return ResultadoServico<string>.Falha("Funcionário está inativo.", 409);
            if (usuario.Bloqueado)
                return ResultadoServico<string>.Falha("Funcionário bloqueado.", 409);
            bool senhaValida =  Senhas.ValidarSenha(dtoResquesteLogin.Senha, usuario.SenhaHash!);
            if (!senhaValida)
            {
                usuario.TentativasLogin += 1;

                if (usuario.TentativasLogin >= maxTentativasLogin)
                {
                    usuario.Bloqueado = true;
                   
                } 
                await _repository.AtualizarAsync(usuario);
                return ResultadoServico<string>.Falha("Funcionário bloqueado, contate seu gestor.", 409);
            }
            if (usuario.DataPrimeiroAcesso == null)
            {
                usuario.DataPrimeiroAcesso = DateTime.Now;
            }
            usuario.DataUltimoAcesso = DateTime.Now;
            usuario.TentativasLogin = 0;
            await _repository.AtualizarAsync(usuario);
            if (usuario.PrimeiroAcesso)
            {
                return ResultadoServico<string>.Falha("Primeiro acesso. É necessário alterar a senha.", 409);
            }

            return ResultadoServico<string>.Ok("Login realizado com sucesso. ", "Ok", 200);

        }

        public async Task<ResultadoServico<bool>> AlterarSenhaPrimeiroAcessoAsync(AlterarSenhaPrimeiroAcessoUsuarioDtoRequest dtoRequestAlterarSenha)
        {
            var usuario = await _repository.ObterPorLoginAsync(dtoRequestAlterarSenha.Login);
            if (usuario is null)
                return ResultadoServico<bool>.Falha($"Funcionário de login: {dtoRequestAlterarSenha.Login} não encontrado.", 409);
            if (!usuario!.PrimeiroAcesso)
                return ResultadoServico<bool>.Falha($"A senha inicial foi alterada em: {usuario.DataPrimeiroAcesso}. Caso tenha esqueciso, contate seu gestor.", 409);
            bool senhaValida = Senhas.ValidarSenha(dtoRequestAlterarSenha.SenhaAtual, usuario.SenhaHash!);
            if (!senhaValida)
            {
                usuario.TentativasLogin += 1;

                if (usuario.TentativasLogin >= maxTentativasLogin)
                {
                    usuario.Bloqueado = true;

                }
                await _repository.AtualizarAsync(usuario);
                return ResultadoServico<bool>.Falha("Funcionário bloqueado, contate seu gestor.", 409);
            }
            usuario.PrimeiroAcesso = false;
            usuario.DataPrimeiroAcesso = DateTime.Now;
            usuario.DataUltimoAcesso = DateTime.Now;
            usuario.TentativasLogin = 0;
            usuario.SenhaHash = Senhas.GerarSenhaHash(dtoRequestAlterarSenha.NovaSenha);
            await _repository.AtualizarAsync(usuario);
            return ResultadoServico<bool>.Ok(true, "Senha alterado com sucesso. ", 200);      
        }

        public async Task<ResultadoServico<bool>> ResetarUsuarioAsync(ResetarUsuarioDtoRequest dtoRequestResetar)
        {
            var usuario = await _repository.ObterPorMatriculaAsync(dtoRequestResetar.Matricula);
            if (usuario is null)
                return ResultadoServico<bool>.Falha($"Usuário de matricula {dtoRequestResetar.Matricula} não encontrado.");
            if (!usuario.Ativo || usuario.Bloqueado)
                return ResultadoServico<bool>.Falha("Não é´possivel resetar a senha, contate seu gestor");
            var funcionario = await _funcionarioRepository.ObterPorMatricula(dtoRequestResetar.Matricula);
            if (funcionario is null)
                return ResultadoServico<bool>.Falha($"O funcionário de matricula: {dtoRequestResetar.Matricula} não encontrado ");
            var senhaInicial = GerarSenhaInicial(funcionario.CpfCnpj, funcionario.Matricula);
            var senhaHash = Senhas.GerarSenhaHash(senhaInicial);
            usuario.SenhaHash = senhaHash;
            usuario.PrimeiroAcesso = true;
            usuario.DataPrimeiroAcesso = null;
            usuario.DataUltimoAcesso = null;
            usuario.TentativasLogin = 0;
            usuario.Bloqueado = false;
            await _repository.AtualizarAsync(usuario);
            return ResultadoServico<bool>.Ok(true, "Senha restaurada com sucesso. ", 200);
        }
       //Métodos auxiliares
        private string GerarSenhaInicial(string cpf, int matricula)
        {
            return cpf.Substring(0, 3) + matricula;
        }

        private async Task<string> GerarLoginAsync(string nome)
        {
            var partesNome = SepararNome(nome);

            string primeiroNome = partesNome[0];
            string ultimoNome = partesNome[partesNome.Length - 1];

            for (int i = 1; i <= primeiroNome.Length; i++)
            {
                string login = primeiroNome[..i] + ultimoNome;

                var usuarioExistente =
                    await _repository.ObterPorLoginAsync(login);

                if (usuarioExistente == null)
                    return login;
            }

            throw new Exception(
                "Não foi possível gerar um login disponível.");
        }

        public static string[] SepararNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome)) return Array.Empty<string>();
            return NomeNormalizado.NormalizarUsuario(nome).Split(' ');


        }

    }
}
