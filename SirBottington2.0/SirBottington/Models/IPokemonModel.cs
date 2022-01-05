namespace SirBottington.Models
{
    public interface IPokemonModel
    {
        string Id { get; set; }
        string Name { get; set; }
        int PokedexId { get; set; }
    }
}