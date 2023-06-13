using Pokemon.Services.Models;
using System.Text.Json;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;

namespace Pokemon.Services;

public class PokemonService
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _serializeOptions;
    private Table pokemonTable;

    public PokemonService(HttpClient client)
    {
        _client = client;
        pokemonTable = CreateClient();
        
        _serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    private Table CreateClient()
    {
        var dynamoClient = new AmazonDynamoDBClient();
        var table = Table.LoadTable(dynamoClient, "temp-demo");
        return table;
    }

    public async Task<Task> ProcessPokemons()
    {
        var pokemons = await GetPokemons($"{Constants.ApiURL}/{Constants.PokemonEndpointName}", new List<PokemonDto>());

        return Task.CompletedTask;
    }

    public async Task<IEnumerable<PokemonDto>?> GetPokemons(string url, List<PokemonDto> pokemons)
    {
        try
        {
            string responseBody = await _client.GetStringAsync(url);

            var data = JsonSerializer.Deserialize<PokemonsResponse>(responseBody, _serializeOptions);

            if (data is null) throw new Exception("Error al obtener la información. ");

            if (data.Results is null || data.Next is null) return pokemons;

            foreach (var pokemon in data.Results)
            {
                var pokemonDto = await GetPokemon(pokemon.Url);
                pokemons.Add(pokemonDto);
            }

            await GetPokemons(data.Next, pokemons);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine("Message :{0} ", e.Message);
        }

        return pokemons;
    }

    public async Task<PokemonDto> GetPokemon(string pokemonUrl)
    {
        string responseBody = await _client.GetStringAsync(pokemonUrl);

        var data = JsonSerializer.Deserialize<PokemonComplexResponse>(responseBody, _serializeOptions);

        if (data is null) throw new Exception("Error al obtener la información. ");

        var url = (data.Sprites.FrontDefault is not null) ? data.Sprites.FrontDefault.ToString() : "";

        return new PokemonDto()
        {
            Id = data.Id,
            Heigth = data.Height,
            Weight = data.Weight,
            Name = data.Name,
            Moves = data.Moves,
            // Types = data.Types,
            UrlAvatar = url,
        };
    }

    private async Task SavePokemon(PokemonDto pokemon)
    {
        var asJson = JsonSerializer.Serialize(pokemon);
        var doc = Document.FromJson(asJson);
        doc["Type"] = "META"; 
        await pokemonTable.PutItemAsync(doc);
        foreach (var pokemonType in pokemon.Types)
        {
            var typeDocument = new Document();
            typeDocument["Id"] = pokemon.Id;
            typeDocument["Type"] = pokemonType.Name;
            await pokemonTable.PutItemAsync(typeDocument);
        }
    }
}