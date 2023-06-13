
using System.Text.Json.Serialization;

namespace Pokemon.Services.Models;

public class PokemonDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long Heigth { get; set; }
    public long Weight { get; set; }
    public string UrlAvatar { get; set; }
    public IEnumerable<TypeDto> Types { get; set; }
    public IEnumerable<MovesDto> Moves { get; set; }
}