# Mecanica API (CRUD_Oficina_Mecanica)

Última atualização: 2026-10-18

Resumo
- API REST para gestão de oficina mecânica: Clientes, Veículos, Ordens de Serviço, Cargos, Funcionários e Usuários.
- Implementada em .NET 10 com ASP.NET Core, Entity Framework Core (SQL Server) e Swagger (Swashbuckle).

Status atual (escopo incluído nesta documentação)
- API principal: Controllers, Services e Repositories implementados para Cliente, Veículo, OrdemServico e Cargo.
- Entidades: Cliente, Veiculo, OrdemServico, Cargo, Funcionario, Usuario, Permissao, GrupoPermissao e relacionamentos entre elas estão modelados e possuem migrations geradas.
- Segurança: suporte a JWT implementado (JwtService, JwtSettings, extensão de autenticação) com permissões adicionadas como claims e autorização dinâmica por políticas.
- Autorização: endpoints de Cliente, Veículo, Ordem de Serviço e Usuário possuem proteção por permissões específicas; Funcionário possui proteção por JWT.
- Usuários: login com carregamento de permissões, alteração/reset de senha e exclusão lógica por matrícula.
- Tratamento de exceções: middleware global (ExcecoesMiddleware) aplicado.
- Testes: projeto separado `Mecanica.Tests` criado com xUnit para evolução da suíte automatizada.


Arquitetura e organização
- Padrão em camadas: Controller → Service → Repository → EF Core → SQL Server
- Pastas principais (resumo):
  - Controllers/ (Cliente, Veiculo, OrdemServico, Cargo, Funcionario, Usuario)
  - Services/ (interfaces e implementações)
  - Repositories/ (interfaces e implementações)
  - Models/ (Entities, DTOs, Enums)
  - Datas/ (AppDbContext)
  - Migrations/ (histórico de banco)
  - Normalizers/ e Validations/ (normalização e validação de entrada)
  - Security/ e Extensions/ (serviço JWT, autorização por permissões, configurações e injeção de dependência)
  - Security/Autorizacao/ (atributo, requisito, policy provider e handler de permissões)
  - Middleware/ (ExcecoesMiddleware)

Principais arquivos de configuração e utilitários
- Program.cs: configura serviços, Swagger (com esquema Bearer) e ativa middleware de exceções, autenticação e autorização.
- appsettings.json / appsettings.Development.json: connection string e configurações.
- Mecanica.csproj: pacotes usados (EF Core, JwtBearer, BCrypt.Net-Next, Swashbuckle.* — presentes como dependências).

Banco de dados / Migrations
- Migrations presentes (lista):
  - 20260820204645_CreationOfMigrations.cs
  - 20260820204645_CreationOfMigrations.Designer.cs
  - 20260904005537_addCpfCnpj.cs
  - 20260904005537_addCpfCnpj.Designer.cs
  - 20260904005905_CpfCnpfObrigatorio.cs
  - 20260904005905_CpfCnpfObrigatorio.Designer.cs
  - 20260904233837_AddCargoFuncionarioUsuario.cs
  - 20260904233837_AddCargoFuncionarioUsuario.Designer.cs
  - 20260909172408_Alteracao entidade Fucnionario.cs
  - 20260909172408_Alteracao entidade Fucnionario.Designer.cs
  - 20260910174553_Usuario novos campos.cs
  - 20260910174553_Usuario novos campos.Designer.cs
   - 20261018195627_CriacaoDePermissoes.cs
  - 20261018195627_CriacaoDePermissoes.Designer.cs
  - AppDbContextModelSnapshot.cs

- Observações de modelagem no DbContext (Datas/AppDbContext.cs):
  - Índices únicos: Cliente.CpfCnpj, Veiculo.Placa, Cargo.Nome, Funcionario.CpfCnpj, Funcionario.Matricula, Usuario.Login, Permissao.Codigo, GrupoPermissao.Nome.
  - Sequências geradas pelo banco: RomaneioSequence (para OrdemServico.Romaneio) e MatriculaSequence (para Funcionario.Matricula). Ambas estão criadas nas migrations e configuradas no model.
  - Relacionamentos com DeleteBehavior.Restrict onde aplicável (ex.: vínculo Veículo→Cliente, Usuario→Funcionario, etc.).

Endpoints expostos (resumo por controller)
- Cliente — rota: /api/cliente
  - GET /api/cliente (CLIENTE_CONSULTAR)
  - GET /api/cliente/{cpfCnpj} (CLIENTE_CONSULTAR)
  - POST /api/cliente (CLIENTE_CRIAR)
  - PUT /api/cliente/{cpfCnpj} (CLIENTE_EDITAR)
  - DELETE /api/cliente/{cpfCnpj} (CLIENTE_APAGAR; soft delete)

