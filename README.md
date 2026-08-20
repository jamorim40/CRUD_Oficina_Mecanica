# Mecanica API

API REST para gerenciamento de oficina mecânica desenvolvida em ASP.NET Core com Entity Framework Core e SQL Server.

Objetivo

Fornecer uma API para gerenciar clientes, veículos e ordens de serviço, preservando histórico de manutenções e permitindo rastreabilidade dos veículos.

Tecnologias

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger (OpenAPI)
- Serilog

Visão geral da arquitetura

Projeto organizado em camadas mínimas:

- Controllers: endpoints da API
- Datas: AppDbContext e configurações de persistência
- Models: entidades e enums
- Repositories / Services: camada de acesso a dados e regras de negócio
- Validations / Middleware: validação e tratamento de erros

Entidades principais

- Cliente: dados do proprietário, soft delete via Ativo
- Veiculo: dados do veículo, FK para Cliente, índice único em Placa
- OrdemServico: registro histórico do atendimento, vinculada ao Veiculo, DataCadastro gerada pelo banco

Banco de dados

- DataCadastro: gerado no banco com GETDATE() (ValueGeneratedOnAdd)
- Romaneio: numeração sequencial gerada por uma sequence no banco (NEXT VALUE FOR RomaneioSequence). A formatação visual (ex.: 000001) deve ser feita na camada de apresentação
- Placa: índice único para evitar duplicidade

Como executar (resumo)

1. Ajustar a connection string em appsettings.Development.json ou appsettings.json
2. Build do projeto:
   dotnet build
3. Criar migration (caso ainda não exista):
   dotnet ef migrations add InitialCreate
4. Aplicar migration ao banco:
   dotnet ef database update
5. Executar a API:
   dotnet run

Observações

- Certifique-se de que o servidor SQL da connection string esteja acessível e que o usuário tenha permissões para criar banco/tabelas.
- Swagger pode ser habilitado em Program.cs para testes locais.

Sugestões de melhorias

- Documentar endpoints principais no README com exemplos de requests/responses.
- Adicionar DTOs e mapeamentos (ex.: AutoMapper) para separar entidade de contrato de API.
- Implementar tratamento global de erros via middleware e resposta padronizada de erro.
- Adicionar validações com FluentValidation e retornar erros claros de validação.
- Cobertura básica de testes unitários para serviços e integrados para repositórios.
- Revisar políticas de logging (Serilog) e incluir logs estruturados para operações críticas.

Licença

Projeto sem licença especificada.
