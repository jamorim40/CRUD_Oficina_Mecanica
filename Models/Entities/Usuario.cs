using Mecanica.Models.Entities.Bases;

namespace Mecanica.Models.Entities
{
    public class Usuario : EntityBase
    {
        public int Id { get; set; }
        public string? Login {  get; set; } 
        public string? SenhaHash { get; set; } 
        public int FuncionarioId {  get; set; }
        public Funcionario Funcionario { get; set; } = null!;
        public DateTime? DataPrimeiroAcesso { get; set; }
        public DateTime? DataUltimoAcesso { get; set; }
        public bool PrimeiroAcesso { get; set; }
        public int TentativasLogin { get; set; }
        public bool Bloqueado { get; set; }

    }
}
