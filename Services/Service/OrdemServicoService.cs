using Mecanica.Extensions;
using Mecanica.Models.Dtos.Requests.OrdemServico;
using Mecanica.Models.Dtos.Responses.OrdemServico;
using Mecanica.Models.Entities;
using Mecanica.Models.Enums;
using Mecanica.Normalizers;
using Mecanica.Repositories.Interfaces;
using Mecanica.Services.Interfaces;
using Mecanica.Shared;

namespace Mecanica.Services.Service
{
    public class OrdemServicoService : IOrdemServicoService
    {
        private readonly IOrdemServicoRepository _repository;
        private readonly IVeiculoRepository _veiculoRepository;
        public OrdemServicoService(IOrdemServicoRepository repository,
                                    IVeiculoRepository veiculoRepository)
        {
            _repository = repository;
            _veiculoRepository = veiculoRepository;
        }
        public async Task<List<RespostaCriarOrdemServicoDto>> ObterTodos()
        {
            var ordemServico = await _repository.ObterTodos();

            return ordemServico.Select(o => new RespostaCriarOrdemServicoDto
            {
                Placa = o.Veiculo!.Placa,
                Romaneio = o.Romaneio,
                Marca = o.Veiculo!.Marca,
                Modelo = o.Veiculo!.Modelo,
                Descricao = o.Descricao,
                DataCadastro = o.DataCadastro,
                DataInicio = o.DataInicio,
                DataFim = o.DataFim,
                Observacao = o.Observacao,
                Status = o.Status.ObterDescricao(),
            }).ToList();
        }
        public async Task<List<RespostaCriarOrdemServicoDto>> ObterPorPlaca(string placa)
        {
            placa = PlacaNormalizado.Normalizar(placa);
            var ordemServico = await _repository.ObterPorPlaca(placa);

            if (!ordemServico.Any())
                return new List<RespostaCriarOrdemServicoDto>();

            return ordemServico.Select(o => new RespostaCriarOrdemServicoDto
            {
                Placa = o.Veiculo!.Placa,
                Romaneio = o.Romaneio,
                Marca = o.Veiculo!.Marca,
                Modelo = o.Veiculo!.Modelo,
                Descricao = o.Descricao,
                DataCadastro = o.DataCadastro,
                DataInicio = o.DataInicio,
                DataFim = o.DataFim,
                Observacao = o.Observacao,
                Status = o.Status.ObterDescricao()
            }).ToList();

        }
        public async Task<ResultadoServico<RespostaCriarOrdemServicoDto>> CriarAsync(RequisicaoCriarOrdemServicoDto dto)
        {
            dto.Placa = PlacaNormalizado.Normalizar(dto.Placa);

            var veiculos = await _veiculoRepository.ObterPorPlaca(dto.Placa);

            if (veiculos is null)
                return new ResultadoServico<RespostaCriarOrdemServicoDto>
                {
                    Sucesso = false,
                    Mensagem = $"Veículo {dto.Placa} não encontrado."
                };

            var ordemServico = new OrdemServico
            {
                VeiculoId = veiculos.Id,
                Descricao = dto.Descricao,
                Observacao = dto.Observacao,
                Status = EnumStatusOrdemServico.Aberto
            };
            ordemServico = await _repository.CriarAsync(ordemServico);
            return new ResultadoServico<RespostaCriarOrdemServicoDto>
            {
                Sucesso = true,
                Conteudo = new RespostaCriarOrdemServicoDto
                {
                    Placa = veiculos.Placa,
                    Marca = veiculos.Marca,
                    Modelo = veiculos.Modelo,
                    Romaneio = ordemServico.Romaneio,
                    Descricao = ordemServico.Descricao,
                    DataCadastro = ordemServico.DataCadastro,
                    Status = ordemServico.Status.ObterDescricao(),
                    Observacao = ordemServico.Observacao
                }
            };
        }

        public async Task<ResultadoServico<RespostaAtualizarOrdemServicoDto>> AtualizarAsync(int  romaneio,RequisicaoAtualizarOrdemServicoDto dto)
        {
            var ordemServico = await _repository.ObterPorRomaneio(romaneio);

            if (ordemServico is null)
            {
                return new ResultadoServico<RespostaAtualizarOrdemServicoDto>
                {
                    Sucesso = false,
                    Mensagem = $"Romaneio {romaneio} não encontrado."
                };
            }

                ordemServico.Status = NormalizarStatusOrdemServico.ObterStatus(dto.Status);

                ordemServico.Observacao = dto.Observacao;

                ordemServico.DataInicio = dto.DataInicio;

                ordemServico.DataFim = dto.DataFim;


            ordemServico = await _repository.AtualizarAsync(ordemServico);

            return new ResultadoServico<RespostaAtualizarOrdemServicoDto>
            {
                Sucesso = true,
                Conteudo = new RespostaAtualizarOrdemServicoDto
                {
                    DataInicio = ordemServico.DataInicio,
                    DataFim = ordemServico.DataFim,
                    Observacao = ordemServico.Observacao!,
                    Status = ordemServico.Status.ObterDescricao()
                }
            };
        }

        public async Task <ResultadoServico<string>>SoftDelete(int romaneio)
        {
            var ordemServivo = await _repository.ObterPorRomaneio(romaneio);

            if (ordemServivo is null)
            {
                return new ResultadoServico<string>
                {
                    Sucesso = false,
                    Mensagem = $"Romaneio {romaneio} não encontrado."
                };
            }

            await _repository.SoftDeleteAsync(romaneio);

            return new ResultadoServico<string>
            {
                Sucesso = true,
                Conteudo = $"Romaneio {romaneio} excluído com sucesso."
            };
        }


    }
}
