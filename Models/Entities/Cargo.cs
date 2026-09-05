using Mecanica.Models.Entities.Bases;

namespace Mecanica.Models.Entities
{
    public class Cargo : EntityBase
    {
        public int Id { get; set; }
        public string Nome {  get; set; } = string.Empty;
    }
}
