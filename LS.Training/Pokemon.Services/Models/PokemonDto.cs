
namespace Pokemon.Services.Models;

public class PokemonDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long Heigth { get; set; }
    public long Weight { get; set; }
    public string UrlAvatar { get; set; }
    public IEnumerable<TypeElement> Types { get; set; }
    public IEnumerable<Move> Moves { get; set; }
}