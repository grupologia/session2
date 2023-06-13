﻿

namespace Pokemon.Services.Models;

public class PokemonsResponse
{
    public int Count { get; set; }
    public string? Next { get; set; }
    public string? Previous { get; set; }
    public IEnumerable<PokemonSimpleResponse>? Results { get; set; }
}

public class PokemonSimpleResponse
{
    public string Name { get; set; }
    public string Url { get; set; }
}