- Veículo — rota: /api/veiculo
  - GET /api/veiculo (VEICULO_CONSULTAR)
  - GET /api/veiculo/{placa} (VEICULO_CONSULTAR)
  - POST /api/veiculo (VEICULO_CRIAR)
  - PUT /api/veiculo/{placa} (VEICULO_EDITAR)
  - DELETE /api/veiculo/{placa} (VEICULO_APAGAR; soft delete)

- Ordem de Serviço — rota: /api/ordemservico
  - GET /api/ordemservico (OS_CONSULTAR)
  - GET /api/ordemservico/{placa} (OS_CONSULTAR)
  - POST /api/ordemservico (OS_CRIAR)
  - PUT /api/ordemservico/{romaneio} (OS_EDITAR)
  - DELETE /api/ordemservico/{romaneio} (OS_APAGAR; soft delete)

- Cargo — rota: /api/cargo
  - GET /api/cargo
  - GET /api/cargo/{nome}
  - POST /api/cargo
  - DELETE /api/cargo/{nome} (soft delete com validação de vínculo)

- Funcionário — rota: /api/funcionario
  - GET /api/funcionario
  - GET /api/funcionario/{matricula}
  - POST /api/funcionario (rota protegida com [Authorize] para criação)
  - PUT /api/funcionario/{matricula}
  - DELETE /api/funcionario/{matricula} (soft delete)

- Usuário — rota: /api/usuario
  - GET /api/usuario/{matricula} (sem política de permissão ativa)
  - POST /api/usuario/Criar (USUARIO_CRIAR)
  - POST /api/usuario/Login
  - POST /api/usuario/Alterar-Senha (AllowAnonymous para primeiro acesso)
  - POST /api/usuario/Resetar-Senha (USUARIO_RESETAR_SENHA)
  - DELETE /api/usuario/{matricula} (VEICULO_APAGAR atualmente; soft delete)
  - GET /api/usuario/Permissoes/{usuarioId} (consulta de permissões; endpoint de apoio/teste)

- Teste (autorização) — rota: /api/teste
  - GET /api/teste/teste (requer token JWT)

Normalização e validação
- Normalizadores: DocumentoNormalized, PlacaNormalizado, NomeNormalizado, TelefoneNormalized, EmailNormalized, StatusNormalizado, StatusOrdemServicoNormalized.
- Validações específicas: validação de CPF/CNPJ com normalização prévia, validações por DTO para Cliente/Veículo/OrdemServico.
- Status de Ordem de Serviço aceita separadores variados (espaços, hífens, barras e sublinhados) durante a normalização.

Segurança
- JWT: implementação em Security/ (JwtService, JwtSettings) e extensão em Extensions/JwtExtesions.cs; autenticação adicionada via AddJwtAuthentication em Program.cs.
- Claims: o token inclui as permissões do usuário usando o claim personalizado `permissoes`.
- Autorização dinâmica: `RequirePermissionAttribute` cria policies no formato `Permission:<codigo>`, processadas por `PermissionPolicyProvider` e `PermissionAuthorizationHandler`.
- Permissões: os códigos são obtidos por meio dos grupos vinculados ao usuário, evitando a duplicação direta de permissões no relacionamento com usuários.
- Senhas: hash com BCrypt (Shared/Senhas.cs).

Pacotes / dependências relevantes
- Microsoft.EntityFrameworkCore, Microsoft.EntityFrameworkCore.SqlServer, Microsoft.EntityFrameworkCore.Tools
- Microsoft.AspNetCore.Authentication.JwtBearer
- BCrypt.Net-Next
- Swashbuckle.AspNetCore (Swagger)
- xUnit, Microsoft.NET.Test.Sdk e coverlet.collector (projeto Mecanica.Tests)
- Serilog.AspNetCore + sinks (pacotes presentes mas sem configuração ativa no Program.cs)

Como executar localmente
1. Instalar .NET 10 SDK
2. Ajustar connection string "DefaultConnection" em appsettings.json
3. Aplicar migrations: dotnet ef database update
4. Executar: dotnet run ou via Visual Studio (F5)

Build e testes
- Build: dotnet build — compilação local aceita (recomenda‑se rodar localmente para confirmar em sua máquina).
- Testes: o projeto separado `Mecanica.Tests` utiliza xUnit e contém a estrutura inicial para testes automatizados.

Pontos pendentes / recomendações
1. Revisar rotas e nomes nos endpoints de Usuário (algumas rotas usam segmentos com hífen e letra maiúscula — padronizar se desejar).
2. Ativar configuração central de logging estruturado (Serilog) em Program.cs se for utilizar os pacotes já adicionados.
3. Expandir a suíte de testes automatizados para controllers, serviços, repositórios e autorização.
4. Revisar o endpoint de consulta de permissões, atualmente mantido como apoio/teste, antes de utilizá-lo em produção.

Contato
- Repositório remoto: https://github.com/jamorim40/CRUD_Oficina_Mecanica

Licença
- Sem licença especificada no repositório.

---

Este README foi atualizado com base no estado atual do código-fonte (2026-10-18). O projeto de testes existente na solução foi deliberadamente excluído desta documentação conforme solicitado.
