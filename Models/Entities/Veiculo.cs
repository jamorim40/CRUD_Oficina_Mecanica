using Mecanica.Models.Entities.Bases;

namespace Mecanica.Models.Entities
{
    public class Veiculo : EntityBase
    {
        public int Id { get; set; }
        public string Marca {  get; set; }=string.Empty;
        public string Modelo { get; set;  }= string.Empty;
        public string Placa {  get; set; }=string.Empty;
        public int ClienteId {  get; set; }
        public Cliente? Cliente { get; set; } = null;
    }
}
