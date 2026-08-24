using Mecanica.Models.Dtos.Requests.Veiculo;

namespace Mecanica.Validations.Interfaces.Veiculo
{
    public class VeiculoValidador : IVeiculoValidador
    {
        public List<string> validador(CriarVeiculoDto dto)
        {
            List<string> erros = new();
            if (string.IsNullOrWhiteSpace(dto.Marca))
                erros.Add("Marca é obrigatório. ");
            if (dto.Marca.Length > 100)
                erros.Add("O nome da marca não deve ter mais de 100 caracteres");
            if (string.IsNullOrWhiteSpace(dto.Modelo))
                erros.Add("Modelo é obrigatório. ");
            if (string.IsNullOrWhiteSpace(dto.Placa))
                erros.Add("Placa é obrigatória. ");
            return erros;
        }
    }
}
