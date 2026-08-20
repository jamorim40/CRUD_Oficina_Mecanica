using Mecanica.Models.Entities.Bases;

namespace Mecanica.Models.Entities
{
    public class Cliente : EntityBase
    {
        public int Id { get; set; }
        public string Nome {  get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Email {  get; set; } = string.Empty;
       
    }
}
