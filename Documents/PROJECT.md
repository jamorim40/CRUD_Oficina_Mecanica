# Projeto: CRUD_Oficina_Mecanica

Última atualização: 2026-09-05

## Resumo técnico

- API REST para gestão de oficina mecânica: Clientes, Veículos, Ordens de Serviço e Cargos.
- Implementada em .NET 10, ASP.NET Core, EF Core e SQL Server.
- Arquitetura em camadas: Controllers → Services → Repositories → EF Core → Banco.

## Estrutura do repositório (pastas principais)

- Controllers/ — endpoints HTTP (ClienteController, VeiculoController, OrdemServicoController, CargoController)
- Services/ — regras de negócio e interfaces (ex.: OrdemServicoService, CargoService, VeiculoService, ClienteService)
- Repositories/ — acesso a dados (implementações e interfaces)
- Models/ — entidades e DTOs (Requests / Responses)
- Datas/ — AppDbContext e configuração do EF Core; migrations em /Migrations
- Validations/ — validadores por entidade
- Normalizers/ — PlacaNormalizado, TelefoneNormalizado, EmailNormalizado, StatusNormalizado
- Documents/ — documentação do projeto (este documento)
- Shared/ — ResultadoServico<T>, utilitários e extensões

## Marcos do projeto (início → situação atual)

- Marco 1 (início): criação da API base com entidades Cliente, Veículo e OrdemServico; configuração EF Core e primeira migration.
- Marco 2 (estabilização): implementação CRUD de Cliente e Veículo, normalizadores e validações básicas; padronização de soft delete em entidades (campo Ativo).
- Marco 3 (evolução): implementação de AtualizarAsync e SoftDelete para OrdemServico; correção de DTO Veículo (Palca → Placa); padronização SoftDeleteAsync entre layers.
- Marco 4 (extensão): adição da entidade Cargo com controller/service/repository; migrations adicionadas para Funcionario e Usuario; repositório parcial para Funcionario.
- Situação atual: API compilando; endpoints principais expostos; front‑end, autenticação e testes automáticos pendentes.

## Endpoints implementados (detalhado, com identificadores corretos)

- Cliente (api/cliente)
  - GET /api/cliente — listar todos (Services/Service/ClienteService.cs → ObterTodos)
  - GET /api/cliente/{CpfCnpj} — obter por CpfCnpj (Controller: Controllers/ClienteController.cs; Service: IClienteService / ClienteService)
  - POST /api/cliente — criar (Models/Dtos/Requests/Cliente)
  - PUT /api/cliente/{CpfCnpj} — atualizar
  - DELETE /api/cliente/{CpfCnpj} — soft delete (ClienteService.SoftDeleteAsync → Repositories/Repository/ClienteRepository.cs)

- Veículo (api/veiculo)
  - GET /api/veiculo — listar (VeiculoService.ObterTodos)
  - GET /api/veiculo/{placa} — obter por placa (uses PlacaNormalizado)
  - POST /api/veiculo — criar
  - PUT /api/veiculo/{placa} — atualizar
  - DELETE /api/veiculo/{placa} — soft delete (VeiculoService / VeiculoRepository: SoftDeleteAsync)

- Ordem de Serviço (api/ordemservico)
  - GET /api/ordemservico — listar (OrdemServicoService.ObterTodos)
  - GET /api/ordemservico/placa/{placa} — listar por placa
  - POST /api/ordemservico — criar (OrdemServicoService.CriarAsync)
  - PUT /api/ordemservico/{romaneio} — atualizar (OrdemServicoService.AtualizarAsync)
  - DELETE /api/ordemservico/{romaneio} — soft delete (OrdemServicoService.SoftDelete)

- Cargo (api/cargo)
  - GET /api/cargo — listar (CargoService.ObterTodos)
  - GET /api/cargo/{nome} — obter por nome
  - POST /api/cargo — criar
  - DELETE /api/cargo/{nome} — soft delete (valida vínculo via FuncionarioRepository.ExisteFuncionarioPorCargo)

- Observação: controllers para Funcionario e Usuario não existem; apenas modelos, migrations e repositório parcial (FuncionarioRepository) estão presentes.

## Implementações recentes (resumidas com referências)

