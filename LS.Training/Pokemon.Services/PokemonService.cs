namespace Pokemon.Services;

public class PokemonService
{
    private readonly HttpClient _client;

    public PokemonService(HttpClient client)
    {
        _client = client;
    }

    public Task ProcessPokemons()
    {
        Console.WriteLine("processing");
        /*
         * getPokemon: https://pokeapi.co/api/v2/pokemon
         * iterar pokemones llamando url
         * extraer, JObject
         * Guardarlo en dynamo
         * mientras getPokemon.next !=null
         * 
        */
        return Task.CompletedTask;
    }
}