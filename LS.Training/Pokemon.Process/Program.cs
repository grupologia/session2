using Amazon.Lambda.RuntimeSupport;

var handler = () =>
{
    Console.WriteLine("Hello world");
};

await LambdaBootstrapBuilder.Create(handler).Build().RunAsync();