﻿using Amazon.Lambda.RuntimeSupport;
using Pokemon.Services;

var client = new HttpClient();
var service = new PokemonService(client);
await service.ProcessPokemons();


//var handler = async () =>
//{
//    var client = new HttpClient();
//    var service = new PokemonService(client);
//    await service.ProcessPokemons();
//};

//await LambdaBootstrapBuilder.Create(handler).Build().RunAsync();