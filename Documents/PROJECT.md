# Projeto: CRUD_Oficina_Mecanica

Última atualização: 2026-09-27

## Resumo técnico

- API REST para gestão de oficina mecânica.
- Stack: .NET 10, ASP.NET Core, Entity Framework Core, SQL Server.
- Arquitetura em camadas: Controllers → Services → Repositories → EF Core.
- Segurança já implementada com JWT + endpoints de usuário/login.
- Tratamento global de exceções implementado via middleware.

## Estrutura principal

- `Controllers/` — Cliente, Veículo, OrdemServico, Cargo, Funcionario, Usuario, Teste
- `Services/` — regras de negócio e contratos
- `Repositories/` — persistência de dados e consultas
- `Models/` — entidades, DTOs e enums
- `Datas/` — `AppDbContext` e mapeamentos
- `Migrations/` — histórico de evolução do banco
- `Normalizers/` e `Validations/` — normalização e validações de entrada
- `Security/` e `Extensions/` — geração de token JWT e configuração de autenticação
- `Middleware/` — `ExcecoesMiddleware`

## Entidades no banco

- Cliente
- Veículo
- OrdemServico
- Cargo
- Funcionario
- Usuario

Observações:
- `Cliente.CpfCnpj`, `Veiculo.Placa`, `Cargo.Nome`, `Funcionario.CpfCnpj`, `Funcionario.Matricula` e `Usuario.Login` possuem restrições de unicidade.
- `OrdemServico.Romaneio` é gerado por sequência (`RomaneioSequence`).
- `Funcionario.Matricula` é gerada por sequência (`MatriculaSequence`).
- Soft delete aplicado por campo `Ativo`.

## Endpoints implementados

### Cliente (`/api/cliente`)
- `GET /api/cliente`
- `GET /api/cliente/{cpfCnpj}`
- `POST /api/cliente`
- `PUT /api/cliente/{cpfCnpj}`
- `DELETE /api/cliente/{cpfCnpj}` (soft delete)

### Veículo (`/api/veiculo`)
- `GET /api/veiculo`
- `GET /api/veiculo/{placa}`
- `POST /api/veiculo`
- `PUT /api/veiculo/{placa}`
- `DELETE /api/veiculo/{placa}` (soft delete)

### Ordem de Serviço (`/api/ordemservico`)
- `GET /api/ordemservico`
- `GET /api/ordemservico/{placa}`
- `POST /api/ordemservico`
- `PUT /api/ordemservico/{romaneio}`
- `DELETE /api/ordemservico/{romaneio}` (soft delete)

### Cargo (`/api/cargo`)
- `GET /api/cargo`
- `GET /api/cargo/{nome}`
- `POST /api/cargo`
- `DELETE /api/cargo/{nome}` (soft delete com validação de vínculo)

### Funcionário (`/api/funcionario`)
- `GET /api/funcionario`
- `GET /api/funcionario/{matricula}`
- `POST /api/funcionario`
- `PUT /api/funcionario/{matricula}`
- `DELETE /api/funcionario/{matricula}` (soft delete)

### Usuário (`/api/usuario`)
- `GET /api/usuario/{matricula}`
- `POST /api/usuario/Criar usuário`
- `POST /api/usuario/Login`
- `POST /api/usuario/Alterar senha`
- `POST /api/usuario/Resetar senha`
- `GET /api/usuario/teste` (requer token JWT)

### Teste de autorização (`/api/teste`)
- `GET /api/teste/teste` (requer token JWT)

## Segurança e autenticação

- JWT configurado em `Program.cs` com `AddJwtAuthentication`.
- Swagger configurado com esquema Bearer.
- `UsuarioService` implementa:
  - criação de usuário com login automático e senha inicial
  - login com geração de token
  - bloqueio após tentativas inválidas
  - fluxo de primeiro acesso (troca de senha)
  - reset de usuário
- Senhas armazenadas com hash BCrypt (`Shared/Senhas.cs`).

## Tratamento de erros

- `ExcecoesMiddleware` centraliza exceções e converte para resposta JSON.
- Mapeamentos atuais:
  - `NaoEncontradoException` → 404
  - `RequisicaoInvalidaException` → 400
  - `RegraNegocioException` → 409
  - Demais exceções → 500

## Estado atual

Implementado:
- CRUD/soft delete de Cliente, Veículo, Ordem de Serviço, Cargo e Funcionário.
- Módulo de Usuário com login e JWT.
- Normalização de dados (nome, telefone, email, documento, placa, status).
- Middleware global de exceções.
- Migrations para evolução do modelo já aplicadas ao projeto.

Pendente / melhoria:
- Padronizar rotas de `UsuarioController` (atualmente há segmentos com espaço).
- Definir política de roles/perfis e expandir autorização além dos endpoints de teste.
- Configurar de fato logging estruturado (há pacotes Serilog no projeto, sem configuração ativa em `Program.cs`).
- Criar suíte de testes automatizados (não há testes no repositório).
- Evoluir CI/CD e documentação de deploy.
