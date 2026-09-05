using Mecanica.Models.Entities.Bases;
namespace Mecanica.Models.Entities
{
    public class Funcionario : EntityBase
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string CpfCnpj { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Email {  get; set; } = string.Empty;
        public int CargoId {  get; set; }
        public Cargo? Cargo { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
