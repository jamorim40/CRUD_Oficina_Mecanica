# Projeto: CRUD_Oficina_Mecanica

Última atualização: 2026-10-18

Nota: O projeto de testes (mecanica.Tests) está presente na solução, mas foi deliberadamente excluído desta atualização — os detalhes de testes serão implementados e documentados separadamente.

## Resumo técnico

- API REST para gestão de oficina mecânica (Clientes, Veículos, Ordens de Serviço, Cargos, Funcionários, Usuários).
- Stack: .NET 10, ASP.NET Core, Entity Framework Core (SQL Server), Swagger (Swashbuckle).
- Arquitetura em camadas: Controllers → Services → Repositories → EF Core.
- Segurança: suporte a JWT com endpoints de usuário (criar, login, alterar/resetar senha).
- Middleware de tratamento global de exceções implementado.

## Estrutura principal (itens presentes no repositório)

- Controllers/: CargoController, ClienteController, FuncionarioController, OrdemServicoController, TesteController, UsuarioController, VeiculoController
- Services/: interfaces e implementações (Services/Interfaces e Services/Service)
- Repositories/: interfaces e implementações (Repositories/Interfaces e Repositories/Repository)
- Models/: Entities, DTOs (Models/Entities, Models/Dtos/Requests, Models/Dtos/Responses), Enums
- Datas/: AppDbContext.cs (mapeamento, índices, sequências)
- Migrations/: histórico (lista abaixo)
- Normalizers/: DocumentoNormalized, EmailNormalized, NomeNormalizado, PlacaNormalizado, StatusNormalizado, StatusOrdemServicoNormalized, TelefoneNormalized
- Validations/: regras de validação por DTO (Cliente, Veiculo, OrdemServico)
- Security/: JwtService.cs, JwtSettings.cs
- Extensions/: DependencyInjectionExtensions.cs, JwtExtesions.cs, SwaggerExtensions.cs
- Middleware/: ExcecoesMiddleware.cs
- Shared/: ResultadoServico, ResultadoServicoExtensions, Senhas (hash BCrypt)
- Configuração: appsettings.json e appsettings.Development.json

## Migrations geradas (lista de arquivos)

- Migrations/20260820204645_CreationOfMigrations.cs
- Migrations/20260820204645_CreationOfMigrations.Designer.cs
- Migrations/20260904005537_addCpfCnpj.cs
- Migrations/20260904005537_addCpfCnpj.Designer.cs
- Migrations/20260904005905_CpfCnpfObrigatorio.cs
- Migrations/20260904005905_CpfCnpfObrigatorio.Designer.cs
- Migrations/20260904233837_AddCargoFuncionarioUsuario.cs
- Migrations/20260904233837_AddCargoFuncionarioUsuario.Designer.cs
- Migrations/20260909172408_Alteracao entidade Fucnionario.cs
- Migrations/20260909172408_Alteracao entidade Fucnionario.Designer.cs
- Migrations/20260910174553_Usuario novos campos.cs
- Migrations/20260910174553_Usuario novos campos.Designer.cs
- Migrations/20261018195627_CriacaoDePermissoes.Designer.cs
- Migrations/AppDbContextModelSnapshot.cs

## Modelagem e comportamento relevante (AppDbContext)

- Índices únicos configurados: Cliente.CpfCnpj, Veiculo.Placa, Cargo.Nome, Funcionario.CpfCnpj, Funcionario.Matricula, Usuario.Login, Permissao.Codigo, GrupoPermissao.Nome.
- Sequências no banco: RomaneioSequence (OrdemServico.Romaneio) e MatriculaSequence (Funcionario.Matricula) — geradas e referenciadas nas migrations e no modelo.
- Relacionamentos com DeleteBehavior.Restrict em FK sensíveis (ex.: Veiculo→Cliente, Usuario→Funcionario, GrupoPermissaoItem/UsuarioGrupoPermissao).
- Soft delete aplicado via propriedade Ativo nas entidades quando aplicável.

## Endpoints implementados (resumo)

### Cliente (rota: /api/cliente)
- GET /api/cliente
- GET /api/cliente/{cpfCnpj}
- POST /api/cliente
- PUT /api/cliente/{cpfCnpj}
- DELETE /api/cliente/{cpfCnpj} (soft delete)

### Veículo (rota: /api/veiculo)
- GET /api/veiculo
- GET /api/veiculo/{placa}
- POST /api/veiculo
- PUT /api/veiculo/{placa}
- DELETE /api/veiculo/{placa} (soft delete)

### Ordem de Serviço (rota: /api/ordemservico)
- GET /api/ordemservico
- GET /api/ordemservico/{placa}
- POST /api/ordemservico
- PUT /api/ordemservico/{romaneio}
- DELETE /api/ordemservico/{romaneio} (soft delete)

### Cargo (rota: /api/cargo)
- GET /api/cargo
- GET /api/cargo/{nome}
- POST /api/cargo
- DELETE /api/cargo/{nome} (soft delete com validação de vínculo)

### Funcionário (rota: /api/funcionario)
- GET /api/funcionario
- GET /api/funcionario/{matricula}
- POST /api/funcionario (rota protegida com [Authorize])
- PUT /api/funcionario/{matricula}
- DELETE /api/funcionario/{matricula} (soft delete)

### Usuário (rota: /api/usuario)
- GET /api/usuario/{matricula} (Authorize)
- POST /api/usuario/Criar (Authorize)
- POST /api/usuario/Login
- POST /api/usuario/Alterar-Senha (Authorize)
- POST /api/usuario/Resetar-Senha (Authorize)

### Teste (rota: /api/teste)
- GET /api/teste/teste (requer token JWT)

## Normalização e validação

- Normalizadores disponíveis: NomeNormalizado, TelefoneNormalized, EmailNormalized, DocumentoNormalized, PlacaNormalizado, StatusNormalizado, StatusOrdemServicoNormalized.
- Validações: classes de validação por DTO presentes em Validations/ e Validations/Interfaces (Cliente, Veiculo, OrdemServico, etc.).

## Segurança e autenticação

- JWT: implementação em Security/ e extensão de configuração em Extensions/JwtExtesions.cs; AddJwtAuthentication usado em Program.cs.
- Senhas: hashing com BCrypt (Shared/Senhas.cs).

## Dependências (resumo do csproj)

- Microsoft.EntityFrameworkCore (+SqlServer, Tools)
- Microsoft.AspNetCore.Authentication.JwtBearer
- BCrypt.Net-Next
- Swashbuckle.AspNetCore
- Serilog.AspNetCore, Serilog.Sinks.Console, Serilog.Sinks.File (pacotes presentes; configuração ativa de Serilog não aplicada em Program.cs)

## Como executar localmente

1. Instalar .NET 10 SDK
2. Configurar connection string "DefaultConnection" em appsettings.json
3. Aplicar migrations: dotnet ef database update
4. Executar: dotnet run ou carregar a solução no Visual Studio (F5)

## Observações e itens pendentes

1. Padronizar rotas/nomes nos endpoints de Usuário (algumas rotas usam segmentos com caracteres especiais/maiúsculas).
2. Ativar logging estruturado (Serilog) caso queira aproveitar os pacotes adicionados.
3. Implementar política de autorização baseada em permissões/roles (entidades Permissao/GrupoPermissao já modeladas).
4. Adicionar e implementar a suíte de testes no projeto mecanica.Tests (projeto existe, mas não documentado/atualizado aqui).

---

Documento gerado automaticamente com base no estado atual do repositório (2026-10-18). Testes permanecem fora desta atualização conforme solicitado.