- OrdemServicoService: AtualizarAsync e SoftDelete implementados (Services/Service/OrdemServicoService.cs).
- Correções: RespostaVeiculoDto.Placa corrigido e mapeamentos atualizados (Models/Dtos/Responses/Veiculo/RespostaVeiculoDto.cs; Services/Service/VeiculoService.cs).
- Padronização SoftDeleteAsync: IVeiculoRepository / IVeiculoService / VeiculoRepository / VeiculoService / VeiculoController atualizados.
- Cargo: controller/service/repository implementados (Controllers/CargoController.cs; Services/Service/CargoService.cs; Repositories/Repository/CargoRepository.cs).
- Migrations: AddCargoFuncionarioUsuario (Migrations/20260904233837_...).

## Problemas detectados e ações tomadas

- Métodos NotImplementedException removidos; revisado e implementado ExistsAsync ou removido conforme interface atual.
- Parametrização de rotas alterada (Cliente por CpfCnpj; Veículo por placa) — refletido em controllers e README/PROJECT.

## Funcionalidades pendentes e melhorias (detalhado)

- Autenticação e autorização:
  - Implementar Identity ou JWT; tela de login (front‑end) e endpoints de autenticação (Usuario).
- Entidades/Controllers faltantes:
  - Implementar Controller/Service para Funcionario e Usuario (CRUD, login, associação com Cargo).
- Qualidade e produção:
  - Middleware global de exceções; centralizar mensagens e códigos HTTP.
  - Logging estruturado (Serilog) e correlação de requisições.
  - Testes: unitários (services/validators) e integração (endpoints/repositories); incluir xUnit/NUnit e cobertura mínima.
  - CI/CD: GitHub Actions para build/test/deploy.
  - Dockerfile e docker-compose para ambiente local.
- APIs e UX:
  - Paginação, filtros e ordenação nas listagens (clientes, veículos, ordens).
  - Exportação/relatórios (CSV/PDF).
  - Versão de API (v1/v2) quando breaking changes forem publicados.
- Banco de dados:
  - Políticas de retenção/arquivamento para soft deletes; histórico/auditoria (createdBy/updatedBy/deletedBy) se necessário.

## Checklist detalhado (feito vs a fazer)

- Feito
  - Estrutura básica (Controllers/Services/Repositories/Models) — OK
  - CRUD Cliente e Veículo — OK (identificadores: CpfCnpj / placa)
  - Ordens de Serviço: criar, listar, atualizar, soft delete — OK
  - Cargo: controller/service/repository — OK
  - Normalizadores implementados (Placa, Telefone, Email, Status) — OK
  - Correções de typos e remoção de NotImplementedException — OK
  - Build local: compilação bem‑sucedida — OK
- A fazer
  - Controllers/Services para Funcionario e Usuario
  - Autenticação (login) e autorização por roles
  - Middleware global de exceções e padronização de erros
  - Logging estruturado (Serilog) e política de logs
  - Testes unitários e de integração; integração a CI
  - Paginação/filtros e endpoints de relatório/exportação
  - Docker e processo de deploy documentado
  - Documentação Swagger ampliada (examples, responses)

## Riscos e recomendações

- Breaking changes: Palca → Placa alterou formato JSON; comunicar consumidores e versionar API quando publicar.
- Soft delete: sem política de auditoria atual; definir procedimento para restore / retenção.
- Migrations: manter migrations versionadas e documentar processo de deploy/DB update.
- Recomendação imediata: criar branch de release e não publicar breaking changes sem versionamento; implementar testes antes de expor publicamente a API.

## Procedimento para atualização do documento

- Política sugerida: atualizar Documents/PROJECT.md em cada commit que modifica comportamento/contract da API (endpoints, DTOs, migrations).
- Incluir no documento:
  - Data
  - Hash do commit (opcional)
  - Resumo das mudanças
  - Impacto (breaking / non-breaking)
- Sugestão: automatizar checklist no CI para verificar que PROJECT.md foi atualizado quando PR alterar contratos públicos.

## Roadmap (curto e médio prazo)

- Curto (0–4 semanas): implementar controllers/services para Funcionario/Usuario; middleware de exceções; testes básicos.
- Médio (1–3 meses): autenticação/authorization; front‑end Blazor (telas principais); CI/CD; relatórios.
- Longo (3+ meses): monitoramento, auditoria e melhorias de performance.

## Histórico resumido (últimas entradas)

- 2026-09-05: Atualizar/SoftDelete em OrdemServico implementados; SoftDeleteAsync padronizado; Placa corrigida; Cargo implementado; migrations Funcionario/Usuario adicionadas; build OK.
- 2026-08-30: Backlog Blazor consolidado; padronizações iniciais aplicadas.

Fim do documento.
