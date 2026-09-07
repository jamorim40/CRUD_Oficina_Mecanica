# Mecanica API (CRUD_Oficina_Mecanica)

Resumo
- API REST para gerenciamento de oficina mecânica: Clientes, Veículos, Ordens de Serviço (com soft delete) e cargos.
- Implementada em .NET 10 com ASP.NET Core, Entity Framework Core e SQL Server.

Status atual
- Implementações: CRUD Cliente e Veículo, Ordens de Serviço (create/list/update/soft delete), Cargo (endpoints completos).
- Evoluções: modelos e migrations para Funcionario e Usuario adicionadas; repositório parcial para Funcionario.

Marcos do projeto
- Início: implementação da API base com entidades Cliente, Veículo e OrdemServico e persistência via EF Core.
- Evolução: padronização de SoftDeleteAsync, implementação de Atualizar/SoftDelete para OrdemServico.
- Extensão: adição da entidade Cargo (controller/service/repository), e migrações para Funcionario e Usuario.
- Situação atual: API compilando e endpoints principais expostos; front‑end e autenticação pendentes.

Arquitetura
- Padrão em camadas: Controller → Service → Repository → EF Core → SQL Server
- Pastas principais: Controllers/, Services/, Repositories/, Models/, Datas/, Validations/, Normalizers/, Documents/

Entidades e comportamento relevante
- Cliente: soft delete via propriedade Ativo; identificação por CpfCnpj.
- Veículo: Placa única (índice), normalizada antes de persistir.
- OrdemServico: vincula-se ao Veículo; Romaneio gerado pelo banco; Status como enum com descrição; soft delete.
- Cargo / Funcionario / Usuario: Cargo implementado; Funcionario e Usuario possuem modelos e migrations; controllers/services de Funcionario/Usuario pendentes.

Endpoints principais (com parâmetros corrigidos)
- Cliente (api/cliente)
  - GET /api/cliente
  - GET /api/cliente/{CpfCnpj}
  - POST /api/cliente
  - PUT /api/cliente/{CpfCnpj}
  - DELETE /api/cliente/{CpfCnpj} (soft delete)

- Veículo (api/veiculo)
  - GET /api/veiculo
  - GET /api/veiculo/{placa}
  - POST /api/veiculo
  - PUT /api/veiculo/{placa}
  - DELETE /api/veiculo/{placa} (soft delete)

- Ordem de Serviço (api/ordemservico)
  - GET /api/ordemservico
  - GET /api/ordemservico/placa/{placa}
  - POST /api/ordemservico
  - PUT /api/ordemservico/{romaneio}
  - DELETE /api/ordemservico/{romaneio} (soft delete)

- Cargo (api/cargo)
  - GET /api/cargo
  - GET /api/cargo/{nome}
  - POST /api/cargo
  - DELETE /api/cargo/{nome} (soft delete, valida vínculo com funcionários)

Observações técnicas
- Parâmetros de rota corrigidos: Cliente usa CpfCnpj; Veículo usa placa. Essas alterações já estão refletidas nas controllers.
- Soft delete implementado via campo Ativo nas entidades.
- Normalizadores: PlacaNormalizado, TelefoneNormalizado, EmailNormalizado, StatusNormalizado.

Como executar localmente
1. Instalar .NET 10 SDK
2. Configurar connection string "DefaultConnection" em appsettings.json
3. Aplicar migrations: `dotnet ef database update`
4. Executar: `dotnet run` ou via Visual Studio (F5)

Build e testes
- Build: `dotnet build` — compilação local foi executada e foi bem‑sucedida.
- Testes: não há suíte de testes automatizada no repositório; recomenda‑se adicionar xUnit/NUnit

O que foi implementado além do previsto
- Entidade Cargo e endpoints completos (não previsto inicialmente).
- Modelos e migrações para Funcionario e Usuario adicionados (evolução do projeto).

O que está pendente (prioridade)
1. Implementar controllers e services para Funcionario e Usuario
2. Login / Autenticação (JWT/Identity) e autorização por roles
3. Middleware global de exceções e logging estruturado (Serilog)
4. Testes automatizados e CI
5. Front‑end Blazor (telas: Cliente, Funcionário, Permissões, Ordem de Serviço, Relatórios, Login)

Changelog (resumo)
- 2026-09-05: Atualizar/SoftDelete em OrdemServico implementados; SoftDeleteAsync padronizado; Placa corrigida; Cargo implementado; migrations para Funcionario/Usuario adicionadas; build OK.
- 2026-08-30: Backlog Blazor documentado; correções de nomes e remoção de NotImplementedException.


Contato
- Repositório remoto: https://github.com/jamorim40/CRUD_Oficina_Mecanica

Licença
- Sem licença especificada (adicionar conforme política do projeto).

---

Este README foi atualizado com base no estado atual do repositório e nas instruções fornecidas.
