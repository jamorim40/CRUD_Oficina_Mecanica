namespace Mecanica.Models.Entities.Bases
{
    public abstract class EntityBase
    {
        public bool Ativo { get; set; } = true;
        public DateTime DataCadastro { get; set; }
    }
}
